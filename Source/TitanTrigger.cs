using UnityEngine;

public class TitanTrigger : MonoBehaviour
{
    public bool isCollide;

    private void OnTriggerEnter(Collider other)
    {
        if (!isCollide)
        {
            var gameObject = other.transform.root.gameObject;
            if (gameObject.layer == 8)
            {
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    if (gameObject.GetPhotonView().isMine)
                    {
                        isCollide = true;
                    }
                }
                else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    var obj3 = Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().main_object;
                    if (obj3 != null && obj3 == gameObject)
                    {
                        isCollide = true;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isCollide)
        {
            var gameObject = other.transform.root.gameObject;
            if (gameObject.layer == 8)
            {
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    if (gameObject.GetPhotonView().isMine)
                    {
                        isCollide = false;
                    }
                }
                else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    var obj3 = Camera.main.GetComponent<IN_GAME_MAIN_CAMERA>().main_object;
                    if (obj3 != null && obj3 == gameObject)
                    {
                        isCollide = false;
                    }
                }
            }
        }
    }
}