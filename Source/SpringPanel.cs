//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[RequireComponent(typeof(UIPanel)), AddComponentMenu("NGUI/Internal/Spring Panel")]
public class SpringPanel : IgnoreTimeScale
{
    private UIDraggablePanel mDrag;
    private UIPanel mPanel;
    private float mThreshold;
    private Transform mTrans;
    public OnFinished onFinished;
    public float strength = 10f;
    public Vector3 target = Vector3.zero;

    public static SpringPanel Begin(GameObject go, Vector3 pos, float strength)
    {
        var component = go.GetComponent<SpringPanel>();
        if (component == null)
        {
            component = go.AddComponent<SpringPanel>();
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
        mPanel = GetComponent<UIPanel>();
        mDrag = GetComponent<UIDraggablePanel>();
        mTrans = transform;
    }

    private void Update()
    {
        var deltaTime = UpdateRealTimeDelta();
        if (mThreshold == 0f)
        {
            var vector = this.target - mTrans.localPosition;
            mThreshold = vector.magnitude * 0.005f;
        }
        var flag = false;
        var localPosition = mTrans.localPosition;
        var target = NGUIMath.SpringLerp(mTrans.localPosition, this.target, strength, deltaTime);
        if (mThreshold >= Vector3.Magnitude(target - this.target))
        {
            target = this.target;
            enabled = false;
            flag = true;
        }
        mTrans.localPosition = target;
        var vector4 = target - localPosition;
        var clipRange = mPanel.clipRange;
        clipRange.x -= vector4.x;
        clipRange.y -= vector4.y;
        mPanel.clipRange = clipRange;
        if (mDrag != null)
        {
            mDrag.UpdateScrollbars(false);
        }
        if (flag && (onFinished != null))
        {
            onFinished();
        }
    }

    public delegate void OnFinished();
}

