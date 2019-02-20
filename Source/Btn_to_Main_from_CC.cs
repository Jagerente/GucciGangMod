//Fixed With [DOGE]DEN aottg Sources fixer;
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class Btn_to_Main_from_CC : MonoBehaviour
{
    private void OnClick()
    {
        PhotonNetwork.Disconnect();
        Screen.lockCursor = false;
        Screen.showCursor = true;
        IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
        GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().gameStart = false;
        GameObject.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = false;
        Destroy(GameObject.Find("MultiplayerManager"));
        Application.LoadLevel("menu");
    }
}

