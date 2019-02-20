//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/Tween/Transform")]
public class TweenTransform : UITweener
{
    public Transform from;
    private Vector3 mPos;
    private Quaternion mRot;
    private Vector3 mScale;
    private Transform mTrans;
    public bool parentWhenFinished;
    public Transform to;

    public static TweenTransform Begin(GameObject go, float duration, Transform to)
    {
        return Begin(go, duration, null, to);
    }

    public static TweenTransform Begin(GameObject go, float duration, Transform from, Transform to)
    {
        var transform = UITweener.Begin<TweenTransform>(go, duration);
        transform.from = from;
        transform.to = to;
        if (duration <= 0f)
        {
            transform.Sample(1f, true);
            transform.enabled = false;
        }
        return transform;
    }

    protected override void OnUpdate(float factor, bool isFinished)
    {
        if (to != null)
        {
            if (mTrans == null)
            {
                mTrans = transform;
                mPos = mTrans.position;
                mRot = mTrans.rotation;
                mScale = mTrans.localScale;
            }
            if (from != null)
            {
                mTrans.position = (@from.position * (1f - factor)) + (to.position * factor);
                mTrans.localScale = (@from.localScale * (1f - factor)) + (to.localScale * factor);
                mTrans.rotation = Quaternion.Slerp(from.rotation, to.rotation, factor);
            }
            else
            {
                mTrans.position = (mPos * (1f - factor)) + (to.position * factor);
                mTrans.localScale = (mScale * (1f - factor)) + (to.localScale * factor);
                mTrans.rotation = Quaternion.Slerp(mRot, to.rotation, factor);
            }
            if (parentWhenFinished && isFinished)
            {
                mTrans.parent = to;
            }
        }
    }
}

