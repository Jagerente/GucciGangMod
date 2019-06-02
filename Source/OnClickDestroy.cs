using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

[RequireComponent(typeof(PhotonView))]
public class OnClickDestroy : MonoBehaviour
{
    public bool DestroyByRpc;

    [RPC]
    public void DestroyRpc()
    {
        Destroy(gameObject);
        PhotonNetwork.UnAllocateViewID(photonView.viewID);
    }

    private void OnClick()
    {
        if (!DestroyByRpc)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        else
        {
            photonView.RPC("DestroyRpc", PhotonTargets.AllBuffered);
        }
    }
}

