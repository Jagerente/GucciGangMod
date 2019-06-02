using AnimationOrTween;
using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Button Tween")]
public class UIButtonTween : MonoBehaviour
{
    public string callWhenFinished;
    public DisableCondition disableWhenFinished;
    public GameObject eventReceiver;
    public EnableCondition ifDisabledOnPlay;
    public bool includeChildren;
    private bool mHighlighted;
    private bool mStarted;
    private UITweener[] mTweens;
    public UITweener.OnFinished onFinished;
    public Direction playDirection = Direction.Forward;
    public bool resetOnPlay;
    public Trigger trigger;
    public int tweenGroup;
    public GameObject tweenTarget;

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

    public void Play(bool forward)
    {
        var go = tweenTarget != null ? tweenTarget : gameObject;
        if (!NGUITools.GetActive(go))
        {
            if (ifDisabledOnPlay != EnableCondition.EnableThenPlay)
            {
                return;
            }
            NGUITools.SetActive(go, true);
        }
        mTweens = !includeChildren ? go.GetComponents<UITweener>() : go.GetComponentsInChildren<UITweener>();
        if (mTweens.Length == 0)
        {
            if (disableWhenFinished != DisableCondition.DoNotDisable)
            {
                NGUITools.SetActive(tweenTarget, false);
            }
        }
        else
        {
            var flag = false;
            if (playDirection == Direction.Reverse)
            {
                forward = !forward;
            }
            var index = 0;
            var length = mTweens.Length;
            while (index < length)
            {
                var tweener = mTweens[index];
                if (tweener.tweenGroup == tweenGroup)
                {
                    if (!flag && !NGUITools.GetActive(go))
                    {
                        flag = true;
                        NGUITools.SetActive(go, true);
                    }
                    if (playDirection == Direction.Toggle)
                    {
                        tweener.Toggle();
                    }
                    else
                    {
                        tweener.Play(forward);
                    }
                    if (resetOnPlay)
                    {
                        tweener.Reset();
                    }
                    tweener.onFinished = onFinished;
                    if (eventReceiver != null && !string.IsNullOrEmpty(callWhenFinished))
                    {
                        tweener.eventReceiver = eventReceiver;
                        tweener.callWhenFinished = callWhenFinished;
                    }
                }
                index++;
            }
        }
    }

    private void Start()
    {
        mStarted = true;
        if (tweenTarget == null)
        {
            tweenTarget = gameObject;
        }
    }

    private void Update()
    {
        if (disableWhenFinished != DisableCondition.DoNotDisable && mTweens != null)
        {
            var flag = true;
            var flag2 = true;
            var index = 0;
            var length = mTweens.Length;
            while (index < length)
            {
                var tweener = mTweens[index];
                if (tweener.tweenGroup == tweenGroup)
                {
                    if (tweener.enabled)
                    {
                        flag = false;
                        break;
                    }
                    if (tweener.direction != (Direction) (int) disableWhenFinished)
                    {
                        flag2 = false;
                    }
                }
                index++;
            }
            if (flag)
            {
                if (flag2)
                {
                    NGUITools.SetActive(tweenTarget, false);
                }
                mTweens = null;
            }
        }
    }
}

