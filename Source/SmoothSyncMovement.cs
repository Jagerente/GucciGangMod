using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

public class SmoothSyncMovement : MonoBehaviour
{
    public Quaternion correctCameraRot;
    private Vector3 correctPlayerPos = Vector3.zero;
    private Quaternion correctPlayerRot = Quaternion.identity;
    private Vector3 correctPlayerVelocity = Vector3.zero;
    public bool disabled;
    public bool noVelocity;
    public bool PhotonCamera;
    public float SmoothingDelay = 5f;

    public void Awake()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            enabled = false;
        }

        correctPlayerPos = transform.position;
        correctPlayerRot = transform.rotation;
        if (rigidbody == null)
        {
            noVelocity = true;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            if (!noVelocity)
            {
                stream.SendNext(rigidbody.velocity);
            }

            if (PhotonCamera)
            {
                stream.SendNext(Camera.main.transform.rotation);
            }
        }
        else
        {
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
            if (!noVelocity)
            {
                correctPlayerVelocity = (Vector3)stream.ReceiveNext();
            }

            if (PhotonCamera)
            {
                correctCameraRot = (Quaternion)stream.ReceiveNext();
            }
        }
    }

    public void Update()
    {
        if (!disabled && !photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * SmoothingDelay);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * SmoothingDelay);
            if (!noVelocity)
            {
                rigidbody.velocity = correctPlayerVelocity;
            }
        }
    }
}