//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class BTN_SIGNOUT : MonoBehaviour
{
    public GameObject logincomponent;
    public GameObject loginPanel;

    private void OnClick()
    {
        NGUITools.SetActive(transform.parent.gameObject, false);
        NGUITools.SetActive(loginPanel, true);
        logincomponent.GetComponent<LoginFengKAI>().logout();
    }
}

