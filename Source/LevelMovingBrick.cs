//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class LevelMovingBrick : MonoBehaviour
{
    private Vector3 pointA;
    private Vector3 pointB;
    public GameObject pointGOA;
    public GameObject pointGOB;
    public float speed = 10f;
    public bool towardsA = true;

    private void Start()
    {
        pointA = pointGOA.transform.position;
        pointB = pointGOB.transform.position;
        Destroy(pointGOA);
        Destroy(pointGOB);
    }

    private void Update()
    {
        if (towardsA)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pointA) < 2f)
            {
                towardsA = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pointB) < 2f)
            {
                towardsA = true;
            }
        }
    }
}

