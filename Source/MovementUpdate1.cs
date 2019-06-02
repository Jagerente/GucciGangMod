using UnityEngine;

public class MovementUpdate1 : MonoBehaviour
{
    public bool disabled;

    private void Start()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            disabled = true;
            enabled = false;
        }
        else if (networkView.isMine)
        {
            object[] args = { transform.position, transform.rotation, transform.lossyScale };
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
            object[] args = { transform.position, transform.rotation, transform.lossyScale };
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

