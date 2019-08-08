using UnityEngine;

public class Btn_to_Main_from_CC : MonoBehaviour
{
    private void OnClick()
    {
        PhotonNetwork.Disconnect();
        Screen.lockCursor = false;
        Screen.showCursor = true;
        IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
        GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().gameStart = false;
        GGM.Caching.GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = false;
        Destroy(GGM.Caching.GameObjectCache.Find("MultiplayerManager"));
        Application.LoadLevel("menu");
    }
}