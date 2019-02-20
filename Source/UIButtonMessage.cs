//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Button Message")]
public class UIButtonMessage : MonoBehaviour
{
    public string functionName;
    public bool includeChildren;
    private bool mHighlighted;
    private bool mStarted;
    public GameObject target;
    public Trigger trigger;

    private void OnClick()
    {
        if (enabled && trigger == Trigger.OnClick)
        {
            Send();
        }
    }

    private void OnDoubleClick()
    {
        if (enabled && trigger == Trigger.OnDoubleClick)
        {
            Send();
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
            if (isOver && trigger == Trigger.OnMouseOver || !isOver && trigger == Trigger.OnMouseOut)
            {
                Send();
            }
            mHighlighted = isOver;
        }
    }

    private void OnPress(bool isPressed)
    {
        if (enabled && (isPressed && trigger == Trigger.OnPress || !isPressed && trigger == Trigger.OnRelease))
        {
            Send();
        }
    }

    private void Send()
    {
        if (!string.IsNullOrEmpty(functionName))
        {
            if (target == null)
            {
                target = gameObject;
            }
            if (includeChildren)
            {
                var componentsInChildren = target.GetComponentsInChildren<Transform>();
                var index = 0;
                var length = componentsInChildren.Length;
                while (index < length)
                {
                    var transform = componentsInChildren[index];
                    transform.gameObject.SendMessage(functionName, gameObject, SendMessageOptions.DontRequireReceiver);
                    index++;
                }
            }
            else
            {
                target.SendMessage(functionName, gameObject, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void Start()
    {
        mStarted = true;
    }

    public enum Trigger
    {
        OnClick,
        OnMouseOver,
        OnMouseOut,
        OnPress,
        OnRelease,
        OnDoubleClick
    }
}

