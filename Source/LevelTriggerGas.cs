using UnityEngine;

public class LevelTriggerGas : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
            {
                other.gameObject.GetComponent<HERO>().fillGas();
                Destroy(gameObject);
            }
            else if (other.gameObject.GetComponent<HERO>().photonView.isMine)
            {
                other.gameObject.GetComponent<HERO>().fillGas();
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
    }
}