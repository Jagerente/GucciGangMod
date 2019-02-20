//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/Examples/Pan With Mouse")]
public class PanWithMouse : IgnoreTimeScale
{
    public Vector2 degrees = new Vector2(5f, 3f);
    private Vector2 mRot = Vector2.zero;
    private Quaternion mStart;
    private Transform mTrans;
    public float range = 1f;

    private void Start()
    {
        mTrans = transform;
        mStart = mTrans.localRotation;
    }

    private void Update()
    {
        var num = UpdateRealTimeDelta();
        var mousePosition = Input.mousePosition;
        var num2 = Screen.width * 0.5f;
        var num3 = Screen.height * 0.5f;
        if (range < 0.1f)
        {
            range = 0.1f;
        }
        var x = Mathf.Clamp((mousePosition.x - num2) / num2 / range, -1f, 1f);
        var y = Mathf.Clamp((mousePosition.y - num3) / num3 / range, -1f, 1f);
        mRot = Vector2.Lerp(mRot, new Vector2(x, y), num * 5f);
        mTrans.localRotation = mStart * Quaternion.Euler(-mRot.y * degrees.y, mRot.x * degrees.x, 0f);
    }
}

