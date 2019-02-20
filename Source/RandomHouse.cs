//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class RandomHouse : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = new Vector3(4f + Random.Range(0f, 4f), 4f + Random.Range(0f, 6f), 4f + Random.Range(2f, 18f));
    }

    private void Update()
    {
    }
}

