using UnityEngine;

[ExecuteInEditMode, AddComponentMenu("NGUI/UI/Panel")]
public class UIPanel : MonoBehaviour
{
    public bool cullWhileDragging;
    public bool depthPass;
    public bool generateNormals;
    [SerializeField, HideInInspector]
    private float mAlpha = 1f;
    private Camera mCam;
    private BetterList<Material> mChanged = new BetterList<Material>();
    private UIPanel[] mChildPanels;
    [SerializeField, HideInInspector]
    private UIDrawCall.Clipping mClipping;
    [HideInInspector, SerializeField]
    private Vector4 mClipRange = Vector4.zero;
    [HideInInspector, SerializeField]
    private Vector2 mClipSoftness = new Vector2(40f, 40f);
    private BetterList<Color32> mCols = new BetterList<Color32>();
    private float mCullTime;
    [SerializeField, HideInInspector]
    private DebugInfo mDebugInfo = DebugInfo.Gizmos;
    private bool mDepthChanged;
    private BetterList<UIDrawCall> mDrawCalls = new BetterList<UIDrawCall>();
    private GameObject mGo;
    private int mLayer = -1;
    private float mMatrixTime;
    private Vector2 mMax = Vector2.zero;
    private Vector2 mMin = Vector2.zero;
    private BetterList<Vector3> mNorms = new BetterList<Vector3>();
    private BetterList<Vector4> mTans = new BetterList<Vector4>();
    private static float[] mTemp = new float[4];
    private Transform mTrans;
    private float mUpdateTime;
    private BetterList<Vector2> mUvs = new BetterList<Vector2>();
    private BetterList<Vector3> mVerts = new BetterList<Vector3>();
    private BetterList<UIWidget> mWidgets = new BetterList<UIWidget>();
    public OnChangeDelegate onChange;
    public bool showInPanelTool = true;
    public bool widgetsAreStatic;
    [HideInInspector]
    public Matrix4x4 worldToLocal = Matrix4x4.identity;

    public void AddWidget(UIWidget w)
    {
        if (w != null && !mWidgets.Contains(w))
        {
            mWidgets.Add(w);
            if (!mChanged.Contains(w.material))
            {
                mChanged.Add(w.material);
            }
            mDepthChanged = true;
        }
    }

    private void Awake()
    {
        mGo = gameObject;
        mTrans = transform;
    }

    public Vector3 CalculateConstrainOffset(Vector2 min, Vector2 max)
    {
        var num = clipRange.z * 0.5f;
        var num2 = clipRange.w * 0.5f;
        var minRect = new Vector2(min.x, min.y);
        var maxRect = new Vector2(max.x, max.y);
        var minArea = new Vector2(clipRange.x - num, clipRange.y - num2);
        var maxArea = new Vector2(clipRange.x + num, clipRange.y + num2);
        if (clipping == UIDrawCall.Clipping.SoftClip)
        {
            minArea.x += clipSoftness.x;
            minArea.y += clipSoftness.y;
            maxArea.x -= clipSoftness.x;
            maxArea.y -= clipSoftness.y;
        }
        return NGUIMath.ConstrainRect(minRect, maxRect, minArea, maxArea);
    }

    public bool ConstrainTargetToBounds(Transform target, bool immediate)
    {
        Bounds targetBounds = NGUIMath.CalculateRelativeWidgetBounds(cachedTransform, target);
        return ConstrainTargetToBounds(target, ref targetBounds, immediate);
    }

    public bool ConstrainTargetToBounds(Transform target, ref Bounds targetBounds, bool immediate)
    {
        var vector = CalculateConstrainOffset(targetBounds.min, targetBounds.max);
        if (vector.magnitude <= 0f)
        {
            return false;
        }
        if (immediate)
        {
            target.localPosition += vector;
            targetBounds.center += vector;
            var component = target.GetComponent<SpringPosition>();
            if (component != null)
            {
                component.enabled = false;
            }
        }
        else
        {
            var position2 = SpringPosition.Begin(target.gameObject, target.localPosition + vector, 13f);
            position2.ignoreTimeScale = true;
            position2.worldSpace = false;
        }
        return true;
    }

