using AnimationOrTween;
using UnityEngine;

public abstract class UITweener : IgnoreTimeScale
{
    public AnimationCurve animationCurve;
    public string callWhenFinished;
    public float delay;
    public float duration;
    public GameObject eventReceiver;
    public bool ignoreTimeScale;
    private float mAmountPerDelta;
    private float mDuration;
    public Method method;
    private float mFactor;
    private bool mStarted;
    private float mStartTime;
    public OnFinished onFinished;
    public bool steeperCurves;
    public Style style;
    public int tweenGroup;

    protected UITweener()
    {
        Keyframe[] keys = {new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f)};
        animationCurve = new AnimationCurve(keys);
        ignoreTimeScale = true;
        duration = 1f;
        mAmountPerDelta = 1f;
    }

    public static T Begin<T>(GameObject go, float duration) where T : UITweener
    {
        var component = go.GetComponent<T>();
        if (component == null)
        {
            component = go.AddComponent<T>();
        }

        component.mStarted = false;
        component.duration = duration;
        component.mFactor = 0f;
        component.mAmountPerDelta = Mathf.Abs(component.mAmountPerDelta);
        component.style = Style.Once;
        Keyframe[] keys = {new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f)};
        component.animationCurve = new AnimationCurve(keys);
        component.eventReceiver = null;
        component.callWhenFinished = null;
        component.onFinished = null;
        component.enabled = true;
        return component;
    }

    private float BounceLogic(float val)
    {
        if (val < 0.363636f)
        {
            val = 7.5685f * val * val;
            return val;
        }

        if (val < 0.727272f)
        {
            val = 7.5625f * (val -= 0.545454f) * val + 0.75f;
            return val;
        }

        if (val < 0.90909f)
        {
            val = 7.5625f * (val -= 0.818181f) * val + 0.9375f;
            return val;
        }

        val = 7.5625f * (val -= 0.9545454f) * val + 0.984375f;
        return val;
    }

    private void OnDisable()
    {
        mStarted = false;
    }

    protected abstract void OnUpdate(float factor, bool isFinished);

    public void Play(bool forward)
    {
        mAmountPerDelta = Mathf.Abs(amountPerDelta);
        if (!forward)
        {
            mAmountPerDelta = -mAmountPerDelta;
        }

        enabled = true;
    }

    public void Reset()
    {
        mStarted = false;
        mFactor = mAmountPerDelta >= 0f ? 0f : 1f;
        Sample(mFactor, false);
    }

    public void Sample(float factor, bool isFinished)
    {
        var f = Mathf.Clamp01(factor);
        if (method == Method.EaseIn)
        {
            f = 1f - Mathf.Sin(1.570796f * (1f - f));
            if (steeperCurves)
            {
                f *= f;
            }
        }
        else if (method == Method.EaseOut)
        {
            f = Mathf.Sin(1.570796f * f);
            if (steeperCurves)
            {
                f = 1f - f;
                f = 1f - f * f;
            }
        }
        else if (method == Method.EaseInOut)
        {
            f -= Mathf.Sin(f * 6.283185f) / 6.283185f;
            if (steeperCurves)
            {
                f = f * 2f - 1f;
                var num2 = Mathf.Sign(f);
                f = 1f - Mathf.Abs(f);
                f = 1f - f * f;
                f = num2 * f * 0.5f + 0.5f;
            }
        }
        else if (method == Method.BounceIn)
        {
            f = BounceLogic(f);
        }
        else if (method == Method.BounceOut)
        {
            f = 1f - BounceLogic(1f - f);
        }

        OnUpdate(animationCurve == null ? f : animationCurve.Evaluate(f), isFinished);
    }

    private void Start()
    {
        Update();
    }

    public void Toggle()
    {
        if (mFactor > 0f)
        {
            mAmountPerDelta = -amountPerDelta;
        }
        else
        {
            mAmountPerDelta = Mathf.Abs(amountPerDelta);
        }

        enabled = true;
    }

    private void Update()
    {
        var num = !ignoreTimeScale ? Time.deltaTime : UpdateRealTimeDelta();
        var num2 = !ignoreTimeScale ? Time.time : realTime;
        if (!mStarted)
        {
            mStarted = true;
            mStartTime = num2 + delay;
        }

        if (num2 >= mStartTime)
        {
            mFactor += amountPerDelta * num;
            if (style == Style.Loop)
            {
                if (mFactor > 1f)
                {
                    mFactor -= Mathf.Floor(mFactor);
                }
            }
            else if (style == Style.PingPong)
            {
                if (mFactor > 1f)
                {
                    mFactor = 1f - (mFactor - Mathf.Floor(mFactor));
                    mAmountPerDelta = -mAmountPerDelta;
                }
                else if (mFactor < 0f)
                {
                    mFactor = -mFactor;
                    mFactor -= Mathf.Floor(mFactor);
                    mAmountPerDelta = -mAmountPerDelta;
                }
            }

            if (style == Style.Once && (mFactor > 1f || mFactor < 0f))
            {
                mFactor = Mathf.Clamp01(mFactor);
                Sample(mFactor, true);
                onFinished?.Invoke(this);
                if (eventReceiver != null && !string.IsNullOrEmpty(callWhenFinished))
                {
                    eventReceiver.SendMessage(callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
                }

                if (mFactor == 1f && mAmountPerDelta > 0f || mFactor == 0f && mAmountPerDelta < 0f)
                {
                    enabled = false;
                }
            }
            else
            {
                Sample(mFactor, false);
            }
        }
    }

    public float amountPerDelta
    {
        get
        {
            if (mDuration != duration)
            {
                mDuration = duration;
                mAmountPerDelta = Mathf.Abs(duration <= 0f ? 1000f : 1f / duration);
            }

            return mAmountPerDelta;
        }
    }

    public Direction direction
    {
        get { return mAmountPerDelta >= 0f ? Direction.Forward : Direction.Reverse; }
    }

    public float tweenFactor
    {
        get { return mFactor; }
    }

    public enum Method
    {
        Linear,
        EaseIn,
        EaseOut,
        EaseInOut,
        BounceIn,
        BounceOut
    }

    public delegate void OnFinished(UITweener tween);

    public enum Style
    {
        Once,
        Loop,
        PingPong
    }
}