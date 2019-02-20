//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/Tween/Alpha")]
public class TweenAlpha : UITweener
{
    public float from = 1f;
    private UIPanel mPanel;
    private Transform mTrans;
    private UIWidget mWidget;
    public float to = 1f;

    private void Awake()
    {
        mPanel = GetComponent<UIPanel>();
        if (mPanel == null)
        {
            mWidget = GetComponentInChildren<UIWidget>();
        }
    }

    public static TweenAlpha Begin(GameObject go, float duration, float alpha)
    {
        var alpha2 = UITweener.Begin<TweenAlpha>(go, duration);
        alpha2.from = alpha2.alpha;
        alpha2.to = alpha;
        if (duration <= 0f)
        {
            alpha2.Sample(1f, true);
            alpha2.enabled = false;
        }
        return alpha2;
    }

    protected override void OnUpdate(float factor, bool isFinished)
    {
        alpha = Mathf.Lerp(from, to, factor);
    }

    public float alpha
    {
        get
        {
            if (mWidget != null)
            {
                return mWidget.alpha;
            }
            if (mPanel != null)
            {
                return mPanel.alpha;
            }
            return 0f;
        }
        set
        {
            if (mWidget != null)
            {
                mWidget.alpha = value;
            }
            else if (mPanel != null)
            {
                mPanel.alpha = value;
            }
        }
    }
}