    private void Fill(Material mat)
    {
        var index = 0;
        while (index < mWidgets.size)
        {
            UIWidget widget = mWidgets.buffer[index];
            if (widget == null)
            {
                mWidgets.RemoveAt(index);
            }
            else
            {
                if (widget.material == mat && widget.isVisible)
                {
                    if (widget.panel == this)
                    {
                        if (generateNormals)
                        {
                            widget.WriteToBuffers(mVerts, mUvs, mCols, mNorms, mTans);
                        }
                        else
                        {
                            widget.WriteToBuffers(mVerts, mUvs, mCols, null, null);
                        }
                    }
                    else
                    {
                        mWidgets.RemoveAt(index);
                        continue;
                    }
                }
                index++;
            }
        }
        if (mVerts.size > 0)
        {
            var drawCall = GetDrawCall(mat, true);
            drawCall.depthPass = depthPass && mClipping == UIDrawCall.Clipping.None;
            drawCall.Set(mVerts, !generateNormals ? null : mNorms, !generateNormals ? null : mTans, mUvs, mCols);
        }
        else
        {
            var item = GetDrawCall(mat, false);
            if (item != null)
            {
                mDrawCalls.Remove(item);
                NGUITools.DestroyImmediate(item.gameObject);
            }
        }
        mVerts.Clear();
        mNorms.Clear();
        mTans.Clear();
        mUvs.Clear();
        mCols.Clear();
    }

    public static UIPanel Find(Transform trans)
    {
        return Find(trans, true);
    }

    public static UIPanel Find(Transform trans, bool createIfMissing)
    {
        var transform = trans;
        UIPanel component = null;
        while (component == null && trans != null)
        {
            component = trans.GetComponent<UIPanel>();
            if (component != null || trans.parent == null)
            {
                break;
            }
            trans = trans.parent;
        }
        if (createIfMissing && component == null && trans != transform)
        {
            component = trans.gameObject.AddComponent<UIPanel>();
            SetChildLayer(component.cachedTransform, component.cachedGameObject.layer);
        }
        return component;
    }

    private UIDrawCall GetDrawCall(Material mat, bool createIfMissing)
    {
        var index = 0;
        int size = drawCalls.size;
        while (index < size)
        {
            UIDrawCall call = drawCalls.buffer[index];
            if (call.material == mat)
            {
                return call;
            }
            index++;
        }
        UIDrawCall item = null;
        if (createIfMissing)
        {
            var target = new GameObject("_UIDrawCall [" + mat.name + "]");
            DontDestroyOnLoad(target);
            target.layer = cachedGameObject.layer;
            item = target.AddComponent<UIDrawCall>();
            item.material = mat;
            mDrawCalls.Add(item);
        }
        return item;
    }

    public bool IsVisible(UIWidget w)
    {
        if (mAlpha < 0.001f)
        {
            return false;
        }
        if (!w.enabled || !NGUITools.GetActive(w.cachedGameObject) || w.alpha < 0.001f)
        {
            return false;
        }
        if (mClipping == UIDrawCall.Clipping.None)
        {
            return true;
        }
        var relativeSize = w.relativeSize;
        var vector2 = Vector2.Scale(w.pivotOffset, relativeSize);
        var vector3 = vector2;
        vector2.x += relativeSize.x;
        vector2.y -= relativeSize.y;
        var cachedTransform = w.cachedTransform;
        var a = cachedTransform.TransformPoint(vector2);
        var b = cachedTransform.TransformPoint(new Vector2(vector2.x, vector3.y));
        var c = cachedTransform.TransformPoint(new Vector2(vector3.x, vector2.y));
        var d = cachedTransform.TransformPoint(vector3);
        return IsVisible(a, b, c, d);
    }

