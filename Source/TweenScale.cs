using UnityEngine;

[AddComponentMenu("NGUI/Tween/Scale")]
public class TweenScale : UITweener
{
    public Vector3 from = Vector3.one;
    private UITable mTable;
    private Transform mTrans;
    public Vector3 to = Vector3.one;
    public bool updateTable;

    public static TweenScale Begin(GameObject go, float duration, Vector3 scale)
    {
        var scale2 = Begin<TweenScale>(go, duration);
        scale2.from = scale2.scale;
        scale2.to = scale;
        if (duration <= 0f)
        {
            scale2.Sample(1f, true);
            scale2.enabled = false;
        }

        return scale2;
    }

    protected override void OnUpdate(float factor, bool isFinished)
    {
        cachedTransform.localScale = @from * (1f - factor) + to * factor;
        if (updateTable)
        {
            if (mTable == null)
            {
                mTable = NGUITools.FindInParents<UITable>(gameObject);
                if (mTable == null)
                {
                    updateTable = false;
                    return;
                }
            }

            mTable.repositionNow = true;
        }
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

    public Vector3 scale
    {
        get { return cachedTransform.localScale; }
        set { cachedTransform.localScale = value; }
    }
}