using ExitGames.Client.Photon;
using UnityEngine;

public class BTN_choose_human : MonoBehaviour
{
    public bool isPlayerAllDead()
    {
        var num = 0;
        var num2 = 0;
        foreach (var player in PhotonNetwork.playerList)
        {
            if ((int) player.customProperties[PhotonPlayerProperty.isTitan] == 1)
            {
                num++;
                if ((bool) player.customProperties[PhotonPlayerProperty.dead])
                {
                    num2++;
                }
            }
        }
        return num == num2;
    }

    public bool isPlayerAllDead2()
    {
        var num = 0;
        var num2 = 0;
        foreach (var player in PhotonNetwork.playerList)
        {
            if (RCextensions.returnIntFromObject(player.customProperties[PhotonPlayerProperty.isTitan]) == 1)
            {
                num++;
                if (RCextensions.returnBoolFromObject(player.customProperties[PhotonPlayerProperty.dead]))
                {
                    num2++;
                }
            }
        }
        return num == num2;
    }

    private void OnClick()
    {
        var selection = GameObject.Find("PopupListCharacterHUMAN").GetComponent<UIPopupList>().selection;
        NGUITools.SetActive(GameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[0], true);
        GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().needChooseSide = false;
        if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
        {
            GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().checkpoint = GameObject.Find("PVPchkPtH");
        }
        if (!PhotonNetwork.isMasterClient && GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().roundTime > 60f)
        {
            if (!isPlayerAllDead2())
            {
                GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().NOTSpawnPlayer(selection);
            }
            else
            {
                GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().NOTSpawnPlayer(selection);
                GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().photonView.RPC("restartGameByClient", PhotonTargets.MasterClient);
            }
        }
        else if (IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.BOSS_FIGHT_CT || IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.TROST || IN_GAME_MAIN_CAMERA.gamemode == GAMEMODE.PVP_CAPTURE)
        {
            if (isPlayerAllDead2())
            {
                GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().NOTSpawnPlayer(selection);
                GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().photonView.RPC("restartGameByClient", PhotonTargets.MasterClient);
            }
            else
            {
                GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().SpawnPlayer(selection);
            }
        }
        else
        {
            GameObject.Find("MultiplayerManager").GetComponent<FengGameManagerMKII>().SpawnPlayer(selection);
        }
        NGUITools.SetActive(GameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[1], false);
        NGUITools.SetActive(GameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[2], false);
        NGUITools.SetActive(GameObject.Find("UI_IN_GAME").GetComponent<UIReferArray>().panels[3], false);
        IN_GAME_MAIN_CAMERA.usingTitan = false;
        GameObject.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().setHUDposition();
        var hashtable = new Hashtable();
        hashtable.Add(PhotonPlayerProperty.character, selection);
        var propertiesToSet = hashtable;
        PhotonNetwork.player.SetCustomProperties(propertiesToSet);
    }
}

