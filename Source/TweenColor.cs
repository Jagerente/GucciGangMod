//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/Tween/Color")]
public class TweenColor : UITweener
{
    public Color from = Color.white;
    private Light mLight;
    private Material mMat;
    private Transform mTrans;
    private UIWidget mWidget;
    public Color to = Color.white;

    private void Awake()
    {
        mWidget = GetComponentInChildren<UIWidget>();
        var renderer = this.renderer;
        if (renderer != null)
        {
            mMat = renderer.material;
        }
        mLight = light;
    }

    public static TweenColor Begin(GameObject go, float duration, Color color)
    {
        var color2 = UITweener.Begin<TweenColor>(go, duration);
        color2.from = color2.color;
        color2.to = color;
        if (duration <= 0f)
        {
            color2.Sample(1f, true);
            color2.enabled = false;
        }
        return color2;
    }

    protected override void OnUpdate(float factor, bool isFinished)
    {
        color = Color.Lerp(from, to, factor);
    }

    public Color color
    {
        get
        {
            if (mWidget != null)
            {
                return mWidget.color;
            }
            if (mLight != null)
            {
                return mLight.color;
            }
            if (mMat != null)
            {
                return mMat.color;
            }
            return Color.black;
        }
        set
        {
            if (mWidget != null)
            {
                mWidget.color = value;
            }
            if (mMat != null)
            {
                mMat.color = value;
            }
            if (mLight != null)
            {
                mLight.color = value;
                mLight.enabled = ((value.r + value.g) + value.b) > 0.01f;
            }
        }
    }
}

