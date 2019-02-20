//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class BTN_LOAD_CC : MonoBehaviour
{
    public GameObject manager;

    private void OnClick()
    {
        manager.GetComponent<CustomCharacterManager>().LoadData();
    }
}

