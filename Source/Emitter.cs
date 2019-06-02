using UnityEngine;

public class Emitter
{
    private float EmitDelayTime;
    private float EmitLoop;
    private float EmitterElapsedTime;
    private bool IsFirstEmit = true;
    private Vector3 LastClientPos = Vector3.zero;
    public EffectLayer Layer;

    public Emitter(EffectLayer owner)
    {
        Layer = owner;
        EmitLoop = Layer.EmitLoop;
        LastClientPos = Layer.ClientTransform.position;
    }

    protected int EmitByDistance()
    {
        var vector = Layer.ClientTransform.position - LastClientPos;
        if (vector.magnitude >= Layer.DiffDistance)
        {
            LastClientPos = Layer.ClientTransform.position;
            return 1;
        }
        return 0;
    }

    protected int EmitByRate()
    {
        var num = Random.Range(0, 100);
        if (num >= 0 && num > Layer.ChanceToEmit)
        {
            return 0;
        }
        EmitDelayTime += Time.deltaTime;
        if (EmitDelayTime < Layer.EmitDelay && !IsFirstEmit)
        {
            return 0;
        }
        EmitterElapsedTime += Time.deltaTime;
        if (EmitterElapsedTime >= Layer.EmitDuration)
        {
            if (EmitLoop > 0f)
            {
                EmitLoop--;
            }
            EmitterElapsedTime = 0f;
            EmitDelayTime = 0f;
            IsFirstEmit = false;
        }
        if (EmitLoop == 0f)
        {
            return 0;
        }
        if (Layer.AvailableNodeCount == 0)
        {
            return 0;
        }
        var num2 = (int) (EmitterElapsedTime * Layer.EmitRate) - (Layer.ActiveENodes.Length - Layer.AvailableNodeCount);
        var availableNodeCount = 0;
        if (num2 > Layer.AvailableNodeCount)
        {
            availableNodeCount = Layer.AvailableNodeCount;
        }
        else
        {
            availableNodeCount = num2;
        }
        if (availableNodeCount <= 0)
        {
            return 0;
        }
        return availableNodeCount;
    }

    public Vector3 GetEmitRotation(EffectNode node)
    {
        var zero = Vector3.zero;
        if (Layer.EmitType == 2)
        {
            if (!Layer.SyncClient)
            {
                return node.Position - (Layer.ClientTransform.position + Layer.EmitPoint);
            }
            return node.Position - Layer.EmitPoint;
        }
        if (Layer.EmitType == 3)
        {
            Vector3 vector2;
            if (!Layer.SyncClient)
            {
                vector2 = node.Position - (Layer.ClientTransform.position + Layer.EmitPoint);
            }
            else
            {
                vector2 = node.Position - Layer.EmitPoint;
            }
            var toDirection = Vector3.RotateTowards(vector2, Layer.CircleDir, (90 - Layer.AngleAroundAxis) * 0.01745329f, 1f);
            return Quaternion.FromToRotation(vector2, toDirection) * vector2;
        }
        if (Layer.IsRandomDir)
        {
            var quaternion2 = Quaternion.Euler(0f, 0f, Layer.AngleAroundAxis);
            var quaternion3 = Quaternion.Euler(0f, Random.Range(0, 360), 0f);
            return Quaternion.FromToRotation(Vector3.up, Layer.OriVelocityAxis) * quaternion3 * quaternion2 * Vector3.up;
        }
        return Layer.OriVelocityAxis;
    }

    public int GetNodes()
    {
        if (Layer.IsEmitByDistance)
        {
            return EmitByDistance();
        }
        return EmitByRate();
    }

    public void Reset()
    {
        EmitterElapsedTime = 0f;
        EmitDelayTime = 0f;
        IsFirstEmit = true;
        EmitLoop = Layer.EmitLoop;
    }

    public void SetEmitPosition(EffectNode node)
    {
        var zero = Vector3.zero;
        if (Layer.EmitType == 1)
        {
            var emitPoint = Layer.EmitPoint;
            var num = Random.Range(emitPoint.x - Layer.BoxSize.x / 2f, emitPoint.x + Layer.BoxSize.x / 2f);
            var num2 = Random.Range(emitPoint.y - Layer.BoxSize.y / 2f, emitPoint.y + Layer.BoxSize.y / 2f);
            var num3 = Random.Range(emitPoint.z - Layer.BoxSize.z / 2f, emitPoint.z + Layer.BoxSize.z / 2f);
            zero.x = num;
            zero.y = num2;
            zero.z = num3;
            if (!Layer.SyncClient)
            {
                zero = Layer.ClientTransform.position + zero;
            }
        }
        else if (Layer.EmitType == 0)
        {
            zero = Layer.EmitPoint;
            if (!Layer.SyncClient)
            {
                zero = Layer.ClientTransform.position + Layer.EmitPoint;
            }
        }
        else if (Layer.EmitType == 2)
        {
            zero = Layer.EmitPoint;
            if (!Layer.SyncClient)
            {
                zero = Layer.ClientTransform.position + Layer.EmitPoint;
            }
            var vector3 = Vector3.up * Layer.Radius;
            zero = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)) * vector3 + zero;
        }
        else if (Layer.EmitType == 4)
        {
            var vector4 = Layer.EmitPoint + Layer.ClientTransform.localRotation * Vector3.forward * Layer.LineLengthLeft;
            var vector5 = Layer.EmitPoint + Layer.ClientTransform.localRotation * Vector3.forward * Layer.LineLengthRight;
            var vector6 = vector5 - vector4;
            var num4 = (node.Index + 1) / (float) Layer.MaxENodes;
            var num5 = vector6.magnitude * num4;
            zero = vector4 + vector6.normalized * num5;
            if (!Layer.SyncClient)
            {
                zero = Layer.ClientTransform.TransformPoint(zero);
            }
        }
        else if (Layer.EmitType == 3)
        {
            var num6 = (node.Index + 1) / (float) Layer.MaxENodes;
            var y = 360f * num6;
            var vector7 = Quaternion.Euler(0f, y, 0f) * (Vector3.right * Layer.Radius);
            zero = Quaternion.FromToRotation(Vector3.up, Layer.CircleDir) * vector7;
            if (!Layer.SyncClient)
            {
                zero = Layer.ClientTransform.position + zero + Layer.EmitPoint;
            }
            else
            {
                zero += Layer.EmitPoint;
            }
        }
        node.SetLocalPosition(zero);
    }
}

