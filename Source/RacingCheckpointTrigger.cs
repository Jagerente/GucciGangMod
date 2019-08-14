using UnityEngine;

public class RacingCheckpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var gameObject = other.gameObject;
        if (gameObject.layer == 8)
        {
            gameObject = gameObject.transform.root.gameObject;
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER && gameObject.GetPhotonView() != null && gameObject.GetPhotonView().isMine && gameObject.GetComponent<HERO>() != null)
            {
                InRoomChat.SystemMessageLocal("Checkpoint set.");
                gameObject.GetComponent<HERO>().fillGas();
                FengGameManagerMKII.FGM.racingSpawnPoint = this.gameObject.transform.position;
                FengGameManagerMKII.FGM.racingSpawnPointSet = true;
            }
        }
    }
}