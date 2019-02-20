//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class CubeCollector : MonoBehaviour
{
    public int type;

    private void Start()
    {
    }

    private void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null && Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < 8f)
        {
            Destroy(gameObject);
        }
    }
}

