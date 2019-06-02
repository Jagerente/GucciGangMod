using UnityEngine;

public class VortexAffector : Affector
{
    protected Vector3 Direction;
    private float Magnitude;
    private bool UseCurve;
    private AnimationCurve VortexCurve;

    public VortexAffector(float mag, Vector3 dir, EffectNode node) : base(node)
    {
        Magnitude = mag;
        Direction = dir;
        UseCurve = false;
    }

    public VortexAffector(AnimationCurve vortexCurve, Vector3 dir, EffectNode node) : base(node)
    {
        VortexCurve = vortexCurve;
        Direction = dir;
        UseCurve = true;
    }

    public override void Update()
    {
        var rhs = Node.GetLocalPosition() - Node.Owner.EmitPoint;
        if (rhs.magnitude != 0f)
        {
            float magnitude;
            var num2 = Vector3.Dot(Direction, rhs);
            rhs -= num2 * Direction;
            var zero = Vector3.zero;
            if (rhs == Vector3.zero)
            {
                zero = rhs;
            }
            else
            {
                zero = Vector3.Cross(Direction, rhs).normalized;
            }

            var elapsedTime = Node.GetElapsedTime();
            if (UseCurve)
            {
                magnitude = VortexCurve.Evaluate(elapsedTime);
            }
            else
            {
                magnitude = Magnitude;
            }

            zero = zero * (magnitude * Time.deltaTime);
            Node.Position += zero;
        }
    }
}