//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PickupItem : Photon.MonoBehaviour, IPunObservable
{
    public static HashSet<PickupItem> DisabledPickupItems = new HashSet<PickupItem>();
    public MonoBehaviour OnPickedUpCall;
    public bool PickupIsMine;
    public bool PickupOnTrigger;
    public float SecondsBeforeRespawn = 2f;
    public bool SentPickup;
    public double TimeOfRespawn;

    public void Drop()
    {
        if (PickupIsMine)
        {
            photonView.RPC("PunRespawn", PhotonTargets.AllViaServer, new object[0]);
        }
    }

    public void Drop(Vector3 newPosition)
    {
        if (PickupIsMine)
        {
            var parameters = new object[] { newPosition };
            photonView.RPC("PunRespawn", PhotonTargets.AllViaServer, parameters);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting && (SecondsBeforeRespawn <= 0f))
        {
            stream.SendNext(gameObject.transform.position);
        }
        else
        {
            var vector = (Vector3) stream.ReceiveNext();
            gameObject.transform.position = vector;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        var component = other.GetComponent<PhotonView>();
        if ((PickupOnTrigger && (component != null)) && component.isMine)
        {
            Pickup();
        }
    }

    internal void PickedUp(float timeUntilRespawn)
    {
        gameObject.SetActive(false);
        DisabledPickupItems.Add(this);
        TimeOfRespawn = 0.0;
        if (timeUntilRespawn > 0f)
        {
            TimeOfRespawn = PhotonNetwork.time + timeUntilRespawn;
            Invoke("PunRespawn", timeUntilRespawn);
        }
    }

    public void Pickup()
    {
        if (!SentPickup)
        {
            SentPickup = true;
            photonView.RPC("PunPickup", PhotonTargets.AllViaServer, new object[0]);
        }
    }

    [RPC]
    public void PunPickup(PhotonMessageInfo msgInfo)
    {
        if (msgInfo.sender.isLocal)
        {
            SentPickup = false;
        }
        if (!gameObject.GetActive())
        {
            Debug.Log(string.Concat(new object[] { "Ignored PU RPC, cause item is inactive. ", gameObject, " SecondsBeforeRespawn: ", SecondsBeforeRespawn, " TimeOfRespawn: ", TimeOfRespawn, " respawn in future: ", TimeOfRespawn > PhotonNetwork.time }));
        }
        else
        {
            PickupIsMine = msgInfo.sender.isLocal;
            if (OnPickedUpCall != null)
            {
                OnPickedUpCall.SendMessage("OnPickedUp", this);
            }
            if (SecondsBeforeRespawn <= 0f)
            {
                PickedUp(0f);
            }
            else
            {
                var num = PhotonNetwork.time - msgInfo.timestamp;
                var num2 = SecondsBeforeRespawn - num;
                if (num2 > 0.0)
                {
                    PickedUp((float) num2);
                }
            }
        }
    }

    [RPC]
    internal void PunRespawn()
    {
        DisabledPickupItems.Remove(this);
        TimeOfRespawn = 0.0;
        PickupIsMine = false;
        if (gameObject != null)
        {
            gameObject.SetActive(true);
        }
    }

    [RPC]
    internal void PunRespawn(Vector3 pos)
    {
        Debug.Log("PunRespawn with Position.");
        PunRespawn();
        gameObject.transform.position = pos;
    }

    public int ViewID
    {
        get
        {
            return photonView.viewID;
        }
    }
}

