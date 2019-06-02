using AnimationOrTween;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Button Play Animation")]
public class UIButtonPlayAnimation : MonoBehaviour
{
    public string callWhenFinished;
    public bool clearSelection;
    public string clipName;
    public DisableCondition disableWhenFinished;
    public GameObject eventReceiver;
    public EnableCondition ifDisabledOnPlay;
    private bool mHighlighted;
    private bool mStarted;
    public ActiveAnimation.OnFinished onFinished;
    public Direction playDirection = Direction.Forward;
    public bool resetOnPlay;
    public Animation target;
    public Trigger trigger;

    private void OnActivate(bool isActive)
    {
        if (enabled && (trigger == Trigger.OnActivate || trigger == Trigger.OnActivateTrue && isActive || trigger == Trigger.OnActivateFalse && !isActive))
        {
            Play(isActive);
        }
    }

    private void OnClick()
    {
        if (enabled && trigger == Trigger.OnClick)
        {
            Play(true);
        }
    }

    private void OnDoubleClick()
    {
        if (enabled && trigger == Trigger.OnDoubleClick)
        {
            Play(true);
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
            if (trigger == Trigger.OnHover || trigger == Trigger.OnHoverTrue && isOver || trigger == Trigger.OnHoverFalse && !isOver)
            {
                Play(isOver);
            }
            mHighlighted = isOver;
        }
    }

    private void OnPress(bool isPressed)
    {
        if (enabled && (trigger == Trigger.OnPress || trigger == Trigger.OnPressTrue && isPressed || trigger == Trigger.OnPressFalse && !isPressed))
        {
            Play(isPressed);
        }
    }

    private void OnSelect(bool isSelected)
    {
        if (enabled && (trigger == Trigger.OnSelect || trigger == Trigger.OnSelectTrue && isSelected || trigger == Trigger.OnSelectFalse && !isSelected))
        {
            Play(true);
        }
    }

    private void Play(bool forward)
    {
        if (target == null)
        {
            target = GetComponentInChildren<Animation>();
        }
        if (target != null)
        {
            if (clearSelection && UICamera.selectedObject == gameObject)
            {
                UICamera.selectedObject = null;
            }
            var num = -(int)this.playDirection;
            var playDirection = !forward ? (Direction) num : this.playDirection;
            var animation = ActiveAnimation.Play(target, clipName, playDirection, ifDisabledOnPlay, disableWhenFinished);
            if (animation != null)
            {
                if (resetOnPlay)
                {
                    animation.Reset();
                }
                animation.onFinished = onFinished;
                if (eventReceiver != null && !string.IsNullOrEmpty(callWhenFinished))
                {
                    animation.eventReceiver = eventReceiver;
                    animation.callWhenFinished = callWhenFinished;
                }
                else
                {
                    animation.eventReceiver = null;
                }
            }
        }
    }

    private void Start()
    {
        mStarted = true;
    }
}

