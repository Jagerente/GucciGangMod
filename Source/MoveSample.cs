using UnityEngine;

public class MoveSample : MonoBehaviour
{
    private void Start()
    {
        object[] args = { "x", 2, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", 0.1 };
        iTween.MoveBy(gameObject, iTween.Hash(args));
    }
}