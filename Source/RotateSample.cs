using UnityEngine;

public class RotateSample : MonoBehaviour
{
    private void Start()
    {
        object[] args = { "x", 0.25, "easeType", "easeInOutBack", "loopType", "pingPong", "delay", 0.4 };
        iTween.RotateBy(gameObject, iTween.Hash(args));
    }
}