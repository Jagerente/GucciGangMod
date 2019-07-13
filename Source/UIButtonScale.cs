using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Button Scale")]
public class UIButtonScale : MonoBehaviour
{
    public float duration = 0.2f;
    public Vector3 hover = new Vector3(1.1f, 1.1f, 1.1f);
    private bool mHighlighted;
    private Vector3 mScale;
    private bool mStarted;
    public Vector3 pressed = new Vector3(1.05f, 1.05f, 1.05f);
    public Transform tweenTarget;

    private void OnDisable()
    {
        if (mStarted && tweenTarget != null)
        {
            var component = tweenTarget.GetComponent<TweenScale>();
            if (component != null)
            {
                component.scale = mScale;
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
            TweenScale.Begin(tweenTarget.gameObject, duration, !isOver ? mScale : Vector3.Scale(mScale, hover)).method = UITweener.Method.EaseInOut;
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
            TweenScale.Begin(tweenTarget.gameObject, duration, !isPressed ? !UICamera.IsHighlighted(gameObject) ? mScale : Vector3.Scale(mScale, hover) : Vector3.Scale(mScale, pressed)).method = UITweener.Method.EaseInOut;
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
            mScale = tweenTarget.localScale;
        }
    }
}