using ExitGames.Client.Photon;
using System;
using System.IO;
using UnityEngine;

namespace GGM
{
    internal class HotKeys : MonoBehaviour
    {
        private void Update()
        {
            //Restarts and clears all stats.
            if (Input.GetKeyDown(KeyCode.G))
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

            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                IN_GAME_MAIN_CAMERA.dayLight = DayLight.Day;
                Camera.main.GetComponent<Skybox>().material = IN_GAME_MAIN_CAMERA.Instance.skyBoxDAY;
            }

            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                IN_GAME_MAIN_CAMERA.dayLight = DayLight.Dawn;
                Camera.main.GetComponent<Skybox>().material = IN_GAME_MAIN_CAMERA.Instance.skyBoxDAWN;
            }

            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                IN_GAME_MAIN_CAMERA.dayLight = DayLight.Night;
                Camera.main.GetComponent<Skybox>().material = IN_GAME_MAIN_CAMERA.Instance.skyBoxNIGHT;
            }
        }
    }
}