using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Drag Object")]
public class UIDragObject : IgnoreTimeScale
{
    public DragEffect dragEffect = DragEffect.MomentumAndSpring;
    private Bounds mBounds;
    private Vector3 mLastPos;
    private Vector3 mMomentum = Vector3.zero;
    public float momentumAmount = 35f;
    private UIPanel mPanel;
    private Plane mPlane;
    private bool mPressed;
    private float mScroll;
    public bool restrictWithinPanel;
    public Vector3 scale = Vector3.one;
    public float scrollWheelFactor;
    public Transform target;

    private void FindPanel()
    {
        mPanel = target == null ? null : UIPanel.Find(target.transform, false);
        if (mPanel == null)
        {
            restrictWithinPanel = false;
        }
    }

    private void LateUpdate()
    {
        var deltaTime = UpdateRealTimeDelta();
        if (target != null)
        {
            if (mPressed)
            {
                var component = target.GetComponent<SpringPosition>();
                if (component != null)
                {
                    component.enabled = false;
                }
                mScroll = 0f;
            }
            else
            {
                mMomentum += scale * (-mScroll * 0.05f);
                mScroll = NGUIMath.SpringLerp(mScroll, 0f, 20f, deltaTime);
                if (mMomentum.magnitude > 0.0001f)
                {
                    if (mPanel == null)
                    {
                        FindPanel();
                    }
                    if (mPanel != null)
                    {
                        target.position += NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
                        if (restrictWithinPanel && mPanel.clipping != UIDrawCall.Clipping.None)
                        {
                            mBounds = NGUIMath.CalculateRelativeWidgetBounds(mPanel.cachedTransform, target);
                            if (!mPanel.ConstrainTargetToBounds(target, ref mBounds, dragEffect == DragEffect.None))
                            {
                                var position2 = target.GetComponent<SpringPosition>();
                                if (position2 != null)
                                {
                                    position2.enabled = false;
                                }
                            }
                        }
                        return;
                    }
                }
                else
                {
                    mScroll = 0f;
                }
            }
            NGUIMath.SpringDampen(ref mMomentum, 9f, deltaTime);
        }
    }

    private void OnDrag(Vector2 delta)
    {
        if (enabled && NGUITools.GetActive(gameObject) && target != null)
        {
            UICamera.currentTouch.clickNotification = UICamera.ClickNotification.BasedOnDelta;
            var ray = UICamera.currentCamera.ScreenPointToRay(UICamera.currentTouch.pos);
            var enter = 0f;
            if (mPlane.Raycast(ray, out enter))
            {
                var point = ray.GetPoint(enter);
                var direction = point - mLastPos;
                mLastPos = point;
                if (direction.x != 0f || direction.y != 0f)
                {
                    direction = target.InverseTransformDirection(direction);
                    direction.Scale(scale);
                    direction = target.TransformDirection(direction);
                }
                if (dragEffect != DragEffect.None)
                {
                    mMomentum = Vector3.Lerp(mMomentum, mMomentum + direction * (0.01f * momentumAmount), 0.67f);
                }
                if (restrictWithinPanel)
                {
                    var localPosition = target.localPosition;
                    target.position += direction;
                    mBounds.center += target.localPosition - localPosition;
                    if (dragEffect != DragEffect.MomentumAndSpring && mPanel.clipping != UIDrawCall.Clipping.None && mPanel.ConstrainTargetToBounds(target, ref mBounds, true))
                    {
                        mMomentum = Vector3.zero;
                        mScroll = 0f;
                    }
                }
                else
                {
                    target.position += direction;
                }
            }
        }
    }

    private void OnPress(bool pressed)
    {
        if (enabled && NGUITools.GetActive(gameObject) && target != null)
        {
            mPressed = pressed;
            if (pressed)
            {
                if (restrictWithinPanel && mPanel == null)
                {
                    FindPanel();
                }
                if (restrictWithinPanel)
                {
                    mBounds = NGUIMath.CalculateRelativeWidgetBounds(mPanel.cachedTransform, target);
                }
                mMomentum = Vector3.zero;
                mScroll = 0f;
                var component = target.GetComponent<SpringPosition>();
                if (component != null)
                {
                    component.enabled = false;
                }
                mLastPos = UICamera.lastHit.point;
                var transform = UICamera.currentCamera.transform;
                mPlane = new Plane((mPanel == null ? transform.rotation : mPanel.cachedTransform.rotation) * Vector3.back, mLastPos);
            }
            else if (restrictWithinPanel && mPanel.clipping != UIDrawCall.Clipping.None && dragEffect == DragEffect.MomentumAndSpring)
            {
                mPanel.ConstrainTargetToBounds(target, ref mBounds, false);
            }
        }
    }

    private void OnScroll(float delta)
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

    public enum DragEffect
    {
        None,
        Momentum,
        MomentumAndSpring
    }
}