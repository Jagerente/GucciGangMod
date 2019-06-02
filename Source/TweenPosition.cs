using UnityEngine;

[AddComponentMenu("NGUI/Tween/Position")]
public class TweenPosition : UITweener
{
    public Vector3 from;
    private Transform mTrans;
    public Vector3 to;

    public static TweenPosition Begin(GameObject go, float duration, Vector3 pos)
    {
        var position = Begin<TweenPosition>(go, duration);
        position.from = position.position;
        position.to = pos;
        if (duration <= 0f)
        {
            position.Sample(1f, true);
            position.enabled = false;
        }
        return position;
    }

    protected override void OnUpdate(float factor, bool isFinished)
    {
        cachedTransform.localPosition = @from * (1f - factor) + to * factor;
    }

    public Transform cachedTransform
    {
        get
        {
            if (mTrans == null)
            {
                mTrans = transform;
            }
            return mTrans;
        }
    }

    public Vector3 position
    {
        get
        {
            return cachedTransform.localPosition;
        }
        set
        {
            cachedTransform.localPosition = value;
        }
    }
}

