using UnityEngine;

[AddComponentMenu("NGUI/Tween/Rotation")]
public class TweenRotation : UITweener
{
    public Vector3 from;
    private Transform mTrans;
    public Vector3 to;

    public static TweenRotation Begin(GameObject go, float duration, Quaternion rot)
    {
        var rotation = Begin<TweenRotation>(go, duration);
        rotation.from = rotation.rotation.eulerAngles;
        rotation.to = rot.eulerAngles;
        if (duration <= 0f)
        {
            rotation.Sample(1f, true);
            rotation.enabled = false;
        }

        return rotation;
    }

    protected override void OnUpdate(float factor, bool isFinished)
    {
        cachedTransform.localRotation = Quaternion.Slerp(Quaternion.Euler(from), Quaternion.Euler(to), factor);
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

    public Quaternion rotation
    {
        get { return cachedTransform.localRotation; }
        set { cachedTransform.localRotation = value; }
    }
}