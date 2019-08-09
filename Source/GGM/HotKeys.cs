using ExitGames.Client.Photon;
using System;
using System.IO;
using GGM.Config;
using UnityEngine;

namespace GGM
{
    internal class HotKeys : MonoBehaviour
    {
        private void Update()
        {
            //Is your mod non-abusive? WELL YES BUT ACTUALLY NO.
            if (Input.GetKeyDown(KeyCode.B))
            {
                Settings.InfiniteBulletsSetting.Value = !Settings.InfiniteBulletsSetting;
                Settings.InfiniteGasSetting.Value = !Settings.InfiniteGasSetting;
                Settings.InfiniteBladesSetting.Value = !Settings.InfiniteBladesSetting;
            }

            //Restarts and clears all stats.
            if (Input.GetKeyDown(KeyCode.F3))
            {
                if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
                {
                    if (PhotonNetwork.isMasterClient)
                    {
                        foreach (var player in PhotonNetwork.playerList)
                        {
                            var stats = new Hashtable
                            {
                                {PhotonPlayerProperty.kills, 0},
                                {PhotonPlayerProperty.deaths, 0},
                                {PhotonPlayerProperty.max_dmg, 0},
                                {PhotonPlayerProperty.total_dmg, 0}
                            };
                            player.SetCustomProperties(stats);
                        }

                        FengGameManagerMKII.FGM.restartRC();
                        string[] msg = { "MasterClient ", "has restarted the game." };
                        InRoomChat.SystemMessageGlobal(msg, false);
                    }
                    else
                    {
                        InRoomChat.SystemMessageLocal(InRoomChat.Error(0));
                        return;
                    }
                }
                else
                {
                    FengGameManagerMKII.FGM.restartGameSingle();
                }
            }

            //Captures a screenshoot
            if (Input.GetKeyDown(KeyCode.F5))
            {
                var path = Application.dataPath + "/Screenshoots";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                Application.CaptureScreenshot(Application.dataPath + "/Screenshoots/Screenshot_" + DateTime.Now.ToString("yyyy:mm:dd:hh:mm:ss").Replace(":", "-") + ".png");
            }
        }
    }
}