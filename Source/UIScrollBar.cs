using System;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Scroll Bar"), ExecuteInEditMode]
public class UIScrollBar : MonoBehaviour
{
    [HideInInspector, SerializeField] private UISprite mBG;
    private Camera mCam;
    [HideInInspector, SerializeField] private Direction mDir;
    [HideInInspector, SerializeField] private UISprite mFG;
    [SerializeField, HideInInspector] private bool mInverted;
    private bool mIsDirty;
    private Vector2 mScreenPos = Vector2.zero;
    [SerializeField, HideInInspector] private float mScroll;
    [HideInInspector, SerializeField] private float mSize = 1f;
    private Transform mTrans;
    public OnScrollBarChange onChange;
    public OnDragFinished onDragFinished;

    private void CenterOnPos(Vector2 localPos)
    {
        if (mBG != null && mFG != null)
        {
            var bounds = NGUIMath.CalculateRelativeInnerBounds(cachedTransform, mBG);
            var bounds2 = NGUIMath.CalculateRelativeInnerBounds(cachedTransform, mFG);
            if (mDir == Direction.Horizontal)
            {
                var num = bounds.size.x - bounds2.size.x;
                var num2 = num * 0.5f;
                var num3 = bounds.center.x - num2;
                var num4 = num <= 0f ? 0f : (localPos.x - num3) / num;
                scrollValue = !mInverted ? num4 : 1f - num4;
            }
            else
            {
                var num5 = bounds.size.y - bounds2.size.y;
                var num6 = num5 * 0.5f;
                var num7 = bounds.center.y - num6;
                var num8 = num5 <= 0f ? 0f : 1f - (localPos.y - num7) / num5;
                scrollValue = !mInverted ? num8 : 1f - num8;
            }
        }
    }

    public void ForceUpdate()
    {
        mIsDirty = false;
        if (mBG != null && mFG != null)
        {
            mSize = Mathf.Clamp01(mSize);
            mScroll = Mathf.Clamp01(mScroll);
            var border = mBG.border;
            var vector2 = mFG.border;
            var vector3 = new Vector2(Mathf.Max(0f, mBG.cachedTransform.localScale.x - border.x - border.z), Mathf.Max(0f, mBG.cachedTransform.localScale.y - border.y - border.w));
            var num = !mInverted ? mScroll : 1f - mScroll;
            if (mDir == Direction.Horizontal)
            {
                var vector6 = new Vector2(vector3.x * mSize, vector3.y);
                mFG.pivot = UIWidget.Pivot.Left;
                mBG.pivot = UIWidget.Pivot.Left;
                mBG.cachedTransform.localPosition = Vector3.zero;
                mFG.cachedTransform.localPosition = new Vector3(border.x - vector2.x + (vector3.x - vector6.x) * num, 0f, 0f);
                mFG.cachedTransform.localScale = new Vector3(vector6.x + vector2.x + vector2.z, vector6.y + vector2.y + vector2.w, 1f);
                if (num < 0.999f && num > 0.001f)
                {
                    mFG.MakePixelPerfect();
                }
            }
            else
            {
                var vector7 = new Vector2(vector3.x, vector3.y * mSize);
                mFG.pivot = UIWidget.Pivot.Top;
                mBG.pivot = UIWidget.Pivot.Top;
                mBG.cachedTransform.localPosition = Vector3.zero;
                mFG.cachedTransform.localPosition = new Vector3(0f, -border.y + vector2.y - (vector3.y - vector7.y) * num, 0f);
                mFG.cachedTransform.localScale = new Vector3(vector7.x + vector2.x + vector2.z, vector7.y + vector2.y + vector2.w, 1f);
                if (num < 0.999f && num > 0.001f)
                {
                    mFG.MakePixelPerfect();
                }
            }
        }
    }

    private void OnDragBackground(GameObject go, Vector2 delta)
    {
        mCam = UICamera.currentCamera;
        Reposition(UICamera.lastTouchPosition);
    }

    private void OnDragForeground(GameObject go, Vector2 delta)
    {
        mCam = UICamera.currentCamera;
        Reposition(mScreenPos + UICamera.currentTouch.totalDelta);
    }

    private void OnPressBackground(GameObject go, bool isPressed)
    {
        mCam = UICamera.currentCamera;
        Reposition(UICamera.lastTouchPosition);
        if (!isPressed)
        {
            onDragFinished?.Invoke();
        }
    }