    public bool IsVisible(Vector3 worldPos)
    {
        if (mAlpha < 0.001f)
        {
            return false;
        }
        if (mClipping != UIDrawCall.Clipping.None)
        {
            UpdateTransformMatrix();
            var vector = worldToLocal.MultiplyPoint3x4(worldPos);
            if (vector.x < mMin.x)
            {
                return false;
            }
            if (vector.y < mMin.y)
            {
                return false;
            }
            if (vector.x > mMax.x)
            {
                return false;
            }
            if (vector.y > mMax.y)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsVisible(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        UpdateTransformMatrix();
        a = worldToLocal.MultiplyPoint3x4(a);
        b = worldToLocal.MultiplyPoint3x4(b);
        c = worldToLocal.MultiplyPoint3x4(c);
        d = worldToLocal.MultiplyPoint3x4(d);
        mTemp[0] = a.x;
        mTemp[1] = b.x;
        mTemp[2] = c.x;
        mTemp[3] = d.x;
        var num = Mathf.Min(mTemp);
        var num2 = Mathf.Max(mTemp);
        mTemp[0] = a.y;
        mTemp[1] = b.y;
        mTemp[2] = c.y;
        mTemp[3] = d.y;
        var num3 = Mathf.Min(mTemp);
        var num4 = Mathf.Max(mTemp);
        if (num2 < mMin.x)
        {
            return false;
        }
        if (num4 < mMin.y)
        {
            return false;
        }
        if (num > mMax.x)
        {
            return false;
        }
        if (num3 > mMax.y)
        {
            return false;
        }
        return true;
    }

    private void LateUpdate()
    {
        mUpdateTime = Time.realtimeSinceStartup;
        UpdateTransformMatrix();
        if (mLayer != cachedGameObject.layer)
        {
            mLayer = mGo.layer;
            var camera = UICamera.FindCameraForLayer(mLayer);
            mCam = camera == null ? NGUITools.FindCameraForLayer(mLayer) : camera.cachedCamera;
            SetChildLayer(cachedTransform, mLayer);
            var num = 0;
            int num2 = drawCalls.size;
            while (num < num2)
            {
                mDrawCalls.buffer[num].gameObject.layer = mLayer;
                num++;
            }
        }
        var forceVisible = !cullWhileDragging ? clipping == UIDrawCall.Clipping.None || mCullTime > mUpdateTime : false;
        var num3 = 0;
        int size = mWidgets.size;
        while (num3 < size)
        {
            UIWidget widget = mWidgets[num3];
            if (widget.UpdateGeometry(this, forceVisible) && !mChanged.Contains(widget.material))
            {
                mChanged.Add(widget.material);
            }
            num3++;
        }
        if (mChanged.size != 0)
        {
            onChange?.Invoke();
        }
        if (mDepthChanged)
        {
            mDepthChanged = false;
            mWidgets.Sort(UIWidget.CompareFunc);
        }
        var index = 0;
        int num6 = mChanged.size;
        while (index < num6)
        {
            Fill(mChanged.buffer[index]);
            index++;
        }
        UpdateDrawcalls();
        mChanged.Clear();
    }

    public void MarkMaterialAsChanged(Material mat, bool sort)
    {
        if (mat != null)
        {
            if (sort)
            {
                mDepthChanged = true;
            }
            if (!mChanged.Contains(mat))
            {
                mChanged.Add(mat);
            }
        }
    }

    private void OnDisable()
    {
        int size = mDrawCalls.size;
        while (size > 0)
        {
            UIDrawCall call = mDrawCalls.buffer[--size];
            if (call != null)
            {
                NGUITools.DestroyImmediate(call.gameObject);
            }
        }
        mDrawCalls.Clear();
        mChanged.Clear();
    }

    private void OnEnable()
    {
        var index = 0;
        while (index < mWidgets.size)
        {
            UIWidget widget = mWidgets.buffer[index];
            if (widget != null)
            {
                MarkMaterialAsChanged(widget.material, true);
                index++;
            }
            else
            {
                mWidgets.RemoveAt(index);
            }
        }
    }

    public void Refresh()
    {
        var componentsInChildren = GetComponentsInChildren<UIWidget>();
        var index = 0;
        var length = componentsInChildren.Length;
        while (index < length)
        {
            componentsInChildren[index].Update();
            index++;
        }
        LateUpdate();
    }

    public void RemoveWidget(UIWidget w)
    {
        if (w != null && w != null && mWidgets.Remove(w) && w.material != null)
        {
            mChanged.Add(w.material);
        }
    }

    public void SetAlphaRecursive(float val, bool rebuildList)
    {
        if (rebuildList || mChildPanels == null)
        {
            mChildPanels = GetComponentsInChildren<UIPanel>(true);
        }
        var index = 0;
        var length = mChildPanels.Length;
        while (index < length)
        {
            mChildPanels[index].alpha = val;
            index++;
        }
    }

    private static void SetChildLayer(Transform t, int layer)
    {
        for (var i = 0; i < t.childCount; i++)
        {
            var child = t.GetChild(i);
            if (child.GetComponent<UIPanel>() == null)
            {
                if (child.GetComponent<UIWidget>() != null)
                {
                    child.gameObject.layer = layer;
                }
                SetChildLayer(child, layer);
            }
        }
    }

    private void Start()
    {
        mLayer = mGo.layer;
        var camera = UICamera.FindCameraForLayer(mLayer);
        mCam = camera == null ? NGUITools.FindCameraForLayer(mLayer) : camera.cachedCamera;
    }

    public void UpdateDrawcalls()
    {
        var zero = Vector4.zero;
        if (mClipping != UIDrawCall.Clipping.None)
        {
            zero = new Vector4(mClipRange.x, mClipRange.y, mClipRange.z * 0.5f, mClipRange.w * 0.5f);
        }
        if (zero.z == 0f)
        {
            zero.z = Screen.width * 0.5f;
        }
        if (zero.w == 0f)
        {
            zero.w = Screen.height * 0.5f;
        }
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsWebPlayer:
            case RuntimePlatform.WindowsEditor:
                zero.x -= 0.5f;
                zero.y += 0.5f;
                break;
        }
        var cachedTransform = this.cachedTransform;
        var index = 0;
        int size = mDrawCalls.size;
        while (index < size)
        {
            UIDrawCall call = mDrawCalls.buffer[index];
            call.clipping = mClipping;
            call.clipRange = zero;
            call.clipSoftness = mClipSoftness;
            call.depthPass = depthPass && mClipping == UIDrawCall.Clipping.None;
            var transform = call.transform;
            transform.position = cachedTransform.position;
            transform.rotation = cachedTransform.rotation;
            transform.localScale = cachedTransform.lossyScale;
            index++;
        }
    }

    private void UpdateTransformMatrix()
    {
        if (mUpdateTime == 0f || mMatrixTime != mUpdateTime)
        {
            mMatrixTime = mUpdateTime;
            worldToLocal = cachedTransform.worldToLocalMatrix;
            if (mClipping != UIDrawCall.Clipping.None)
            {
                var vector = new Vector2(mClipRange.z, mClipRange.w);
                if (vector.x == 0f)
                {
                    vector.x = mCam != null ? mCam.pixelWidth : Screen.width;
                }
                if (vector.y == 0f)
                {
                    vector.y = mCam != null ? mCam.pixelHeight : Screen.height;
                }
                vector = vector * 0.5f;
                mMin.x = mClipRange.x - vector.x;
                mMin.y = mClipRange.y - vector.y;
                mMax.x = mClipRange.x + vector.x;
                mMax.y = mClipRange.y + vector.y;
            }
        }
    }

    public float alpha
    {
        get
        {
            return mAlpha;
        }
        set
        {
            var num = Mathf.Clamp01(value);
            if (mAlpha != num)
            {
                mAlpha = num;
                for (var i = 0; i < mDrawCalls.size; i++)
                {
                    UIDrawCall call = mDrawCalls[i];
                    MarkMaterialAsChanged(call.material, false);
                }
                for (var j = 0; j < mWidgets.size; j++)
                {
                    mWidgets[j].MarkAsChangedLite();
                }
            }
        }
    }

    public GameObject cachedGameObject
    {
        get
        {
            if (mGo == null)
            {
                mGo = gameObject;
            }
            return mGo;
        }
    }

    public Transform cachedTransform
    {
        get
        {
            if (mTrans == null)
            {
                mTrans = transform;
            }
            return mTrans;
        }
    }

    public UIDrawCall.Clipping clipping
    {
        get
        {
            return mClipping;
        }
        set
        {
            if (mClipping != value)
            {
                mClipping = value;
                mMatrixTime = 0f;
                UpdateDrawcalls();
            }
        }
    }

    public Vector4 clipRange
    {
        get
        {
            return mClipRange;
        }
        set
        {
            if (mClipRange != value)
            {
                mCullTime = mCullTime != 0f ? Time.realtimeSinceStartup + 0.15f : 0.001f;
                mClipRange = value;
                mMatrixTime = 0f;
                UpdateDrawcalls();
            }
        }
    }

    public Vector2 clipSoftness
    {
        get
        {
            return mClipSoftness;
        }
        set
        {
            if (mClipSoftness != value)
            {
                mClipSoftness = value;
                UpdateDrawcalls();
            }
        }
    }

    public DebugInfo debugInfo
    {
        get
        {
            return mDebugInfo;
        }
        set
        {
            if (mDebugInfo != value)
            {
                mDebugInfo = value;
                var drawCalls = this.drawCalls;
                var flags = mDebugInfo != DebugInfo.Geometry ? HideFlags.HideAndDontSave : HideFlags.NotEditable | HideFlags.DontSave;
                var num = 0;
                int size = drawCalls.size;
                while (num < size)
                {
                    UIDrawCall call = drawCalls[num];
                    var gameObject = call.gameObject;
                    NGUITools.SetActiveSelf(gameObject, false);
                    gameObject.hideFlags = flags;
                    NGUITools.SetActiveSelf(gameObject, true);
                    num++;
                }
            }
        }
    }

    public BetterList<UIDrawCall> drawCalls
    {
        get
        {
            int size = mDrawCalls.size;
            while (size > 0)
            {
                UIDrawCall call = mDrawCalls[--size];
                if (call == null)
                {
                    mDrawCalls.RemoveAt(size);
                }
            }
            return mDrawCalls;
        }
    }

    public BetterList<UIWidget> widgets
    {
        get
        {
            return mWidgets;
        }
    }

    public enum DebugInfo
    {
        None,
        Gizmos,
        Geometry
    }

    public delegate void OnChangeDelegate();
}

