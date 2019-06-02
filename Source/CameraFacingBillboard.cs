using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour
{
    public Axis axis;
    private Camera referenceCamera;
    public bool reverseFace;

    private void Awake()
    {
        if (referenceCamera == null)
        {
            referenceCamera = Camera.main;
        }
    }

    public Vector3 GetAxis(Axis refAxis)
    {
        switch (refAxis)
        {
            case Axis.down:
                return Vector3.down;

            case Axis.left:
                return Vector3.left;

            case Axis.right:
                return Vector3.right;

            case Axis.forward:
                return Vector3.forward;

            case Axis.back:
                return Vector3.back;
        }
        return Vector3.up;
    }

    private void Update()
    {
        var worldPosition = transform.position + referenceCamera.transform.rotation * (!reverseFace ? Vector3.back : Vector3.forward);
        var worldUp = referenceCamera.transform.rotation * GetAxis(axis);
        transform.LookAt(worldPosition, worldUp);
    }

    public enum Axis
    {
        up,
        down,
        left,
        right,
        forward,
        back
    }
}

