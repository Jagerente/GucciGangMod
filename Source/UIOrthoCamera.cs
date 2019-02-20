//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/UI/Orthographic Camera"), ExecuteInEditMode, RequireComponent(typeof(Camera))]
public class UIOrthoCamera : MonoBehaviour
{
    private Camera mCam;
    private Transform mTrans;

    private void Start()
    {
        mCam = camera;
        mTrans = transform;
        mCam.orthographic = true;
    }

    private void Update()
    {
        var num = mCam.rect.yMin * Screen.height;
        var num2 = mCam.rect.yMax * Screen.height;
        var b = (num2 - num) * 0.5f * mTrans.lossyScale.y;
        if (!Mathf.Approximately(mCam.orthographicSize, b))
        {
            mCam.orthographicSize = b;
        }
    }
}

