using UnityEngine;
using MonoBehaviour = Photon.MonoBehaviour;

[RequireComponent(typeof(PhotonView))]
public class MoveByKeys : MonoBehaviour
{
    public float speed = 10f;

    private void Start()
    {
        enabled = photonView.isMine;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            var transform = this.transform;
            transform.position += Vector3.left * (speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            var transform2 = transform;
            transform2.position += Vector3.right * (speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            var transform3 = transform;
            transform3.position += Vector3.forward * (speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            var transform4 = transform;
            transform4.position += Vector3.back * (speed * Time.deltaTime);
        }
    }
}