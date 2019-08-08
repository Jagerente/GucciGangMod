using UnityEngine;

[RequireComponent(typeof(Camera)), AddComponentMenu("NGUI/Interaction/Draggable Camera")]
public class UIDraggableCamera : IgnoreTimeScale
{
    public UIDragObject.DragEffect dragEffect = UIDragObject.DragEffect.MomentumAndSpring;
    private Bounds mBounds;
    private Camera mCam;
    private bool mDragStarted;
    private Vector2 mMomentum = Vector2.zero;
    public float momentumAmount = 35f;
    private bool mPressed;
    private UIRoot mRoot;
    private float mScroll;
    private Transform mTrans;
    public Transform rootForBounds;
    public Vector2 scale = Vector2.one;
    public float scrollWheelFactor;
    public bool smoothDragStart = true;

    private void Awake()
    {
        mCam = camera;
        mTrans = transform;
        if (rootForBounds == null)
        {
            Debug.LogError(NGUITools.GetHierarchy(gameObject) + " needs the 'Root For Bounds' parameter to be set",
                this);
            enabled = false;
        }
    }

    private Vector3 CalculateConstrainOffset()
    {
        if (rootForBounds == null || rootForBounds.childCount == 0)
        {
            return Vector3.zero;
        }

        var position = new Vector3(mCam.rect.xMin * Screen.width, mCam.rect.yMin * Screen.height, 0f);
        var vector2 = new Vector3(mCam.rect.xMax * Screen.width, mCam.rect.yMax * Screen.height, 0f);
        position = mCam.ScreenToWorldPoint(position);
        vector2 = mCam.ScreenToWorldPoint(vector2);
        var minRect = new Vector2(mBounds.min.x, mBounds.min.y);
        var maxRect = new Vector2(mBounds.max.x, mBounds.max.y);
        return NGUIMath.ConstrainRect(minRect, maxRect, position, vector2);
    }

    public bool ConstrainToBounds(bool immediate)
    {
        if (mTrans != null && rootForBounds != null)
        {
            var vector = CalculateConstrainOffset();
            if (vector.magnitude > 0f)
            {
                if (immediate)
                {
                    mTrans.position -= vector;
                }
                else
                {
                    var position = SpringPosition.Begin(gameObject, mTrans.position - vector, 13f);
                    position.ignoreTimeScale = true;
                    position.worldSpace = true;
                }

                return true;
            }
        }

        return false;
    }

    public void Drag(Vector2 delta)
    {
        if (smoothDragStart && !mDragStarted)
        {
            mDragStarted = true;
        }
        else
        {
            UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
            if (mRoot != null)
            {
                delta = delta * mRoot.pixelSizeAdjustment;
            }

            var vector = Vector2.Scale(delta, -scale);
            mTrans.localPosition += (Vector3)vector;
            mMomentum = Vector2.Lerp(mMomentum, mMomentum + vector * (0.01f * momentumAmount), 0.67f);
            if (dragEffect != UIDragObject.DragEffect.MomentumAndSpring && ConstrainToBounds(true))
            {
                mMomentum = Vector2.zero;
                mScroll = 0f;
            }
        }
    }

    public void Press(bool isPressed)
    {
        if (isPressed)
        {
            mDragStarted = false;
        }

        if (rootForBounds != null)
        {
            mPressed = isPressed;
            if (isPressed)
            {
                mBounds = NGUIMath.CalculateAbsoluteWidgetBounds(rootForBounds);
                mMomentum = Vector2.zero;
                mScroll = 0f;
                var component = GetComponent<SpringPosition>();
                if (component != null)
                {
                    component.enabled = false;
                }
            }
            else if (dragEffect == UIDragObject.DragEffect.MomentumAndSpring)
            {
                ConstrainToBounds(false);
            }
        }
    }

    public void Scroll(float delta)
    {
        if (enabled && NGUITools.GetActive(gameObject))
        {
            if (Mathf.Sign(mScroll) != Mathf.Sign(delta))
            {
                mScroll = 0f;
            }

            mScroll += delta * scrollWheelFactor;
        }
    }

    private void Start()
    {
        mRoot = NGUITools.FindInParents<UIRoot>(gameObject);
    }

    private void Update()
    {
        var deltaTime = UpdateRealTimeDelta();
        if (mPressed)
        {
            var component = GetComponent<SpringPosition>();
            if (component != null)
            {
                component.enabled = false;
            }

            mScroll = 0f;
        }
        else
        {
            mMomentum += scale * (mScroll * 20f);
            mScroll = NGUIMath.SpringLerp(mScroll, 0f, 20f, deltaTime);
            if (mMomentum.magnitude > 0.01f)
            {
                mTrans.localPosition += (Vector3)NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
                mBounds = NGUIMath.CalculateAbsoluteWidgetBounds(rootForBounds);
                if (!ConstrainToBounds(dragEffect == UIDragObject.DragEffect.None))
                {
                    var component2 = GetComponent<SpringPosition>();
                    if (component2 != null)
                    {
                        component2.enabled = false;
                    }
                }

                return;
            }

            mScroll = 0f;
        }

        NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
    }

    public Vector2 currentMomentum
    {
        get { return mMomentum; }
        set { mMomentum = value; }
    }
}