    private void OnPressForeground(GameObject go, bool isPressed)
    {
        if (isPressed)
        {
            mCam = UICamera.currentCamera;
            var bounds = NGUIMath.CalculateAbsoluteWidgetBounds(mFG.cachedTransform);
            mScreenPos = mCam.WorldToScreenPoint(bounds.center);
        }
        else
        {
            onDragFinished?.Invoke();
        }
    }

    private void Reposition(Vector2 screenPos)
    {
        float num;
        var cachedTransform = this.cachedTransform;
        var plane = new Plane(cachedTransform.rotation * Vector3.back, cachedTransform.position);
        var ray = cachedCamera.ScreenPointToRay(screenPos);
        if (plane.Raycast(ray, out num))
        {
            CenterOnPos(cachedTransform.InverseTransformPoint(ray.GetPoint(num)));
        }
    }

    private void Start()
    {
        if (background != null && background.collider != null)
        {
            var listener = UIEventListener.Get(background.gameObject);
            listener.onPress = (UIEventListener.BoolDelegate) Delegate.Combine(listener.onPress, new UIEventListener.BoolDelegate(OnPressBackground));
            listener.onDrag = (UIEventListener.VectorDelegate) Delegate.Combine(listener.onDrag, new UIEventListener.VectorDelegate(OnDragBackground));
        }

        if (foreground != null && foreground.collider != null)
        {
            var listener2 = UIEventListener.Get(foreground.gameObject);
            listener2.onPress = (UIEventListener.BoolDelegate) Delegate.Combine(listener2.onPress, new UIEventListener.BoolDelegate(OnPressForeground));
            listener2.onDrag = (UIEventListener.VectorDelegate) Delegate.Combine(listener2.onDrag, new UIEventListener.VectorDelegate(OnDragForeground));
        }

        ForceUpdate();
    }

    private void Update()
    {
        if (mIsDirty)
        {
            ForceUpdate();
        }
    }

    public float alpha
    {
        get
        {
            if (mFG != null)
            {
                return mFG.alpha;
            }

            if (mBG != null)
            {
                return mBG.alpha;
            }

            return 0f;
        }
        set
        {
            if (mFG != null)
            {
                mFG.alpha = value;
                NGUITools.SetActiveSelf(mFG.gameObject, mFG.alpha > 0.001f);
            }

            if (mBG != null)
            {
                mBG.alpha = value;
                NGUITools.SetActiveSelf(mBG.gameObject, mBG.alpha > 0.001f);
            }
        }
    }

    public UISprite background
    {
        get { return mBG; }
        set
        {
            if (mBG != value)
            {
                mBG = value;
                mIsDirty = true;
            }
        }
    }

    public float barSize
    {
        get { return mSize; }
        set
        {
            var num = Mathf.Clamp01(value);
            if (mSize != num)
            {
                mSize = num;
                mIsDirty = true;
                onChange?.Invoke(this);
            }
        }
    }

    public Camera cachedCamera
    {
        get
        {
            if (mCam == null)
            {
                mCam = NGUITools.FindCameraForLayer(gameObject.layer);
            }

            return mCam;
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

    public Direction direction
    {
        get { return mDir; }
        set
        {
            if (mDir != value)
            {
                mDir = value;
                mIsDirty = true;
                if (mBG != null)
                {
                    var cachedTransform = mBG.cachedTransform;
                    var localScale = cachedTransform.localScale;
                    if (mDir == Direction.Vertical && localScale.x > localScale.y || mDir == Direction.Horizontal && localScale.x < localScale.y)
                    {
                        var x = localScale.x;
                        localScale.x = localScale.y;
                        localScale.y = x;
                        cachedTransform.localScale = localScale;
                        ForceUpdate();
                        if (mBG.collider != null)
                        {
                            NGUITools.AddWidgetCollider(mBG.gameObject);
                        }

                        if (mFG.collider != null)
                        {
                            NGUITools.AddWidgetCollider(mFG.gameObject);
                        }
                    }
                }
            }
        }
    }

    public UISprite foreground
    {
        get { return mFG; }
        set
        {
            if (mFG != value)
            {
                mFG = value;
                mIsDirty = true;
            }
        }
    }

    public bool inverted
    {
        get { return mInverted; }
        set
        {
            if (mInverted != value)
            {
                mInverted = value;
                mIsDirty = true;
            }
        }
    }

    public float scrollValue
    {
        get { return mScroll; }
        set
        {
            var num = Mathf.Clamp01(value);
            if (mScroll != num)
            {
                mScroll = num;
                mIsDirty = true;
                onChange?.Invoke(this);
            }
        }
    }

    public enum Direction
    {
        Horizontal,
        Vertical
    }

    public delegate void OnDragFinished();

    public delegate void OnScrollBarChange(UIScrollBar sb);
}