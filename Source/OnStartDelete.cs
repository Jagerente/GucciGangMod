using UnityEngine;

public class OnStartDelete : MonoBehaviour
{
    private void Start()
    {
        DestroyObject(gameObject);
    }
}