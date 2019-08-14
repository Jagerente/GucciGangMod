using System;
using UnityEngine;

public abstract class UIWidget : MonoBehaviour
{
    private static Comparison<UIWidget> f__amcache14;
    protected bool mChanged = true;
    [SerializeField, HideInInspector] private Color mColor = Color.white;
    [SerializeField, HideInInspector] private int mDepth;
    private bool mForceVisible;
    private UIGeometry mGeom = new UIGeometry();
    protected GameObject mGo;
    private float mLastAlpha;
    private Matrix4x4 mLocalToPanel;
    [HideInInspector, SerializeField] protected Material mMat;
    private Vector3 mOldV0;
    private Vector3 mOldV1;
    protected UIPanel mPanel;
    [HideInInspector, SerializeField] private Pivot mPivot = Pivot.Center;
    protected bool mPlayMode = true;
    [SerializeField, HideInInspector] protected Texture mTex;
    protected Transform mTrans;
    private bool mVisibleByPanel = true;

    protected virtual void Awake()
    {
        mGo = gameObject;
        mPlayMode = Application.isPlaying;
    }

    public void CheckLayer()
    {
        if (mPanel != null && mPanel.gameObject.layer != gameObject.layer)
        {
            Debug.LogWarning("You can't place widgets on a layer different than the UIPanel that manages them.\nIf you want to move widgets to a different layer, parent them to a new panel instead.", this);
            gameObject.layer = mPanel.gameObject.layer;
        }
    }

    [Obsolete("Use ParentHasChanged() instead")]
    public void CheckParent()
    {
        ParentHasChanged();
    }

    public static int CompareFunc(UIWidget left, UIWidget right)
    {
        if (left.mDepth > right.mDepth)
        {
            return 1;
        }

        if (left.mDepth < right.mDepth)
        {
            return -1;
        }

        return 0;
    }

    public void CreatePanel()
    {
        if (mPanel == null && enabled && NGUITools.GetActive(gameObject) && material != null)
        {
            mPanel = UIPanel.Find(cachedTransform);
            if (mPanel != null)
            {
                CheckLayer();
                mPanel.AddWidget(this);
                mChanged = true;
            }
        }
    }

    public virtual void MakePixelPerfect()
    {
        var localScale = cachedTransform.localScale;
        var num = Mathf.RoundToInt(localScale.x);
        var num2 = Mathf.RoundToInt(localScale.y);
        localScale.x = num;
        localScale.y = num2;
        localScale.z = 1f;
        var localPosition = cachedTransform.localPosition;
        localPosition.z = Mathf.RoundToInt(localPosition.z);
        if (num % 2 == 1 && (pivot == Pivot.Top || pivot == Pivot.Center || pivot == Pivot.Bottom))
        {
            localPosition.x = Mathf.Floor(localPosition.x) + 0.5f;
        }
        else
        {
            localPosition.x = Mathf.Round(localPosition.x);
        }

        if (num2 % 2 == 1 && (pivot == Pivot.Left || pivot == Pivot.Center || pivot == Pivot.Right))
        {
            localPosition.y = Mathf.Ceil(localPosition.y) - 0.5f;
        }
        else
        {
            localPosition.y = Mathf.Round(localPosition.y);
        }

        cachedTransform.localPosition = localPosition;
        cachedTransform.localScale = localScale;
    }

    public virtual void MarkAsChanged()
    {
        mChanged = true;
        if (mPanel != null && enabled && NGUITools.GetActive(gameObject) && !Application.isPlaying && material != null)
        {
            mPanel.AddWidget(this);
            CheckLayer();
        }
    }

    public void MarkAsChangedLite()
    {
        mChanged = true;
    }

    private void OnDestroy()
    {
        if (mPanel != null)
        {
            mPanel.RemoveWidget(this);
            mPanel = null;
        }
    }

    private void OnDisable()
    {
        if (!keepMaterial)
        {
            material = null;
        }
        else if (mPanel != null)
        {
            mPanel.RemoveWidget(this);
        }

        mPanel = null;
    }

    protected virtual void OnEnable()
    {
        mChanged = true;
        if (!keepMaterial)
        {
            mMat = null;
            mTex = null;
        }

        mPanel = null;
    }

