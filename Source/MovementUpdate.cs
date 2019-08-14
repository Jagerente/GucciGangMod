using UnityEngine;

public class MovementUpdate : MonoBehaviour
{
    public bool disabled;
    private Vector3 lastPosition;
    private Quaternion lastRotation;
    private Vector3 lastVelocity;
    private Vector3 targetPosition;

    private void Start()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            disabled = true;
            enabled = false;
        }
        else if (networkView.isMine)
        {
            object[] args = {transform.position, transform.rotation, transform.localScale, Vector3.zero};
            networkView.RPC("updateMovement", RPCMode.OthersBuffered, args);
        }
        else
        {
            targetPosition = transform.position;
        }
    }

    private void Update()
    {
        if (!disabled && Network.peerType != NetworkPeerType.Disconnected && Network.peerType != NetworkPeerType.Connecting)
        {
            if (networkView.isMine)
            {
                if (Vector3.Distance(transform.position, lastPosition) >= 0.5f)
                {
                    lastPosition = transform.position;
                    object[] args = {transform.position, transform.rotation, transform.localScale, rigidbody.velocity};
                    networkView.RPC("updateMovement", RPCMode.Others, args);
                }
                else if (Vector3.Distance(transform.rigidbody.velocity, lastVelocity) >= 0.1f)
                {
                    lastVelocity = transform.rigidbody.velocity;
                    object[] objArray2 = {transform.position, transform.rotation, transform.localScale, rigidbody.velocity};
                    networkView.RPC("updateMovement", RPCMode.Others, objArray2);
                }
                else if (Quaternion.Angle(transform.rotation, lastRotation) >= 1f)
                {
                    lastRotation = transform.rotation;
                    object[] objArray3 = {transform.position, transform.rotation, transform.localScale, rigidbody.velocity};
                    networkView.RPC("updateMovement", RPCMode.Others, objArray3);
                }
            }
            else
            {
                transform.position = Vector3.Slerp(transform.position, targetPosition, Time.deltaTime * 2f);
            }
        }
    }

    [RPC]
    private void updateMovement(Vector3 newPosition, Quaternion newRotation, Vector3 newScale, Vector3 veloctiy)
    {
        targetPosition = newPosition;
        transform.rotation = newRotation;
        transform.localScale = newScale;
        rigidbody.velocity = veloctiy;
    }
}