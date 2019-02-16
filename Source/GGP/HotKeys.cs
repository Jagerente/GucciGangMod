using System;
using UnityEngine;
using ExitGames.Client.Photon;

namespace GGP
{
    class HotKeys : MonoBehaviour
    {
        public static bool Profile = false;
        public static PhotonPlayer TPRemember;
        public static bool isPause = false;
        private void Update()
        {
            #region Stop Time
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (Time.timeScale == 1E-06f)
                    Time.timeScale = 1f;
                else
                    Time.timeScale = 1E-06f;

            }
            #endregion
            #region Infinites
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (Settings.InfiniteBlades == 0 || Settings.InfiniteBullets == 0 || Settings.InfiniteGas == 0)
                {
                    Settings.InfiniteBlades = 1;
                    Settings.InfiniteBullets = 1;
                    Settings.InfiniteGas = 1;
                }
                else if (Settings.InfiniteBlades == 1 || Settings.InfiniteBullets == 1 || Settings.InfiniteGas == 1)
                {
                    Settings.InfiniteBlades = 0;
                    Settings.InfiniteBullets = 0;
                    Settings.InfiniteGas = 0;
                }
            }
            #endregion
            #region No Clip
            if (Input.GetKeyDown(Profile ? KeyCode.N : KeyCode.V))
            {
                Settings.NoClip = Settings.NoClip == 0 ? 1 : 0;
            }
            #endregion
            #region No Gravity
            if (Input.GetKeyDown(KeyCode.F4))
            {
                Settings.NoGravity = Settings.NoGravity == 0 ? 1 : 0;
            }
            #endregion
            #region Restart
            if (Input.GetKeyDown(Profile ? KeyCode.Z : KeyCode.G))
            {
                if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.SINGLE)
                {
                    if (PhotonNetwork.isMasterClient)
                    {
                        foreach (PhotonPlayer player in PhotonNetwork.playerList)
                        {
                            var allstats = new Hashtable();
                            allstats.Add(PhotonPlayerProperty.kills, 0);
                            allstats.Add(PhotonPlayerProperty.deaths, 0);
                            allstats.Add(PhotonPlayerProperty.max_dmg, 0);
                            allstats.Add(PhotonPlayerProperty.total_dmg, 0);
                            player.SetCustomProperties(allstats);
                        }
                        FengGameManagerMKII.instance.restartRC();
                        InRoomChat.Message_3("MasterClient has restarted the game.");
                        InRoomChat.Message("MasterClient has restarted the game.");
                    }
                    else
                    {
                        InRoomChat.Message(InRoomChat.Error(0));
                        return;
                    }
                }
                else
                {
                    FengGameManagerMKII.instance.restartGameSingle();
                }
            }
            #endregion
            #region Pause
            if (Input.GetKeyDown(Profile ? KeyCode.F3 : KeyCode.P))
            {
                if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.MULTIPLAYER)
                {
                    if (PhotonNetwork.isMasterClient)
                    {
                        if (!isPause)
                        {
                            FengGameManagerMKII.instance.photonView.RPC("pauseRPC", PhotonTargets.All, true);
                            InRoomChat.Message_3("MasterClient has paused the game.");
                            InRoomChat.Message("MasterClient has paused the game.");
                            HotKeys.isPause = true;
                        }
                        else
                        {
                            FengGameManagerMKII.instance.photonView.RPC("pauseRPC", PhotonTargets.All, false);
                            InRoomChat.Message_3("MasterClient has unpaused the game.");
                            InRoomChat.Message("MasterClient has unpaused the game.");
                            HotKeys.isPause = false;
                        }
                    }
                    else
                    {
                        InRoomChat.Message(InRoomChat.Error(0));
                        return;
                    }
                }
                else if (IN_GAME_MAIN_CAMERA.gametype == GAMETYPE.SINGLE)
                {
                    if (Time.timeScale == 1f)
                    {
                        Time.timeScale = 0f;
                        GameObject.Find("MainCamera").GetComponent<MouseLook>().disable = false;
                    }
                    else
                    {
                        GameObject.Find("MainCamera").GetComponent<MouseLook>().disable = true;
                        Time.timeScale = 1f;
                    }
                }
            }
            #endregion
            #region Screenshot
            if (Input.GetKeyDown(KeyCode.F5))
            {
                Application.CaptureScreenshot(Application.dataPath + "/Screenshoots/Screenshot_" + DateTime.Now.ToString("yyyy:mm:dd:hh:mm:ss").Replace(":", "-") + ".png");
            }
            #endregion
            #region Teleport
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                RaycastHit findpoint;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out findpoint, 9999999f, Layer.GroundEnemy.value))
                {
                    var point = findpoint.point;
                    GameObject.Find("MainCamera").GetComponent<IN_GAME_MAIN_CAMERA>().main_object.transform.position = new Vector3(point.x, point.y, point.z);
                }
            }
            if (Input.GetKey(KeyCode.Alpha4))
            {
                GameObject obj = new GameObject();
                GameObject obj2 = new GameObject();
                GameObject[] objArray2 = GameObject.FindGameObjectsWithTag("Player");
                for (int num1 = 0; num1 < objArray2.Length; num1++)
                {
                    GameObject obj3 = objArray2[num1];
                    if (obj3.GetPhotonView().owner == TPRemember)
                    {
                        obj = obj3;
                    }
                    if (obj3.GetPhotonView().owner == PhotonNetwork.player)
                    {
                        obj2 = obj3;
                    }
                }
                //InRoomChat.Message("Teleported to ", TPRemember, ".");
                obj2.transform.position = obj.transform.position;
                //FengGameManagerMKII.instance.StartCoroutine(FengGameManagerMKII.instance.LoadSound(FengGameManagerMKII.SoundType.Teleport));
            }
            #endregion
            #region Object Spawn
            //if (Input.GetKeyDown(KeyCode.J))
            //{
            //    if (Settings.ObjectSpawn != null && PhotonNetwork.isMasterClient)
            //    {
            //        RaycastHit hitInfo;
            //        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 9999999f, Layer.GroundEnemy.value))
            //        {
            //            var vector = hitInfo.point;
            //            var obj = PhotonNetwork.Instantiate(Settings.ObjectSpawn, vector, new Quaternion(0, 0, 0, 0), 0);
            //            if (Settings.ObjectSpawn == "RCAsset/CannonWallProp")
            //            {
            //                var cpr = obj.GetComponent<CannonPropRegion>();
            //                string[] array = new string[7];
            //                array[0] = " photon,CannonWall,default,1,1,1,0,1,1,1,1.0,1.0,";
            //                string[] array2 = array;
            //                int num = 1;
            //                array2[num] = vector.x.ToString();
            //                array[2] = ",";
            //                string[] array3 = array;
            //                int num2 = 3;
            //                array3[num2] = vector.y.ToString();
            //                array[4] = ",";
            //                string[] array4 = array;
            //                int num3 = 5;
            //                array4[num3] = vector.z.ToString();
            //                array[6] = ",0,0,0,0";
            //                cpr.settings = string.Concat(array);
            //            }
            //            else if (Settings.ObjectSpawn == "RCAsset/CannonGroundProp")
            //            {
            //                var cpr = obj.GetComponent<CannonPropRegion>();
            //                string[] array = new string[7];
            //                array[0] = " photon,CannonGround,default,1,1,1,0,1,1,1,1.0,1.0,";
            //                string[] array2 = array;
            //                int num = 1;
            //                array2[num] = vector.x.ToString();
            //                array[2] = ",";
            //                string[] array3 = array;
            //                int num2 = 3;
            //                array3[num2] = vector.y.ToString();
            //                array[4] = ",";
            //                string[] array4 = array;
            //                int num3 = 5;
            //                array4[num3] = vector.z.ToString();
            //                array[6] = ",0,0,0,0";
            //                cpr.settings = string.Concat(array);
            //            }
            //            else if (Settings.ObjectSpawn == "horse")
            //            {
            //                if (!PhotonNetwork.player.IsDead)
            //                {
            //                    IN_GAME_MAIN_CAMERA.mainCamera.mainHERO.myHorse = obj;
            //                    IN_GAME_MAIN_CAMERA.mainCamera.mainHERO.horse = obj.GetComponent<Horse>();
            //                    obj.GetComponent<Horse>().myHero = IN_GAME_MAIN_CAMERA.mainCamera.mainHERO;
            //                    IN_GAME_MAIN_CAMERA.mainCamera.mainHERO.myHorse.GetComponent<TITAN_CONTROLLER>()
            //                    .isHorse = true;
            //                }
            //                else
            //                {
            //                    obj.GetComponent<TITAN_CONTROLLER>().isHorse = true;
            //                    if (heroes.Any())
            //                    {
            //                        obj.GetComponent<Horse>().myHero = heroes.RandomPick();
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            #endregion
            #region Titan Laugh
            //if (Input.GetKeyDown(KeyCode.Z))
            //{
            //    int titan_count = 0;
            //    foreach (GameObject titan in GameObject.FindGameObjectsWithTag("titan"))
            //    {
            //        if (titan.GetComponent<TITAN>() != null)
            //        {
            //            titan.GetComponent<TITAN>().beLaughAttacked();
            //            titan_count++;
            //        }
            //    }
            //    InRoomChat.Message(titan_count + " Titans was laughed.");
            //    //FengGameManagerMKII.instance.StartCoroutine(FengGameManagerMKII.instance.LoadSound(FengGameManagerMKII.SoundType.Joke));
            //}
            #endregion
        }
    }
}
