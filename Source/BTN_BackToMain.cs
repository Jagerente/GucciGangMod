//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class BTN_BackToMain : MonoBehaviour
{
    private void OnClick()
    {
        NGUITools.SetActive(transform.parent.gameObject, false);
        NGUITools.SetActive(GameObject.Find("UIRefer").GetComponent<UIMainReferences>().panelMain, true);
        GameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = false;
        PhotonNetwork.Disconnect();
        GGM.RichPresence.UpdateStatus();
    }
}

