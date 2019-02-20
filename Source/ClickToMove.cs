//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class ClickToMove : MonoBehaviour
{
    public int smooth;
    private Vector3 targetPosition;

    public void Main()
    {
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var plane = new Plane(Vector3.up, transform.position);
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var enter = 0f;
            if (plane.Raycast(ray, out enter))
            {
                var point = ray.GetPoint(enter);
                targetPosition = ray.GetPoint(enter);
                var quaternion = Quaternion.LookRotation(point - transform.position);
                transform.rotation = quaternion;
            }
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
    }
}

