using UnityEngine;

public class ParticleScaling : MonoBehaviour
{
    public void OnWillRenderObject()
    {
        GetComponent<ParticleSystem>().renderer.material.SetVector("_Center", transform.position);
        GetComponent<ParticleSystem>().renderer.material.SetVector("_Scaling", transform.lossyScale);
        GetComponent<ParticleSystem>().renderer.material.SetMatrix("_Camera", Camera.current.worldToCameraMatrix);
        GetComponent<ParticleSystem>().renderer.material
            .SetMatrix("_CameraInv", Camera.current.worldToCameraMatrix.inverse);
    }
}