//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/Examples/Window Auto-Yaw")]
public class WindowAutoYaw : MonoBehaviour
{
    private Transform mTrans;
    public Camera uiCamera;
    public int updateOrder;
    public float yawAmount = 20f;

    private void CoroutineUpdate(float delta)
    {
        if (uiCamera != null)
        {
            var vector = uiCamera.WorldToViewportPoint(mTrans.position);
            mTrans.localRotation = Quaternion.Euler(0f, ((vector.x * 2f) - 1f) * yawAmount, 0f);
        }
    }

    private void OnDisable()
    {
        mTrans.localRotation = Quaternion.identity;
    }

    private void Start()
    {
        if (uiCamera == null)
        {
            uiCamera = NGUITools.FindCameraForLayer(gameObject.layer);
        }
        mTrans = transform;
        UpdateManager.AddCoroutine(this, updateOrder, new UpdateManager.OnUpdate(CoroutineUpdate));
    }
}

