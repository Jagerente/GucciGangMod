//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class OnAwakeUsePhotonView : Photon.MonoBehaviour
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
            var parameters = new object[] { (byte) 1 };
            photonView.RPC("OnAwakeRPC", PhotonTargets.All, parameters);
        }
    }
}

