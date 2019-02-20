//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class UVAffector : Affector
{
    protected float ElapsedTime;
    protected UVAnimation Frames;
    protected float UVTime;

    public UVAffector(UVAnimation frame, float time, EffectNode node) : base(node)
    {
        Frames = frame;
        UVTime = time;
    }

    public override void Reset()
    {
        ElapsedTime = 0f;
        Frames.curFrame = 0;
    }

    public override void Update()
    {
        float num;
        ElapsedTime += Time.deltaTime;
        if (UVTime <= 0f)
        {
            num = Node.GetLifeTime() / Frames.frames.Length;
        }
        else
        {
            num = UVTime / Frames.frames.Length;
        }
        if (ElapsedTime >= num)
        {
            var zero = Vector2.zero;
            var dm = Vector2.zero;
            Frames.GetNextFrame(ref zero, ref dm);
            Node.LowerLeftUV = zero;
            Node.UVDimensions = dm;
            ElapsedTime -= num;
        }
    }
}

