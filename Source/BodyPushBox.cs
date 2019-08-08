using UnityEngine;

public class BodyPushBox : MonoBehaviour
{
    public GameObject parent;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "bodyCollider")
        {
            var component = other.gameObject.GetComponent<BodyPushBox>();
            if (component != null && component.parent != null)
            {
                float num3;
                var vector = component.parent.transform.position - parent.transform.position;
                var radius = gameObject.GetComponent<CapsuleCollider>().radius;
                var num2 = gameObject.GetComponent<CapsuleCollider>().radius;
                vector.y = 0f;
                if (vector.magnitude > 0f)
                {
                    num3 = radius + num2 - vector.magnitude;
                    vector.Normalize();
                }
                else
                {
                    num3 = radius + num2;
                    vector.x = 1f;
                }
                if (num3 < 0.1f)
                {
                }
            }
        }
    }
}