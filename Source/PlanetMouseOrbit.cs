using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse Orbit")]
public class PlanetMouseOrbit : MonoBehaviour
{
    public float distance = 10f;
    public Transform target;
    private float x;
    public float xSpeed = 250f;
    private float y;
    public int yMaxLimit = 80;
    public int yMinLimit = -20;
    public float ySpeed = 120f;
    public int zoomRate = 25;

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
        {
            angle += 360f;
        }

        if (angle > 360f)
        {
            angle -= 360f;
        }

        return Mathf.Clamp(angle, min, max);
    }

    public void Main()
    {
    }

    public void Start()
    {
        var eulerAngles = transform.eulerAngles;
        x = eulerAngles.y;
        y = eulerAngles.x;
    }

    public void Update()
    {
        if (target != null)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            distance += -(Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
            y = ClampAngle(y, yMinLimit, yMaxLimit);
            var quaternion = Quaternion.Euler(y, x, 0f);
            var vector = quaternion * new Vector3(0f, 0f, -distance) + target.position;
            transform.rotation = quaternion;
            transform.position = vector;
        }
    }
}