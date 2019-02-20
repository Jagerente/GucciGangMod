//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PickupItemSimple : Photon.MonoBehaviour
{
    public bool PickupOnCollide;
    public float SecondsBeforeRespawn = 2f;
    public bool SentPickup;

    public void OnTriggerEnter(Collider other)
    {
        var component = other.GetComponent<PhotonView>();
        if (PickupOnCollide && component != null && component.isMine)
        {
            Pickup();
        }
    }

    public void Pickup()
    {
        if (!SentPickup)
        {
            SentPickup = true;
            photonView.RPC("PunPickupSimple", PhotonTargets.AllViaServer);
        }
    }

    [RPC]
    public void PunPickupSimple(PhotonMessageInfo msgInfo)
    {
        if (!SentPickup || !msgInfo.sender.isLocal || gameObject.GetActive())
        {
        }
        SentPickup = false;
        if (!gameObject.GetActive())
        {
            Debug.Log("Ignored PU RPC, cause item is inactive. " + gameObject);
        }
        else
        {
            var num = PhotonNetwork.time - msgInfo.timestamp;
            var time = SecondsBeforeRespawn - (float) num;
            if (time > 0f)
            {
                gameObject.SetActive(false);
                Invoke("RespawnAfter", time);
            }
        }
    }

    public void RespawnAfter()
    {
        if (gameObject != null)
        {
            gameObject.SetActive(true);
        }
    }
}

