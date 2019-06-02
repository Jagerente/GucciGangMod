using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

[RequireComponent(typeof(PhotonView))]
public class OnAwakeUsePhotonView : MonoBehaviour
{
    private void Awake()
    {
        if (photonView.isMine)
        {
            photonView.RPC("OnAwakeRPC", PhotonTargets.All);
        }
    }

    [RPC]
    public void OnAwakeRPC()
    {
        Debug.Log("RPC: 'OnAwakeRPC' PhotonView: " + photonView);
    }

    [RPC]
    public void OnAwakeRPC(byte myParameter)
    {
        Debug.Log(string.Concat("RPC: 'OnAwakeRPC' Parameter: ", myParameter, " PhotonView: ", photonView));
    }

    private void Start()
    {
        if (photonView.isMine)
        {
            object[] parameters = { (byte) 1 };
            photonView.RPC("OnAwakeRPC", PhotonTargets.All, parameters);
        }
    }
}

