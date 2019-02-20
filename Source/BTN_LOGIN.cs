//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class BTN_LOGIN : MonoBehaviour
{
    public GameObject logincomponent;
    public GameObject output;
    public GameObject password;
    public GameObject username;

    private void OnClick()
    {
        logincomponent.GetComponent<LoginFengKAI>().login(username.GetComponent<UIInput>().text, password.GetComponent<UIInput>().text);
        output.GetComponent<UILabel>().text = "please wait...";
    }
}

