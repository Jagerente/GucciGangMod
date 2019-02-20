//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[RequireComponent(typeof(Camera)), AddComponentMenu("NGUI/UI/Viewport Camera"), ExecuteInEditMode]
public class UIViewport : MonoBehaviour
{
    public Transform bottomRight;
    public float fullSize = 1f;
    private Camera mCam;
    public Camera sourceCamera;
    public Transform topLeft;

    private void LateUpdate()
    {
        if ((topLeft != null) && (bottomRight != null))
        {
            var vector = sourceCamera.WorldToScreenPoint(topLeft.position);
            var vector2 = sourceCamera.WorldToScreenPoint(bottomRight.position);
            var rect = new Rect(vector.x / Screen.width, vector2.y / Screen.height, (vector2.x - vector.x) / Screen.width, (vector.y - vector2.y) / Screen.height);
            var num = fullSize * rect.height;
            if (rect != mCam.rect)
            {
                mCam.rect = rect;
            }
            if (mCam.orthographicSize != num)
            {
                mCam.orthographicSize = num;
            }
        }
    }

    private void Start()
    {
        mCam = camera;
        if (sourceCamera == null)
        {
            sourceCamera = Camera.main;
        }
    }
}

