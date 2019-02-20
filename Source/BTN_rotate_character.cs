//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class BTN_rotate_character : MonoBehaviour
{
    public GameObject camera;
    private float distance = 3f;
    public GameObject hero;
    private bool isRotate;

    private void OnPress(bool press)
    {
        if (press)
        {
            isRotate = true;
        }
        else
        {
            isRotate = false;
        }
    }

    private void Update()
    {
        distance -= Input.GetAxis("Mouse ScrollWheel") * 0.05f;
        distance = Mathf.Clamp(distance, 0.8f, 3.5f);
        camera.transform.position = hero.transform.position;
        var transform = camera.transform;
        transform.position += Vector3.up * 1.1f;
        if (isRotate)
        {
            var x = Input.GetAxis("Mouse X") * 2.5f;
            var y = -Input.GetAxis("Mouse Y") * 2.5f;
            camera.transform.RotateAround(camera.transform.position, Vector3.up, x);
            camera.transform.RotateAround(camera.transform.position, camera.transform.right, y);
        }
        var transform2 = camera.transform;
        transform2.position -= camera.transform.forward * distance;
    }
}

