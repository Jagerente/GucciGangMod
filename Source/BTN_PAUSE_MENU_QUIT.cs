using UnityEngine;

public class BTN_PAUSE_MENU_QUIT : MonoBehaviour
{
    private void OnClick()
    {
        if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
        {
            Time.timeScale = 1f;
        }
        else
        {
            PhotonNetwork.Disconnect();
        }
        Screen.lockCursor = false;
        Screen.showCursor = true;
        IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
        GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().gameStart = false;
        GGM.Caching.GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = false;
        Destroy(GGM.Caching.GameObjectCache.Find("MultiplayerManager"));
        Application.LoadLevel("menu");
    }
}