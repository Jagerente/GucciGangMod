using UnityEngine;

[AddComponentMenu("NGUI/Examples/Lag Position")]
public class LagPosition : MonoBehaviour
{
    public bool ignoreTimeScale;
    private Vector3 mAbsolute;
    private Vector3 mRelative;
    private Transform mTrans;
    public Vector3 speed = new Vector3(10f, 10f, 10f);
    public int updateOrder;

    private void CoroutineUpdate(float delta)
    {
        var parent = mTrans.parent;
        if (parent != null)
        {
            var vector = parent.position + parent.rotation * mRelative;
            mAbsolute.x = Mathf.Lerp(mAbsolute.x, vector.x, Mathf.Clamp01(delta * speed.x));
            mAbsolute.y = Mathf.Lerp(mAbsolute.y, vector.y, Mathf.Clamp01(delta * speed.y));
            mAbsolute.z = Mathf.Lerp(mAbsolute.z, vector.z, Mathf.Clamp01(delta * speed.z));
            mTrans.position = mAbsolute;
        }
    }

    private void OnEnable()
    {
        mTrans = transform;
        mAbsolute = mTrans.position;
    }

    private void Start()
    {
        mTrans = transform;
        mRelative = mTrans.localPosition;
        if (ignoreTimeScale)
        {
            UpdateManager.AddCoroutine(this, updateOrder, CoroutineUpdate);
        }
        else
        {
            UpdateManager.AddLateUpdate(this, updateOrder, CoroutineUpdate);
        }
    }
}

