//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class ColorAffector : Affector
{
    protected Color[] ColorArr;
    protected float ElapsedTime;
    protected float GradualLen;
    protected bool IsNodeLife;
    protected COLOR_GRADUAL_TYPE Type;

    public ColorAffector(Color[] colorArr, float gradualLen, COLOR_GRADUAL_TYPE type, EffectNode node) : base(node)
    {
        ColorArr = colorArr;
        Type = type;
        GradualLen = gradualLen;
        if (GradualLen < 0f)
        {
            IsNodeLife = true;
        }
    }

    public override void Reset()
    {
        ElapsedTime = 0f;
    }

    public override void Update()
    {
        ElapsedTime += Time.deltaTime;
        if (IsNodeLife)
        {
            GradualLen = Node.GetLifeTime();
        }
        if (GradualLen > 0f)
        {
            if (ElapsedTime > GradualLen)
            {
                if (Type != COLOR_GRADUAL_TYPE.CLAMP)
                {
                    if (Type == COLOR_GRADUAL_TYPE.LOOP)
                    {
                        ElapsedTime = 0f;
                    }
                    else
                    {
                        var array = new Color[ColorArr.Length];
                        ColorArr.CopyTo(array, 0);
                        for (var i = 0; i < (array.Length / 2); i++)
                        {
                            ColorArr[(array.Length - i) - 1] = array[i];
                            ColorArr[i] = array[(array.Length - i) - 1];
                        }
                        ElapsedTime = 0f;
                    }
                }
            }
            else
            {
                var index = (int) ((ColorArr.Length - 1) * (ElapsedTime / GradualLen));
                if (index == (ColorArr.Length - 1))
                {
                    index--;
                }
                var num3 = index + 1;
                var num4 = GradualLen / (ColorArr.Length - 1);
                var t = (ElapsedTime - (num4 * index)) / num4;
                Node.Color = Color.Lerp(ColorArr[index], ColorArr[num3], t);
            }
        }
    }
}

