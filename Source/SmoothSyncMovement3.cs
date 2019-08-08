using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

public class SmoothSyncMovement3 : MonoBehaviour
{
    private Vector3 correctPlayerPos = Vector3.zero;
    private Quaternion correctPlayerRot = Quaternion.identity;
    public bool disabled;
    public float SmoothingDelay = 5f;

    public void Awake()
    {
        SmoothingDelay = 10f;
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            enabled = false;
        }

        correctPlayerPos = transform.position;
        correctPlayerRot = transform.rotation;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }

    public void Update()
    {
        if (!disabled && !photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * SmoothingDelay);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * SmoothingDelay);
        }
    }
}