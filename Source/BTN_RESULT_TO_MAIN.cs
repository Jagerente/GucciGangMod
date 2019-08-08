using UnityEngine;

public class BTN_RESULT_TO_MAIN : MonoBehaviour
{
    private void OnClick()
    {
        Time.timeScale = 1f;
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.Disconnect();
        }
        IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
        GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().gameStart = false;
        Screen.lockCursor = false;
        Screen.showCursor = true;
        GGM.Caching.GameObjectCache.Find("InputManagerController").GetComponent<FengCustomInputs>().menuOn = false;
        Destroy(GGM.Caching.GameObjectCache.Find("MultiplayerManager"));
        Application.LoadLevel("menu");
    }
}