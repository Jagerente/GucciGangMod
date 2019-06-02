using System.Runtime.InteropServices;
using UnityEngine;

[StructLayout(LayoutKind.Sequential)]
public struct STransform
{
    public Vector3 position;
    public Quaternion rotation;

    public void Reset()
    {
        position = Vector3.zero;
        rotation = Quaternion.identity;
    }

    public void LookAt(Vector3 target, Vector3 up)
    {
        var forward = target - position;
        rotation = Quaternion.LookRotation(forward, up);
    }
}