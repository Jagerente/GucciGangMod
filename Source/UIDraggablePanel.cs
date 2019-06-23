using System;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(UIPanel)), AddComponentMenu("NGUI/Interaction/Draggable Panel")]
public class UIDraggablePanel : IgnoreTimeScale
{
    public bool disableDragIfFits;
    public DragEffect dragEffect = DragEffect.MomentumAndSpring;
    public UIScrollBar horizontalScrollBar;
    public bool iOSDragEmulation = true;
    private Bounds mBounds;
    private bool mCalculatedBounds;
    private int mDragID = -10;
    private bool mDragStarted;
    private Vector2 mDragStartOffset = Vector2.zero;
    private bool mIgnoreCallbacks;
    private Vector3 mLastPos;
    private Vector3 mMomentum = Vector3.zero;
    public float momentumAmount = 35f;
    private UIPanel mPanel;
    private Plane mPlane;
    private bool mPressed;
    private float mScroll;
    private bool mShouldMove;
    private Transform mTrans;
    public OnDragFinished onDragFinished;
    public Vector2 relativePositionOnReset = Vector2.zero;
    public bool repositionClipping;
    public bool restrictWithinPanel = true;
    public Vector3 scale = Vector3.one;
    public float scrollWheelFactor;
    public ShowCondition showScrollBars = ShowCondition.OnlyIfNeeded;
    public bool smoothDragStart = true;
    public UIScrollBar verticalScrollBar;

    private void Awake()
    {
        mTrans = transform;
        mPanel = GetComponent<UIPanel>();
        mPanel.onChange = (UIPanel.OnChangeDelegate) Delegate.Combine(mPanel.onChange, new UIPanel.OnChangeDelegate(OnPanelChange));
    }

    public void DisableSpring()
    {
        var component = GetComponent<SpringPanel>();
        if (component != null)
        {
            component.enabled = false;
        }
    }

