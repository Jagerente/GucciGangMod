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
        public static HotKey Pause = new HotKey("Pause", KeyCode.O);
        public static HotKey CannonSpawn = new HotKey("Cannon Spawn", KeyCode.J);
        public static HotKey CannonForward = new HotKey("Cannon Forward", KeyCode.W);
        public static HotKey CannonBackward = new HotKey("Cannon Backward", KeyCode.S);
        public static HotKey CannonTurnLeft = new HotKey("Cannon Turn Left", KeyCode.A);
        public static HotKey CannonTurnRight = new HotKey("Cannon Turn Right", KeyCode.D);
        public static HotKey CannonTurnUp = new HotKey("Cannon Turn Up", KeyCode.R);
        public static HotKey CannonTurnDown = new HotKey("Cannon Turn Up", KeyCode.F);
        public static HotKey CannonUp = new HotKey("Cannon Up", KeyCode.E);
        public static HotKey CannonDown = new HotKey("Cannon Down", KeyCode.Q);

        private void Update()
        {
            if (FengGameManagerMKII.inputManager != null)
            {
                if (FengGameManagerMKII.inputManager.menuOn)
                {
                    return;
                }
            }
            else
            {
                return;
            }

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
                Application.CaptureScreenshot(Application.dataPath + "/Screenshots/Screenshot_" + DateTime.Now.ToString("yyyy:mm:dd:hh:mm:ss").Replace(":", "-") + ".png");
            }

            //Is your mod non-abusive? WELL YES BUT ACTUALLY NO.
            if (Infinites.IsDown())
            {
                Settings.InfiniteBulletsSetting.Value = !Settings.InfiniteBulletsSetting;
                Settings.InfiniteGasSetting.Value = !Settings.InfiniteGasSetting;
                Settings.InfiniteBladesSetting.Value = !Settings.InfiniteBladesSetting;
            }

            //Pause
            if (Pause.IsDown())
            {
                switch (IN_GAME_MAIN_CAMERA.gametype)
                {
                    case GAMETYPE.MULTIPLAYER:
                        FengGameManagerMKII.FGM.SetPause(Time.timeScale == 1);
                        break;
                    case GAMETYPE.SINGLE:
                        if (Time.timeScale == 1f)
                        {
                            IN_GAME_MAIN_CAMERA.LockCamera(true);
                            Time.timeScale = 0f;
                        }
                        else
                        {
                            Time.timeScale = 1f;
                            IN_GAME_MAIN_CAMERA.LockCamera(false);
                        }
                        break;
                }
            }

            //CannonSpawn
            if (CannonSpawn.IsDown())
            {
                if (PhotonNetwork.isMasterClient)
                {
                    RaycastHit hitInfo;
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 9999999f, Layer.GroundEnemy.value))
                    {
                        var vector = hitInfo.point;
                        var objToSpawn = Settings.CannonTypeSetting == 0 ? "RCAsset/CannonGroundProp" : "RCAsset/CannonWallProp";
                        var obj = PhotonNetwork.Instantiate(objToSpawn, vector, new Quaternion(0, 0, 0, 0), 0);
                        if (objToSpawn == "RCAsset/CannonWallProp")
                        {
                            var cpr = obj.GetComponent<CannonPropRegion>();
                            string[] array = new string[7];
                            array[0] = " photon,CannonWall,default,1,1,1,0,1,1,1,1.0,1.0,";
                            string[] array2 = array;
                            int num = 1;
                            array2[num] = vector.x.ToString();
                            array[2] = ",";
                            string[] array3 = array;
                            int num2 = 3;
                            array3[num2] = vector.y.ToString();
                            array[4] = ",";
                            string[] array4 = array;
                            int num3 = 5;
                            array4[num3] = vector.z.ToString();
                            array[6] = ",0,0,0,0";
                            cpr.settings = string.Concat(array);
                        }
                        else if (objToSpawn == "RCAsset/CannonGroundProp")
                        {
                            var cpr = obj.GetComponent<CannonPropRegion>();
                            string[] array = new string[7];
                            array[0] = " photon,CannonGround,default,1,1,1,0,1,1,1,1.0,1.0,";
                            string[] array2 = array;
                            int num = 1;
                            array2[num] = vector.x.ToString();
                            array[2] = ",";
                            string[] array3 = array;
                            int num2 = 3;
                            array3[num2] = vector.y.ToString();
                            array[4] = ",";
                            string[] array4 = array;
                            int num3 = 5;
                            array4[num3] = vector.z.ToString();
                            array[6] = ",0,0,0,0";
                            cpr.settings = string.Concat(array);
                        }
                    }
                }
            }
        }
    }
}