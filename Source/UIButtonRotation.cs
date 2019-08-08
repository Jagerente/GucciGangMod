using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Button Rotation")]
public class UIButtonRotation : MonoBehaviour
{
    public float duration = 0.2f;
    public Vector3 hover = Vector3.zero;
    private bool mHighlighted;
    private Quaternion mRot;
    private bool mStarted;
    public Vector3 pressed = Vector3.zero;
    public Transform tweenTarget;

    private void OnDisable()
    {
        if (mStarted && tweenTarget != null)
        {
            var component = tweenTarget.GetComponent<TweenRotation>();
            if (component != null)
            {
                component.rotation = mRot;
                component.enabled = false;
            }
        }
    }

    private void OnEnable()
    {
        if (mStarted && mHighlighted)
        {
            OnHover(UICamera.IsHighlighted(gameObject));
        }
    }

    private void OnHover(bool isOver)
    {
        if (enabled)
        {
            if (!mStarted)
            {
                Start();
            }
            TweenRotation.Begin(tweenTarget.gameObject, duration, !isOver ? mRot : mRot * Quaternion.Euler(hover)).method = UITweener.Method.EaseInOut;
            mHighlighted = isOver;
        }
    }

    private void OnPress(bool isPressed)
    {
        if (enabled)
        {
            if (!mStarted)
            {
                Start();
            }
            TweenRotation.Begin(tweenTarget.gameObject, duration, !isPressed ? !UICamera.IsHighlighted(gameObject) ? mRot : mRot * Quaternion.Euler(hover) : mRot * Quaternion.Euler(pressed)).method = UITweener.Method.EaseInOut;
        }
    }

    private void Start()
    {
        if (!mStarted)
        {
            mStarted = true;
            if (tweenTarget == null)
            {
                tweenTarget = transform;
            }
            mRot = tweenTarget.localRotation;
        }
    }
}