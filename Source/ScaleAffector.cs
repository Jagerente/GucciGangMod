using UnityEngine;

public class ScaleAffector : Affector
{
    protected float DeltaX;
    protected float DeltaY;
    protected AnimationCurve ScaleXCurve;
    protected AnimationCurve ScaleYCurve;
    protected RSTYPE Type;

    public ScaleAffector(float x, float y, EffectNode node) : base(node)
    {
        Type = RSTYPE.SIMPLE;
        DeltaX = x;
        DeltaY = y;
    }

    public ScaleAffector(AnimationCurve curveX, AnimationCurve curveY, EffectNode node) : base(node)
    {
        Type = RSTYPE.CURVE;
        ScaleXCurve = curveX;
        ScaleYCurve = curveY;
    }

    public override void Update()
    {
        var elapsedTime = Node.GetElapsedTime();
        if (Type == RSTYPE.CURVE)
        {
            if (ScaleXCurve != null)
            {
                Node.Scale.x = ScaleXCurve.Evaluate(elapsedTime);
            }
            if (ScaleYCurve != null)
            {
                Node.Scale.y = ScaleYCurve.Evaluate(elapsedTime);
            }
        }
        else if (Type == RSTYPE.SIMPLE)
        {
            var num2 = Node.Scale.x + DeltaX * Time.deltaTime;
            var num3 = Node.Scale.y + DeltaY * Time.deltaTime;
            if (num2 * Node.Scale.x > 0f)
            {
                Node.Scale.x = num2;
            }
            if (num3 * Node.Scale.y > 0f)
            {
                Node.Scale.y = num3;
            }
        }
    }
}

