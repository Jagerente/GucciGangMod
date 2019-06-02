using UnityEngine;

[AddComponentMenu("NGUI/Tween/Spring Position")]
public class SpringPosition : IgnoreTimeScale
{
    public string callWhenFinished;
    public GameObject eventReceiver;
    public bool ignoreTimeScale;
    private float mThreshold;
    private Transform mTrans;
    public OnFinished onFinished;
    public float strength = 10f;
    public Vector3 target = Vector3.zero;
    public bool worldSpace;

    public static SpringPosition Begin(GameObject go, Vector3 pos, float strength)
    {
        var component = go.GetComponent<SpringPosition>();
        if (component == null)
        {
            component = go.AddComponent<SpringPosition>();
        }
        component.target = pos;
        component.strength = strength;
        component.onFinished = null;
        if (!component.enabled)
        {
            component.mThreshold = 0f;
            component.enabled = true;
        }
        return component;
    }

    private void Start()
    {
        mTrans = transform;
    }

    private void Update()
    {
        var deltaTime = !ignoreTimeScale ? Time.deltaTime : UpdateRealTimeDelta();
        if (worldSpace)
        {
            if (mThreshold == 0f)
            {
                var vector = target - mTrans.position;
                mThreshold = vector.magnitude * 0.001f;
            }
            mTrans.position = NGUIMath.SpringLerp(mTrans.position, target, strength, deltaTime);
            var vector2 = target - mTrans.position;
            if (mThreshold >= vector2.magnitude)
            {
                mTrans.position = target;
                onFinished?.Invoke(this);
                if (eventReceiver != null && !string.IsNullOrEmpty(callWhenFinished))
                {
                    eventReceiver.SendMessage(callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
                }
                enabled = false;
            }
        }
        else
        {
            if (mThreshold == 0f)
            {
                var vector3 = target - mTrans.localPosition;
                mThreshold = vector3.magnitude * 0.001f;
            }
            mTrans.localPosition = NGUIMath.SpringLerp(mTrans.localPosition, target, strength, deltaTime);
            var vector4 = target - mTrans.localPosition;
            if (mThreshold >= vector4.magnitude)
            {
                mTrans.localPosition = target;
                onFinished?.Invoke(this);
                if (eventReceiver != null && !string.IsNullOrEmpty(callWhenFinished))
                {
                    eventReceiver.SendMessage(callWhenFinished, this, SendMessageOptions.DontRequireReceiver);
                }
                enabled = false;
            }
        }
    }

    public delegate void OnFinished(SpringPosition spring);
}

