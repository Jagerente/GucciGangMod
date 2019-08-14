using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.IO;
using GGM.Config;
using GGM.GUI.Pages;
using UnityEngine;

namespace GGM
{
    internal class HotKey
    {
        public string Name { get; set; }
        public KeyCode DefaultKey { get; set; }
        public KeyCode Key { get; set; }

        public static List<HotKey> AllHotKeys;

        private bool Ignore = false;

        public HotKey(string name, KeyCode key)
        {
            if (AllHotKeys == null)
                AllHotKeys = new List<HotKey>();
            Name = name;
            DefaultKey = key;
            Key = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("GGM_HotKey_" + Name, DefaultKey.ToString()));
            AllHotKeys.Add(this);
        }

        public bool IsUp()
        {
            if (Ignore)
            {
                return Ignore = false;
            }
            return Input.GetKeyUp(Key);
        }

        public bool IsDown()
        {
            if (Ignore)
            {
                return Ignore = false;
            }
            return Input.GetKeyDown(Key);
        }

        public bool IsPressed()
        {
            if (Ignore)
            {
                return Ignore = false;
            }
            return Input.GetKey(Key);
        }

        public void Rebind(KeyCode key)
        {
            Ignore = true;
            Key = key;
        }

        public static void Save()
        {
            foreach (var key in AllHotKeys)
            {
                PlayerPrefs.SetString("GGM_HotKey_" + key.Name, key.Key.ToString());
            }
        }

        public static void Load()
        {
            foreach (var hotKey in AllHotKeys)
            {
                hotKey.Key = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("GGM_HotKey_" + hotKey.Name, hotKey.DefaultKey.ToString()));
            }
        }

        public static HotKey Find(KeyCode code)
        {
            HotKey res = null;
            foreach (var key in AllHotKeys)
            {
                if (key.Key == code)
                {
                    res = key;
                }
            }
            return res;
        }
    }

    internal class HotKeys : MonoBehaviour
    {
        public static HotKey Restart = new HotKey("Restart", KeyCode.G);
        public static HotKey Screenshot = new HotKey("Screenshot", KeyCode.F5);
        public static HotKey Infinites = new HotKey("Infinites", KeyCode.B);

        private void Update()
        {
            //Restarts and clears all stats.
            if (Restart.IsDown())
            {
                if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
                {
                    if (PhotonNetwork.isMasterClient)
                    {
                        foreach (var player in PhotonNetwork.playerList)
                        {
                            var stats = new Hashtable { { PhotonPlayerProperty.kills, 0 }, { PhotonPlayerProperty.deaths, 0 }, { PhotonPlayerProperty.max_dmg, 0 }, { PhotonPlayerProperty.total_dmg, 0 } };
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

            //Captures a screenshot
            if (Screenshot.IsDown())
            {
                var path = Application.dataPath + "/Screenshots";
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                Application.CaptureScreenshot(Application.dataPath + "/Screenshoots/Screenshot_" + DateTime.Now.ToString("yyyy:mm:dd:hh:mm:ss").Replace(":", "-") + ".png");
            }

            //Is your mod non-abusive? WELL YES BUT ACTUALLY NO.
            if (Infinites.IsDown())
            {
                Settings.InfiniteBulletsSetting.Value = !Settings.InfiniteBulletsSetting;
                Settings.InfiniteGasSetting.Value = !Settings.InfiniteGasSetting;
                Settings.InfiniteBladesSetting.Value = !Settings.InfiniteBladesSetting;
            }
        }
    }
}