    public void Drag()
    {
        if (enabled && NGUITools.GetActive(gameObject) && mShouldMove)
        {
            if (mDragID == -10)
            {
                mDragID = UICamera.currentTouchID;
            }
            UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
            if (smoothDragStart && !mDragStarted)
            {
                mDragStarted = true;
                mDragStartOffset = UICamera.currentTouch.totalDelta;
            }
            var ray = !smoothDragStart ? UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos) : UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos - mDragStartOffset);
            var enter = 0f;
            if (mPlane.Raycast(ray, out enter))
            {
                var point = ray.GetPoint(enter);
                var direction = point - mLastPos;
                mLastPos = point;
                if (direction.x != 0f || direction.y != 0f)
                {
                    direction = mTrans.InverseTransformDirection(direction);
                    direction.Scale(scale);
                    direction = mTrans.TransformDirection(direction);
                }
                mMomentum = Vector3.Lerp(mMomentum, mMomentum + direction * (0.01f * momentumAmount), 0.67f);
                if (!iOSDragEmulation)
                {
                    MoveAbsolute(direction);
                }
                else if (mPanel.CalculateConstrainOffset(bounds.min, bounds.max).magnitude > 0.001f)
                {
                    MoveAbsolute(direction * 0.5f);
                    mMomentum = mMomentum * 0.5f;
                }
                else
                {
                    MoveAbsolute(direction);
                }
                if (restrictWithinPanel && mPanel.clipping != UIDrawCall.Clipping.None && dragEffect != DragEffect.MomentumAndSpring)
                {
                    RestrictWithinBounds(true);
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (repositionClipping)
        {
            repositionClipping = false;
            mCalculatedBounds = false;
            SetDragAmount(relativePositionOnReset.x, relativePositionOnReset.y, true);
        }
        if (Application.isPlaying)
        {
            var deltaTime = UpdateRealTimeDelta();
            if (showScrollBars != ShowCondition.Always)
            {
                var shouldMoveVertically = false;
                var shouldMoveHorizontally = false;
                if (showScrollBars != ShowCondition.WhenDragging || mDragID != -10 || mMomentum.magnitude > 0.01f)
                {
                    shouldMoveVertically = this.shouldMoveVertically;
                    shouldMoveHorizontally = this.shouldMoveHorizontally;
                }
                if (verticalScrollBar != null)
                {
                    var num2 = verticalScrollBar.alpha + (!shouldMoveVertically ? -deltaTime * 3f : deltaTime * 6f);
                    num2 = Mathf.Clamp01(num2);
                    if (verticalScrollBar.alpha != num2)
                    {
                        verticalScrollBar.alpha = num2;
                    }
                }
                if (horizontalScrollBar != null)
                {
                    var num3 = horizontalScrollBar.alpha + (!shouldMoveHorizontally ? -deltaTime * 3f : deltaTime * 6f);
                    num3 = Mathf.Clamp01(num3);
                    if (horizontalScrollBar.alpha != num3)
                    {
                        horizontalScrollBar.alpha = num3;
                    }
                }
            }
            if (mShouldMove && !mPressed)
            {
                mMomentum -= scale * (mScroll * 0.05f);
                if (mMomentum.magnitude > 0.0001f)
                {
                    mScroll = NGUIMath.SpringLerp(mScroll, 0f, 20f, deltaTime);
                    var absolute = NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
                    MoveAbsolute(absolute);
                    if (restrictWithinPanel && mPanel.clipping != UIDrawCall.Clipping.None)
                    {
                        RestrictWithinBounds(false);
                    }
                    if (mMomentum.magnitude < 0.0001f)
                    {
                        onDragFinished?.Invoke();
                    }
                    return;
                }
                mScroll = 0f;
                mMomentum = Vector3.zero;
            }
            else
            {
                mScroll = 0f;
            }
            NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
        }
    }

    public void MoveAbsolute(Vector3 absolute)
    {
        var vector = mTrans.InverseTransformPoint(absolute);
        var vector2 = mTrans.InverseTransformPoint(Vector3.zero);
        MoveRelative(vector - vector2);
    }

    public void MoveRelative(Vector3 relative)
    {
        mTrans.localPosition += relative;
        var clipRange = mPanel.clipRange;
        clipRange.x -= relative.x;
        clipRange.y -= relative.y;
        mPanel.clipRange = clipRange;
        UpdateScrollbars(false);
    }

    private void OnDestroy()
    {
        if (mPanel != null)
        {
            mPanel.onChange = (UIPanel.OnChangeDelegate) Delegate.Remove(mPanel.onChange, new UIPanel.OnChangeDelegate(OnPanelChange));
        }
    }

    private void OnHorizontalBar(UIScrollBar sb)
    {
        if (!mIgnoreCallbacks)
        {
            var x = horizontalScrollBar == null ? 0f : horizontalScrollBar.scrollValue;
            var y = verticalScrollBar == null ? 0f : verticalScrollBar.scrollValue;
            SetDragAmount(x, y, false);
        }
    }

    private void OnPanelChange()
    {
        UpdateScrollbars(true);
    }

    private void OnVerticalBar(UIScrollBar sb)
    {
        if (!mIgnoreCallbacks)
        {
            var x = horizontalScrollBar == null ? 0f : horizontalScrollBar.scrollValue;
            var y = verticalScrollBar == null ? 0f : verticalScrollBar.scrollValue;
            SetDragAmount(x, y, false);
        }
    }

    public void Press(bool pressed)
    {
        if (smoothDragStart && pressed)
        {
            mDragStarted = false;
            mDragStartOffset = Vector2.zero;
        }
        if (enabled && NGUITools.GetActive(gameObject))
        {
            if (!pressed && mDragID == UICamera.currentTouchID)
            {
                mDragID = -10;
            }
            mCalculatedBounds = false;
            mShouldMove = shouldMove;
            if (mShouldMove)
            {
                mPressed = pressed;
                if (pressed)
                {
                    mMomentum = Vector3.zero;
                    mScroll = 0f;
                    DisableSpring();
                    mLastPos = UICamera.lastHit.point;
                    mPlane = new Plane(mTrans.rotation * Vector3.back, mLastPos);
                }
                else
                {
                    if (restrictWithinPanel && mPanel.clipping != UIDrawCall.Clipping.None && dragEffect == DragEffect.MomentumAndSpring)
                    {
                        RestrictWithinBounds(false);
                    }

                    onDragFinished?.Invoke();
                }
            }
        }
    }

    public void ResetPosition()
    {
        mCalculatedBounds = false;
        SetDragAmount(relativePositionOnReset.x, relativePositionOnReset.y, false);
        SetDragAmount(relativePositionOnReset.x, relativePositionOnReset.y, true);
    }

    public bool RestrictWithinBounds(bool instant)
    {
        var relative = mPanel.CalculateConstrainOffset(bounds.min, bounds.max);
        if (relative.magnitude <= 0.001f)
        {
            return false;
        }
        if (!instant && dragEffect == DragEffect.MomentumAndSpring)
        {
            SpringPanel.Begin(mPanel.gameObject, mTrans.localPosition + relative, 13f);
        }
        else
        {
            MoveRelative(relative);
            mMomentum = Vector3.zero;
            mScroll = 0f;
        }
        return true;
    }

    public void Scroll(float delta)
    {
        if (enabled && NGUITools.GetActive(gameObject) && scrollWheelFactor != 0f)
        {
            DisableSpring();
            mShouldMove = shouldMove;
            if (Mathf.Sign(mScroll) != Mathf.Sign(delta))
            {
                mScroll = 0f;
            }
            mScroll += delta * scrollWheelFactor;
        }
    }

    public void SetDragAmount(float x, float y, bool updateScrollbars)
    {
        DisableSpring();
        var bounds = this.bounds;
        if (bounds.min.x != bounds.max.x && bounds.min.y != bounds.max.y)
        {
            var clipRange = mPanel.clipRange;
            var num = clipRange.z * 0.5f;
            var num2 = clipRange.w * 0.5f;
            var from = bounds.min.x + num;
            var to = bounds.max.x - num;
            var num5 = bounds.min.y + num2;
            var num6 = bounds.max.y - num2;
            if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
            {
                from -= mPanel.clipSoftness.x;
                to += mPanel.clipSoftness.x;
                num5 -= mPanel.clipSoftness.y;
                num6 += mPanel.clipSoftness.y;
            }
            var num7 = Mathf.Lerp(from, to, x);
            var num8 = Mathf.Lerp(num6, num5, y);
            if (!updateScrollbars)
            {
                var localPosition = mTrans.localPosition;
                if (scale.x != 0f)
                {
                    localPosition.x += clipRange.x - num7;
                }
                if (scale.y != 0f)
                {
                    localPosition.y += clipRange.y - num8;
                }
                mTrans.localPosition = localPosition;
            }
            clipRange.x = num7;
            clipRange.y = num8;
            mPanel.clipRange = clipRange;
            if (updateScrollbars)
            {
                UpdateScrollbars(false);
            }
        }
    }

    private void Start()
    {
        UpdateScrollbars(true);
        if (horizontalScrollBar != null)
        {
            horizontalScrollBar.onChange = (UIScrollBar.OnScrollBarChange) Delegate.Combine(horizontalScrollBar.onChange, new UIScrollBar.OnScrollBarChange(OnHorizontalBar));
            horizontalScrollBar.alpha = showScrollBars != ShowCondition.Always && !shouldMoveHorizontally ? 0f : 1f;
        }
        if (verticalScrollBar != null)
        {
            verticalScrollBar.onChange = (UIScrollBar.OnScrollBarChange) Delegate.Combine(verticalScrollBar.onChange, new UIScrollBar.OnScrollBarChange(OnVerticalBar));
            verticalScrollBar.alpha = showScrollBars != ShowCondition.Always && !shouldMoveVertically ? 0f : 1f;
        }
    }

    public void UpdateScrollbars(bool recalculateBounds)
    {
        if (mPanel != null)
        {
            if (horizontalScrollBar != null || verticalScrollBar != null)
            {
                if (recalculateBounds)
                {
                    mCalculatedBounds = false;
                    mShouldMove = shouldMove;
                }
                var bounds = this.bounds;
                Vector2 min = bounds.min;
                Vector2 max = bounds.max;
                if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
                {
                    var clipSoftness = mPanel.clipSoftness;
                    min -= clipSoftness;
                    max += clipSoftness;
                }
                if (horizontalScrollBar != null && max.x > min.x)
                {
                    var clipRange = mPanel.clipRange;
                    var num = clipRange.z * 0.5f;
                    var num2 = clipRange.x - num - bounds.min.x;
                    var num3 = bounds.max.x - num - clipRange.x;
                    var num4 = max.x - min.x;
                    num2 = Mathf.Clamp01(num2 / num4);
                    num3 = Mathf.Clamp01(num3 / num4);
                    var num5 = num2 + num3;
                    mIgnoreCallbacks = true;
                    horizontalScrollBar.barSize = 1f - num5;
                    horizontalScrollBar.scrollValue = num5 <= 0.001f ? 0f : num2 / num5;
                    mIgnoreCallbacks = false;
                }
                if (verticalScrollBar != null && max.y > min.y)
                {
                    var vector7 = mPanel.clipRange;
                    var num6 = vector7.w * 0.5f;
                    var num7 = vector7.y - num6 - min.y;
                    var num8 = max.y - num6 - vector7.y;
                    var num9 = max.y - min.y;
                    num7 = Mathf.Clamp01(num7 / num9);
                    num8 = Mathf.Clamp01(num8 / num9);
                    var num10 = num7 + num8;
                    mIgnoreCallbacks = true;
                    verticalScrollBar.barSize = 1f - num10;
                    verticalScrollBar.scrollValue = num10 <= 0.001f ? 0f : 1f - num7 / num10;
                    mIgnoreCallbacks = false;
                }
            }
            else if (recalculateBounds)
            {
                mCalculatedBounds = false;
            }
        }
    }

    public Bounds bounds
    {
        get
        {
            if (!mCalculatedBounds)
            {
                mCalculatedBounds = true;
                mBounds = NGUIMath.CalculateRelativeWidgetBounds(mTrans, mTrans);
            }
            return mBounds;
        }
    }

    public Vector3 currentMomentum
    {
        get
        {
            return mMomentum;
        }
        set
        {
            mMomentum = value;
            mShouldMove = true;
        }
    }

    public UIPanel panel
    {
        get
        {
            return mPanel;
        }
    }

    private bool shouldMove
    {
        get
        {
            if (!disableDragIfFits)
            {
                return true;
            }
            if (mPanel == null)
            {
                mPanel = GetComponent<UIPanel>();
            }
            var clipRange = mPanel.clipRange;
            var bounds = this.bounds;
            var num = clipRange.z != 0f ? clipRange.z * 0.5f : Screen.width;
            var num2 = clipRange.w != 0f ? clipRange.w * 0.5f : Screen.height;
            if (!Mathf.Approximately(scale.x, 0f))
            {
                if (bounds.min.x < clipRange.x - num)
                {
                    return true;
                }
                if (bounds.max.x > clipRange.x + num)
                {
                    return true;
                }
            }
            if (!Mathf.Approximately(scale.y, 0f))
            {
                if (bounds.min.y < clipRange.y - num2)
                {
                    return true;
                }
                if (bounds.max.y > clipRange.y + num2)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public bool shouldMoveHorizontally
    {
        get
        {
            var x = bounds.size.x;
            if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
            {
                x += mPanel.clipSoftness.x * 2f;
            }
            return x > mPanel.clipRange.z;
        }
    }

    public bool shouldMoveVertically
    {
        get
        {
            var y = bounds.size.y;
            if (mPanel.clipping == UIDrawCall.Clipping.SoftClip)
            {
                y += mPanel.clipSoftness.y * 2f;
            }
            return y > mPanel.clipRange.w;
        }
    }

    public enum DragEffect
    {
        None,
        Momentum,
        MomentumAndSpring
    }

    public delegate void OnDragFinished();

    public enum ShowCondition
    {
        Always,
        OnlyIfNeeded,
        WhenDragging
    }
}

