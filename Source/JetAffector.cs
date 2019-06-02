using UnityEngine;

public class JetAffector : Affector
{
    protected float MaxAcceleration;
    protected float MinAcceleration;

    public JetAffector(float min, float max, EffectNode node) : base(node)
    {
        MinAcceleration = min;
        MaxAcceleration = max;
    }

    public override void Update()
    {
        if (Mathf.Abs(Node.Acceleration) < 1E-06)
        {
            var num = Random.Range(MinAcceleration, MaxAcceleration);
            Node.Acceleration = num;
        }
    }
}

