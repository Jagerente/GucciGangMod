using AnimationOrTween;
using UnityEngine;

[AddComponentMenu("NGUI/Internal/Active Animation"), RequireComponent(typeof(Animation))]
public class ActiveAnimation : IgnoreTimeScale
{
    public string callWhenFinished;
    public GameObject eventReceiver;
    private Animation mAnim;


    private Direction mDisableDirection;
    private Direction mLastDirection;
    private bool mNotify;
    public OnFinished onFinished;

    private void Play(string clipName, Direction playDirection)
    {
        if (mAnim != null)
        {
            enabled = true;
            mAnim.enabled = false;
            if (playDirection == Direction.Toggle)
            {
                playDirection = mLastDirection == Direction.Forward ? Direction.Reverse : Direction.Forward;
            }

            var flag = string.IsNullOrEmpty(clipName);
            if (flag)
            {
                if (!mAnim.isPlaying)
                {
                    mAnim.Play();
                }
            }
            else if (!mAnim.IsPlaying(clipName))
            {
                mAnim.Play(clipName);
            }

            foreach (AnimationState animationState in mAnim)
            {
                if (string.IsNullOrEmpty(clipName) || animationState.name == clipName)
                {
                    var num = Mathf.Abs(animationState.speed);
                    animationState.speed = num * (float) playDirection;
                    if (playDirection == Direction.Reverse && animationState.time == 0f)
                    {
                        animationState.time = animationState.length;
                    }
                    else if (playDirection == Direction.Forward && animationState.time == animationState.length)
                    {
                        animationState.time = 0f;
                    }
                }
            }

            mLastDirection = playDirection;
            mNotify = true;
            mAnim.Sample();
        }
    }

    public static ActiveAnimation Play(Animation anim, Direction playDirection)
    {
        return Play(anim, null, playDirection, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
    }

    public static ActiveAnimation Play(Animation anim, string clipName, Direction playDirection)
    {
        return Play(anim, clipName, playDirection, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
    }

    public static ActiveAnimation Play(Animation anim, string clipName, Direction playDirection,
        EnableCondition enableBeforePlay, DisableCondition disableCondition)
    {
        if (!NGUITools.GetActive(anim.gameObject))
        {
            if (enableBeforePlay != EnableCondition.EnableThenPlay)
            {
                return null;
            }

            NGUITools.SetActive(anim.gameObject, true);
            var componentsInChildren = anim.gameObject.GetComponentsInChildren<UIPanel>();
            var index = 0;
            var length = componentsInChildren.Length;
            while (index < length)
            {
                componentsInChildren[index].Refresh();
                index++;
            }
        }

        var component = anim.GetComponent<ActiveAnimation>();
        if (component == null)
        {
            component = anim.gameObject.AddComponent<ActiveAnimation>();
        }

        component.mAnim = anim;
        component.mDisableDirection = (Direction) disableCondition;
        component.eventReceiver = null;
        component.callWhenFinished = null;
        component.onFinished = null;
        component.Play(clipName, playDirection);
        return component;
    }

    public void Reset()
    {
        if (mAnim != null)
        {
            foreach (AnimationState animationState in mAnim)
            {
                if (mLastDirection == Direction.Reverse)
                {
                    animationState.time = animationState.length;
                }
                else if (mLastDirection == Direction.Forward)
                {
                    animationState.time = 0f;
                }
            }
        }
    }


    private void Update()
    {
        var num = UpdateRealTimeDelta();
        if (num == 0f)
        {
            return;
        }

        if (mAnim != null)
        {
            var flag = false;
            foreach (AnimationState animationState in mAnim)
            {
                if (mAnim.IsPlaying(animationState.name))
                {
                    var num2 = animationState.speed * num;
                    animationState.time += num2;
                    if (num2 < 0f)
                    {
                        if (animationState.time > 0f)
                        {
                            flag = true;
                        }
                        else
                        {
                            animationState.time = 0f;
                        }
                    }
                    else if (animationState.time < animationState.length)
                    {
                        flag = true;
                    }
                    else
                    {
                        animationState.time = animationState.length;
                    }
                }
            }

            mAnim.Sample();
            if (flag)
            {
                return;
            }

            enabled = false;
            if (mNotify)
            {
                mNotify = false;
                onFinished?.Invoke(this);
                if (eventReceiver != null && !string.IsNullOrEmpty(callWhenFinished))
                {
                    eventReceiver.SendMessage(callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
                }

                if (mDisableDirection != Direction.Toggle && mLastDirection == mDisableDirection)
                {
                    NGUITools.SetActive(gameObject, false);
                }
            }
        }
        else
        {
            enabled = false;
        }
    }

    public bool isPlaying
    {
        get
        {
            if (mAnim == null)
            {
                return false;
            }

            foreach (AnimationState animationState in mAnim)
            {
                if (mAnim.IsPlaying(animationState.name))
                {
                    if (mLastDirection == Direction.Forward)
                    {
                        if (animationState.time < animationState.length)
                        {
                            var result = true;
                            return result;
                        }
                    }
                    else
                    {
                        if (mLastDirection != Direction.Reverse)
                        {
                            var result = true;
                            return result;
                        }

                        if (animationState.time > 0f)
                        {
                            var result = true;
                            return result;
                        }
                    }
                }
            }

            return false;
        }
    }


    public delegate void OnFinished(ActiveAnimation anim);
}