using ExitGames.Client.Photon;
using UnityEngine;

public class BTN_choose_titan : MonoBehaviour
{
    private void OnClick()
    {
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_AHSS)
        {
            var id = "AHSS";
            NGUITools.SetActive(GGM.Caching.GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0], true);
            GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().needChooseSide = false;
            if (!PhotonNetwork.isMasterClient && GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().roundTime > 60f)
            {
                GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().NOTSpawnPlayer(id);
                GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().photonView.RPC("restartGameByClient", PhotonTargets.MasterClient);
            }
            else
            {
                GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().SpawnPlayer(id, "playerRespawn2");
            }
            NGUITools.SetActive(GGM.Caching.GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[1], false);
            NGUITools.SetActive(GGM.Caching.GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[2], false);
            NGUITools.SetActive(GGM.Caching.GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[3], false);
            IN_GAME_MAIN_CAMERA.usingTitan = false;
            GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().setHUDposition();
            var hashtable = new Hashtable();
            hashtable.Add(PhotonPlayerProperty.character, id);
            var propertiesToSet = hashtable;
            PhotonNetwork.player.SetCustomProperties(propertiesToSet);
        }
        else
        {
            if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
            {
                GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().checkpoint = GGM.Caching.GameObjectCache.Find("PVPchkPtT");
            }
            var selection = GGM.Caching.GameObjectCache.Find("PopupListCharacterTITAN").GetComponent<UIPopupList>().selection;
            NGUITools.SetActive(transform.parent.gameObject, false);
            NGUITools.SetActive(GGM.Caching.GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0], true);
            if (!PhotonNetwork.isMasterClient && GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().roundTime > 60f || GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().justSuicide)
            {
                GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().justSuicide = false;
                GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().NOTSpawnNonAITitan(selection);
            }
            else
            {
                GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().SpawnNonAITitan2(selection);
            }
            GGM.Caching.GameObjectCache.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().needChooseSide = false;
            NGUITools.SetActive(GGM.Caching.GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[1], false);
            NGUITools.SetActive(GGM.Caching.GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[2], false);
            NGUITools.SetActive(GGM.Caching.GameObjectCache.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[3], false);
            IN_GAME_MAIN_CAMERA.usingTitan = true;
            GGM.Caching.GameObjectCache.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().setHUDposition();
        }
    }

    private void Start()
    {
        if (!LevelInfo.getInfo(FengGameManagerMKII.level).teamTitan)
        {
            gameObject.GetComponent<UIButton>().isEnabled = false;
        }
    }
}