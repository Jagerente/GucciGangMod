//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class CameraForLeftEye : MonoBehaviour
{
    private Camera camera;
    private Camera cameraRightEye;
    public GameObject rightEye;

    private void LateUpdate()
    {
        camera.aspect = cameraRightEye.aspect;
        camera.fieldOfView = cameraRightEye.fieldOfView;
    }

    private void Start()
    {
        camera = GetComponent<Camera>();
        cameraRightEye = rightEye.GetComponent<Camera>();
    }
}

