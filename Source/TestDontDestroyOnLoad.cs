//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class TestDontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
    }
}