    public virtual void OnFill(BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
    {
    }

    protected virtual void OnStart()
    {
    }

    public void ParentHasChanged()
    {
        if (mPanel != null)
        {
            var panel = UIPanel.Find(cachedTransform);
            if (mPanel != panel)
            {
                mPanel.RemoveWidget(this);
                if (!keepMaterial || Application.isPlaying)
                {
                    material = null;
                }

                mPanel = null;
                CreatePanel();
            }
        }
    }

    public static BetterList<UIWidget> Raycast(GameObject root, Vector2 mousePos)
    {
        var list = new BetterList<UIWidget>();
        var camera = UICamera.FindCameraForLayer(root.layer);
        if (camera != null)
        {
            var cachedCamera = camera.cachedCamera;
            foreach (var widget in root.GetComponentsInChildren<UIWidget>())
            {
                if (NGUIMath.DistanceToRectangle(NGUIMath.CalculateWidgetCorners(widget), mousePos, cachedCamera) == 0f)
                {
                    list.Add(widget);
                }
            }

            if (f__amcache14 == null)
            {
                f__amcache14 = (w1, w2) => w2.mDepth.CompareTo(w1.mDepth);
            }

            list.Sort(f__amcache14);
        }

        return list;
    }

    private void Start()
    {
        OnStart();
        CreatePanel();
    }

    public virtual void Update()
    {
        if (mPanel == null)
        {
            CreatePanel();
        }
    }

    public bool UpdateGeometry(UIPanel p, bool forceVisible)
    {
        if (material != null && p != null)
        {
            mPanel = p;
            var flag = false;
            var finalAlpha = this.finalAlpha;
            var flag2 = finalAlpha > 0.001f;
            var flag3 = forceVisible || mVisibleByPanel;
            if (cachedTransform.hasChanged)
            {
                mTrans.hasChanged = false;
                if (!mPanel.widgetsAreStatic)
                {
                    var relativeSize = this.relativeSize;
                    var pivotOffset = this.pivotOffset;
                    var relativePadding = this.relativePadding;
                    var x = pivotOffset.x * relativeSize.x - relativePadding.x;
                    var y = pivotOffset.y * relativeSize.y + relativePadding.y;
                    var num4 = x + relativeSize.x + relativePadding.x + relativePadding.z;
                    var num5 = y - relativeSize.y - relativePadding.y - relativePadding.w;
                    mLocalToPanel = p.worldToLocal * cachedTransform.localToWorldMatrix;
                    flag = true;
                    var v = new Vector3(x, y, 0f);
                    var vector5 = new Vector3(num4, num5, 0f);
                    v = mLocalToPanel.MultiplyPoint3x4(v);
                    vector5 = mLocalToPanel.MultiplyPoint3x4(vector5);
                    if (Vector3.SqrMagnitude(mOldV0 - v) > 1E-06f || Vector3.SqrMagnitude(mOldV1 - vector5) > 1E-06f)
                    {
                        mChanged = true;
                        mOldV0 = v;
                        mOldV1 = vector5;
                    }
                }

                if (flag2 || mForceVisible != forceVisible)
                {
                    mForceVisible = forceVisible;
                    flag3 = forceVisible || mPanel.IsVisible(this);
                }
            }
            else if (flag2 && mForceVisible != forceVisible)
            {
                mForceVisible = forceVisible;
                flag3 = mPanel.IsVisible(this);
            }

            if (mVisibleByPanel != flag3)
            {
                mVisibleByPanel = flag3;
                mChanged = true;
            }

            if (mVisibleByPanel && mLastAlpha != finalAlpha)
            {
                mChanged = true;
            }

            mLastAlpha = finalAlpha;
            if (mChanged)
            {
                mChanged = false;
                if (isVisible)
                {
                    mGeom.Clear();
                    OnFill(mGeom.verts, mGeom.uvs, mGeom.cols);
                    if (mGeom.hasVertices)
                    {
                        Vector3 vector6 = pivotOffset;
                        var vector7 = relativeSize;
                        vector6.x *= vector7.x;
                        vector6.y *= vector7.y;
                        if (!flag)
                        {
                            mLocalToPanel = p.worldToLocal * cachedTransform.localToWorldMatrix;
                        }

                        mGeom.ApplyOffset(vector6);
                        mGeom.ApplyTransform(mLocalToPanel, p.generateNormals);
                    }

                    return true;
                }

                if (mGeom.hasVertices)
                {
                    mGeom.Clear();
                    return true;
                }
            }
        }

        return false;
    }

    public void WriteToBuffers(BetterList<Vector3> v, BetterList<Vector2> u, BetterList<Color32> c, BetterList<Vector3> n, BetterList<Vector4> t)
    {
        mGeom.WriteToBuffers(v, u, c, n, t);
    }

    public float alpha
    {
        get { return mColor.a; }
        set
        {
            var mColor = this.mColor;
            mColor.a = value;
            color = mColor;
        }
    }

    public virtual Vector4 border
    {
        get { return Vector4.zero; }
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

    public Color color
    {
        get { return mColor; }
        set
        {
            if (!mColor.Equals(value))
            {
                mColor = value;
                mChanged = true;
            }
        }
    }

    public int depth
    {
        get { return mDepth; }
        set
        {
            if (mDepth != value)
            {
                mDepth = value;
                if (mPanel != null)
                {
                    mPanel.MarkMaterialAsChanged(material, true);
                }
            }
        }
    }

    public float finalAlpha
    {
        get
        {
            if (mPanel == null)
            {
                CreatePanel();
            }

            return mPanel == null ? mColor.a : mColor.a * mPanel.alpha;
        }
    }

    public bool isVisible
    {
        get { return mVisibleByPanel && finalAlpha > 0.001f; }
    }

    public virtual bool keepMaterial
    {
        get { return false; }
    }

    public virtual Texture mainTexture
    {
        get
        {
            var material = this.material;
            if (material != null)
            {
                if (material.mainTexture != null)
                {
                    mTex = material.mainTexture;
                }
                else if (mTex != null)
                {
                    if (mPanel != null)
                    {
                        mPanel.RemoveWidget(this);
                    }

                    mPanel = null;
                    mMat.mainTexture = mTex;
                    if (enabled)
                    {
                        CreatePanel();
                    }
                }
            }

            return mTex;
        }
        set
        {
            var material = this.material;
            if (material == null || material.mainTexture != value)
            {
                if (mPanel != null)
                {
                    mPanel.RemoveWidget(this);
                }

                mPanel = null;
                mTex = value;
                material = this.material;
                if (material != null)
                {
                    material.mainTexture = value;
                    if (enabled)
                    {
                        CreatePanel();
                    }
                }
            }
        }
    }

    public virtual Material material
    {
        get { return mMat; }
        set
        {
            if (mMat != value)
            {
                if (mMat != null && mPanel != null)
                {
                    mPanel.RemoveWidget(this);
                }

                mPanel = null;
                mMat = value;
                mTex = null;
                if (mMat != null)
                {
                    CreatePanel();
                }
            }
        }
    }

    public UIPanel panel
    {
        get
        {
            if (mPanel == null)
            {
                CreatePanel();
            }

            return mPanel;
        }
        set { mPanel = value; }
    }

    public Pivot pivot
    {
        get { return mPivot; }
        set
        {
            if (mPivot != value)
            {
                var vector = NGUIMath.CalculateWidgetCorners(this)[0];
                mPivot = value;
                mChanged = true;
                var vector2 = NGUIMath.CalculateWidgetCorners(this)[0];
                var cachedTransform = this.cachedTransform;
                var position = cachedTransform.position;
                var z = cachedTransform.localPosition.z;
                position.x += vector.x - vector2.x;
                position.y += vector.y - vector2.y;
                this.cachedTransform.position = position;
                position = this.cachedTransform.localPosition;
                position.x = Mathf.Round(position.x);
                position.y = Mathf.Round(position.y);
                position.z = z;
                this.cachedTransform.localPosition = position;
            }
        }
    }

    public Vector2 pivotOffset
    {
        get
        {
            var zero = Vector2.zero;
            var relativePadding = this.relativePadding;
            var pivot = this.pivot;
            switch (pivot)
            {
                case Pivot.Top:
                case Pivot.Center:
                case Pivot.Bottom:
                    zero.x = (relativePadding.x - relativePadding.z - 1f) * 0.5f;
                    break;

                case Pivot.TopRight:
                case Pivot.Right:
                case Pivot.BottomRight:
                    zero.x = -1f - relativePadding.z;
                    break;

                default:
                    zero.x = relativePadding.x;
                    break;
            }

            switch (pivot)
            {
                case Pivot.Left:
                case Pivot.Center:
                case Pivot.Right:
                    zero.y = (relativePadding.w - relativePadding.y + 1f) * 0.5f;
                    return zero;

                case Pivot.BottomLeft:
                case Pivot.Bottom:
                case Pivot.BottomRight:
                    zero.y = 1f + relativePadding.w;
                    return zero;
            }

            zero.y = -relativePadding.y;
            return zero;
        }
    }

    public virtual bool pixelPerfectAfterResize
    {
        get { return false; }
    }

    public virtual Vector4 relativePadding
    {
        get { return Vector4.zero; }
    }

    public virtual Vector2 relativeSize
    {
        get { return Vector2.one; }
    }

    public enum Pivot
    {
        TopLeft,
        Top,
        TopRight,
        Left,
        Center,
        Right,
        BottomLeft,
        Bottom,
        BottomRight
    }
}