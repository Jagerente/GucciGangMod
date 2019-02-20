//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using AnimationOrTween;
using System;
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
            if (string.IsNullOrEmpty(clipName))
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
            var enumerator = mAnim.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var current = (AnimationState) enumerator.Current;
                    if (string.IsNullOrEmpty(clipName) || current.name == clipName)
                    {
                        var num = Mathf.Abs(current.speed);
                        current.speed = num * (float) playDirection;
                        if (playDirection == Direction.Reverse && current.time == 0f)
                        {
                            current.time = current.length;
                        }
                        else if (playDirection == Direction.Forward && current.time == current.length)
                        {
                            current.time = 0f;
                        }
                    }
                }
            }
            finally
            {
                var disposable = enumerator as IDisposable;
                if (disposable != null)
                	disposable.Dispose();
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

    public static ActiveAnimation Play(Animation anim, string clipName, Direction playDirection, EnableCondition enableBeforePlay, DisableCondition disableCondition)
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
            var enumerator = mAnim.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    var current = (AnimationState) enumerator.Current;
                    if (mLastDirection == Direction.Reverse)
                    {
                        current.time = current.length;
                    }
                    else if (mLastDirection == Direction.Forward)
                    {
                        current.time = 0f;
                    }
                }
            }
            finally
            {
                var disposable = enumerator as IDisposable;
                if (disposable != null)
                	disposable.Dispose();
            }
        }
    }

    private void Update()
    {
        var num = UpdateRealTimeDelta();
        if (num != 0f)
        {
            if (mAnim != null)
            {
                var flag = false;
                var enumerator = mAnim.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (AnimationState) enumerator.Current;
                        if (mAnim.IsPlaying(current.name))
                        {
                            var num2 = current.speed * num;
                            current.time += num2;
                            if (num2 < 0f)
                            {
                                if (current.time > 0f)
                                {
                                    flag = true;
                                }
                                else
                                {
                                    current.time = 0f;
                                }
                            }
                            else if (current.time < current.length)
                            {
                                flag = true;
                            }
                            else
                            {
                                current.time = current.length;
                            }
                        }
                    }
                }
                finally
                {
                    var disposable = enumerator as IDisposable;
                    if (disposable != null)
                    	disposable.Dispose();
                }
                mAnim.Sample();
                if (!flag)
                {
                    enabled = false;
                    if (mNotify)
                    {
                        mNotify = false;
                        if (onFinished != null)
                        {
                            onFinished(this);
                        }
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
            }
            else
            {
                enabled = false;
            }
        }
    }

    public bool isPlaying
    {
        get
        {
            if (mAnim != null)
            {
                var enumerator = mAnim.GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        var current = (AnimationState) enumerator.Current;
                        if (mAnim.IsPlaying(current.name))
                        {
                            if (mLastDirection == Direction.Forward)
                            {
                                if (current.time < current.length)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                if (mLastDirection == Direction.Reverse)
                                {
                                    if (current.time > 0f)
                                    {
                                        return true;
                                    }
                                    continue;
                                }
                                return true;
                            }
                        }
                    }
                }
                finally
                {
                    var disposable = enumerator as IDisposable;
                    if (disposable != null)
                    	disposable.Dispose();
                }
            }
            return false;
        }
    }

    public delegate void OnFinished(ActiveAnimation anim);
}

