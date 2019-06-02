using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float decay;
    private float duration;
    private bool flip;
    private float R;

    private void FixedUpdate()
    {
    }

    private void shakeUpdate()
    {
        if (duration > 0f)
        {
            duration -= Time.deltaTime;
            if (flip)
            {
                var transform = gameObject.transform;
                transform.position += Vector3.up * R;
            }
            else
            {
                var transform2 = gameObject.transform;
                transform2.position -= Vector3.up * R;
            }
            flip = !flip;
            R *= decay;
        }
    }

    private void Start()
    {
    }

    public void startShake(float R, float duration,  float decay = 0.95f)
    {
        if (this.duration < duration)
        {
            this.R = R;
            this.duration = duration;
            this.decay = decay;
        }
    }

    private void Update()
    {
    }
}

