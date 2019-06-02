using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class ManualPhotonViewAllocator : MonoBehaviour
{
    public GameObject Prefab;

    public void AllocateManualPhotonView()
    {
        PhotonView photonView = gameObject.GetPhotonView();
        if (photonView == null)
        {
            Debug.LogError("Can't do manual instantiation without PhotonView component.");
        }
        else
        {
            var num = PhotonNetwork.AllocateViewID();
            object[] parameters = { num };
            photonView.RPC("InstantiateRpc", PhotonTargets.AllBuffered, parameters);
        }
    }

    [RPC]
    public void InstantiateRpc(int viewID)
    {
        var go = Instantiate(Prefab, InputToEvent.inputHitPos + new Vector3(0f, 5f, 0f), Quaternion.identity) as GameObject;
        go.GetPhotonView().viewID = viewID;
        go.GetComponent<OnClickDestroy>().DestroyByRpc = true;
    }
}

