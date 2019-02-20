//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class supplyCheck : MonoBehaviour
{
    private float elapsedTime;
    private float stepTime = 1f;

    private void Start()
    {
        if (Minimap.instance != null)
        {
            Minimap.instance.TrackGameObjectOnMinimap(gameObject, Color.white, false, true, Minimap.IconStyle.SUPPLY);
        }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > stepTime)
        {
            elapsedTime -= stepTime;
            foreach (var obj2 in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (obj2.GetComponent<HERO>() != null)
                {
                    if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                    {
                        if (Vector3.Distance(obj2.transform.position, transform.position) < 1.5f)
                        {
                            obj2.GetComponent<HERO>().getSupply();
                        }
                    }
                    else if (obj2.GetPhotonView().isMine && (Vector3.Distance(obj2.transform.position, transform.position) < 1.5f))
                    {
                        obj2.GetComponent<HERO>().getSupply();
                    }
                }
            }
        }
    }
}

