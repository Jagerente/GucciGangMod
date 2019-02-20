//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class RacingKillTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var gameObject = other.gameObject;
        if (gameObject.layer == 8)
        {
            gameObject = gameObject.transform.root.gameObject;
            if (((IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER) && (gameObject.GetPhotonView() != null)) && gameObject.GetPhotonView().isMine)
            {
                var component = gameObject.GetComponent<HERO>();
                if (component != null)
                {
                    component.markDie();
                    component.photonView.RPC("netDie2", PhotonTargets.All, new object[] { -1, "Server" });
                }
            }
        }
    }
}

