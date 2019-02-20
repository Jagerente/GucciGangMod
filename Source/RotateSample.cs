//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class RotateSample : MonoBehaviour
{
    private void Start()
    {
        var args = new object[] { "x", 0.25, "easeType", "easeInOutBack", "loopType", "pingPong", "delay", 0.4 };
        iTween.RotateBy(gameObject, iTween.Hash(args));
    }
}

