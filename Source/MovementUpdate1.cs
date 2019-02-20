//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class MovementUpdate1 : MonoBehaviour
{
    public bool disabled;
    private Vector3 lastPosition;
    private Quaternion lastRotation;
    private Vector3 lastVelocity;

    private void Start()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            disabled = true;
            enabled = false;
        }
        else if (networkView.isMine)
        {
            var args = new object[] { transform.position, transform.rotation, transform.lossyScale };
            networkView.RPC("updateMovement1", RPCMode.OthersBuffered, args);
        }
        else
        {
            enabled = false;
        }
    }

    private void Update()
    {
        if (!disabled)
        {
            var args = new object[] { transform.position, transform.rotation, transform.lossyScale };
            networkView.RPC("updateMovement1", RPCMode.Others, args);
        }
    }

    [RPC]
    private void updateMovement1(Vector3 newPosition, Quaternion newRotation, Vector3 newScale)
    {
        transform.position = newPosition;
        transform.rotation = newRotation;
        transform.localScale = newScale;
    }
}

