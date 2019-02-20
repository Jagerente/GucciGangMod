//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class LevelBottom : MonoBehaviour
{
    public GameObject link;
    public BottomType type;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (type == BottomType.Die)
            {
                if (other.gameObject.GetComponent<HERO>() != null)
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                    {
                        if (other.gameObject.GetPhotonView().isMine)
                        {
                            other.gameObject.GetComponent<HERO>().netDieLocal(rigidbody.velocity * 50f, false, -1, string.Empty, true);
                        }
                    }
                    else
                    {
                        other.gameObject.GetComponent<HERO>().die(other.gameObject.rigidbody.velocity * 50f, false);
                    }
                }
            }
            else if (type == BottomType.Teleport)
            {
                if (link != null)
                {
                    other.gameObject.transform.position = link.transform.position;
                }
                else
                {
                    other.gameObject.transform.position = Vector3.zero;
                }
            }
        }
    }

    private void Start()
    {
    }

    private void Update()
    {
    }
}

