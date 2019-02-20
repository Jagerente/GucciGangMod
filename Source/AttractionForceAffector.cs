//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class AttractionForceAffector : Affector
{
    private AnimationCurve AttractionCurve;
    private float Magnitude;
    protected Vector3 Position;
    private bool UseCurve;

    public AttractionForceAffector(float magnitude, Vector3 pos, EffectNode node) : base(node)
    {
        Magnitude = magnitude;
        Position = pos;
        UseCurve = false;
    }

    public AttractionForceAffector(AnimationCurve curve, Vector3 pos, EffectNode node) : base(node)
    {
        AttractionCurve = curve;
        Position = pos;
        UseCurve = true;
    }

    public override void Update()
    {
        Vector3 vector;
        float magnitude;
        if (Node.SyncClient)
        {
            vector = Position - Node.GetLocalPosition();
        }
        else
        {
            vector = Node.ClientTrans.position + Position - Node.GetLocalPosition();
        }
        var elapsedTime = Node.GetElapsedTime();
        if (UseCurve)
        {
            magnitude = AttractionCurve.Evaluate(elapsedTime);
        }
        else
        {
            magnitude = Magnitude;
        }
        var num3 = magnitude;
        Node.Velocity += vector.normalized * num3 * Time.deltaTime;
    }
}

