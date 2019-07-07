using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GGM.Caching;
using UnityEngine;
using Random = UnityEngine.Random;
using static FengGameManagerMKII;

namespace GGM.GUI.Pages
{
    public class LevelEditor : MonoBehaviour
    {
        void OnGUI()
        {
            bool flag2;
            int num13;
            int num18;
            TextEditor editor;
            int num23;
            Event current;
            bool flag4;
            string str4;
            bool flag5;
            Texture2D textured;
            bool flag6;
            int num30;
            bool flag10;
            GameObject obj4;
            float num14;
            Color color;
            Mesh mesh;
            Color[] colorArray;
            float num20;
            float num21;
            float num27;
            int num28;
            int num29;
            float num31;
            var num11 = Screen.width - 300f;
            UnityEngine.GUI.backgroundColor = new Color(0.08f, 0.3f, 0.4f, 1f);
            UnityEngine.GUI.DrawTexture(new Rect(7f, 7f, 291f, 586f), ColorCache.Blue);
            UnityEngine.GUI.DrawTexture(new Rect(num11 + 2f, 7f, 291f, 586f), ColorCache.Blue);
            flag2 = false;
            var flag3 = false;
            UnityEngine.GUI.Box(new Rect(5f, 5f, 295f, 590f), string.Empty);
            UnityEngine.GUI.Box(new Rect(num11, 5f, 295f, 590f), string.Empty);
            if (UnityEngine.GUI.Button(new Rect(10f, 10f, 60f, 25f), "Script", "box"))
            {
                settings[68] = 100;
            }

            if (UnityEngine.GUI.Button(new Rect(75f, 10f, 65f, 25f), "Controls", "box"))
            {
                settings[68] = 101;
            }

            if (UnityEngine.GUI.Button(new Rect(210f, 10f, 80f, 25f), "Full Screen", "box"))
            {
                Screen.fullScreen = !Screen.fullScreen;
                if (Screen.fullScreen)
                {
                    Screen.SetResolution(960, 600, false);
                }
                else
                {
                    Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                }
            }

            if ((int)settings[68] == 100 || (int)settings[68] == 102)
            {
                string str2;
                int num19;
                UnityEngine.GUI.Label(new Rect(115f, 40f, 100f, 20f), "Level Script:", "Label");
                UnityEngine.GUI.Label(new Rect(115f, 115f, 100f, 20f), "Import Data", "Label");
                UnityEngine.GUI.Label(new Rect(12f, 535f, 280f, 60f),
                    "Warning: your current level will be lost if you quit or import data. Make sure to save the level to a text document.",
                    "Label");
                settings[77] = UnityEngine.GUI.TextField(new Rect(10f, 140f, 285f, 350f), (string)settings[77]);
                if (UnityEngine.GUI.Button(new Rect(35f, 500f, 60f, 30f), "Apply"))
                {
                    foreach (GameObject obj2 in FindObjectsOfType(typeof(GameObject)))
                    {
                        if (obj2.name.StartsWith("custom") || obj2.name.StartsWith("base") ||
                            obj2.name.StartsWith("photon") || obj2.name.StartsWith("spawnpoint") ||
                            obj2.name.StartsWith("misc") || obj2.name.StartsWith("racing"))
                        {
                            Destroy(obj2);
                        }
                    }

                    linkHash[3].Clear();
                    settings[186] = 0;
                    var strArray = Regex.Replace((string)settings[77], @"\s+", "").Replace("\r\n", "")
                        .Replace("\n", "").Replace("\r", "").Split(';');
                    for (num13 = 0; num13 < strArray.Length; num13++)
                    {
                        var strArray2 = strArray[num13].Split(',');
                        if (strArray2[0].StartsWith("custom") || strArray2[0].StartsWith("base") ||
                            strArray2[0].StartsWith("photon") || strArray2[0].StartsWith("spawnpoint") ||
                            strArray2[0].StartsWith("misc") || strArray2[0].StartsWith("racing"))
                        {
                            float num15;
                            float num16;
                            float num17;
                            GameObject obj3 = null;
                            if (strArray2[0].StartsWith("custom"))
                            {
                                obj3 = (GameObject)Instantiate((GameObject)ResourcesCache.RCLoadGO(strArray2[1]),
                                    new Vector3(Convert.ToSingle(strArray2[12]), Convert.ToSingle(strArray2[13]),
                                        Convert.ToSingle(strArray2[14])),
                                    new Quaternion(Convert.ToSingle(strArray2[15]),
                                        Convert.ToSingle(strArray2[16]), Convert.ToSingle(strArray2[17]),
                                        Convert.ToSingle(strArray2[18])));
                            }
                            else if (strArray2[0].StartsWith("photon"))
                            {
                                if (strArray2[1].StartsWith("Cannon"))
                                {
                                    if (strArray2.Length < 15)
                                    {
                                        obj3 = (GameObject)Instantiate(
                                            (GameObject)ResourcesCache.RCLoadGO(strArray2[1] + "Prop"),
                                            new Vector3(Convert.ToSingle(strArray2[2]),
                                                Convert.ToSingle(strArray2[3]), Convert.ToSingle(strArray2[4])),
                                            new Quaternion(Convert.ToSingle(strArray2[5]),
                                                Convert.ToSingle(strArray2[6]), Convert.ToSingle(strArray2[7]),
                                                Convert.ToSingle(strArray2[8])));
                                    }
                                    else
                                    {
                                        obj3 = (GameObject)Instantiate(
                                            (GameObject)ResourcesCache.RCLoadGO(strArray2[1] + "Prop"),
                                            new Vector3(Convert.ToSingle(strArray2[12]),
                                                Convert.ToSingle(strArray2[13]), Convert.ToSingle(strArray2[14])),
                                            new Quaternion(Convert.ToSingle(strArray2[15]),
                                                Convert.ToSingle(strArray2[16]),
                                                Convert.ToSingle(strArray2[17]),
                                                Convert.ToSingle(strArray2[18])));
                                    }
                                }
                                else
                                {
                                    obj3 = (GameObject)Instantiate(
                                        (GameObject)ResourcesCache.RCLoadGO(strArray2[1]),
                                        new Vector3(Convert.ToSingle(strArray2[4]), Convert.ToSingle(strArray2[5]),
                                            Convert.ToSingle(strArray2[6])),
                                        new Quaternion(Convert.ToSingle(strArray2[7]),
                                            Convert.ToSingle(strArray2[8]), Convert.ToSingle(strArray2[9]),
                                            Convert.ToSingle(strArray2[10])));
                                }
                            }
                            else if (strArray2[0].StartsWith("spawnpoint"))
                            {
                                obj3 = (GameObject)Instantiate((GameObject)ResourcesCache.RCLoadGO(strArray2[1]),
                                    new Vector3(Convert.ToSingle(strArray2[2]), Convert.ToSingle(strArray2[3]),
                                        Convert.ToSingle(strArray2[4])),
                                    new Quaternion(Convert.ToSingle(strArray2[5]), Convert.ToSingle(strArray2[6]),
                                        Convert.ToSingle(strArray2[7]), Convert.ToSingle(strArray2[8])));
                            }
                            else if (strArray2[0].StartsWith("base"))
                            {
                                if (strArray2.Length < 15)
                                {
                                    obj3 = (GameObject)Instantiate((GameObject)Resources.Load(strArray2[1]),
                                        new Vector3(Convert.ToSingle(strArray2[2]), Convert.ToSingle(strArray2[3]),
                                            Convert.ToSingle(strArray2[4])),
                                        new Quaternion(Convert.ToSingle(strArray2[5]),
                                            Convert.ToSingle(strArray2[6]), Convert.ToSingle(strArray2[7]),
                                            Convert.ToSingle(strArray2[8])));
                                }
                                else
                                {
                                    obj3 = (GameObject)Instantiate((GameObject)Resources.Load(strArray2[1]),
                                        new Vector3(Convert.ToSingle(strArray2[12]),
                                            Convert.ToSingle(strArray2[13]), Convert.ToSingle(strArray2[14])),
                                        new Quaternion(Convert.ToSingle(strArray2[15]),
                                            Convert.ToSingle(strArray2[16]), Convert.ToSingle(strArray2[17]),
                                            Convert.ToSingle(strArray2[18])));
                                }
                            }
                            else if (strArray2[0].StartsWith("misc"))
                            {
                                if (strArray2[1].StartsWith("barrier"))
                                {
                                    obj3 = (GameObject)Instantiate(
                                        (GameObject)ResourcesCache.RCLoadGO("barrierEditor"),
                                        new Vector3(Convert.ToSingle(strArray2[5]), Convert.ToSingle(strArray2[6]),
                                            Convert.ToSingle(strArray2[7])),
                                        new Quaternion(Convert.ToSingle(strArray2[8]),
                                            Convert.ToSingle(strArray2[9]), Convert.ToSingle(strArray2[10]),
                                            Convert.ToSingle(strArray2[11])));
                                }
                                else if (strArray2[1].StartsWith("region"))
                                {
                                    obj3 = (GameObject)Instantiate(
                                        (GameObject)ResourcesCache.RCLoadGO("regionEditor"));
                                    obj3.transform.position = new Vector3(Convert.ToSingle(strArray2[6]),
                                        Convert.ToSingle(strArray2[7]), Convert.ToSingle(strArray2[8]));
                                    obj4 = (GameObject)Instantiate(Resources.Load("UI/LabelNameOverHead"));
                                    obj4.name = "RegionLabel";
                                    obj4.transform.parent = obj3.transform;
                                    num14 = 1f;
                                    if (Convert.ToSingle(strArray2[4]) > 100f)
                                    {
                                        num14 = 0.8f;
                                    }
                                    else if (Convert.ToSingle(strArray2[4]) > 1000f)
                                    {
                                        num14 = 0.5f;
                                    }

                                    obj4.transform.localPosition = new Vector3(0f, num14, 0f);
                                    obj4.transform.localScale = new Vector3(5f / Convert.ToSingle(strArray2[3]),
                                        5f / Convert.ToSingle(strArray2[4]), 5f / Convert.ToSingle(strArray2[5]));
                                    obj4.GetComponent<UILabel>().text = strArray2[2];
                                    obj3.AddComponent<RCRegionLabel>();
                                    obj3.GetComponent<RCRegionLabel>().myLabel = obj4;
                                }
                                else if (strArray2[1].StartsWith("racingStart"))
                                {
                                    obj3 = (GameObject)Instantiate(
                                        (GameObject)ResourcesCache.RCLoadGO("racingStart"),
                                        new Vector3(Convert.ToSingle(strArray2[5]), Convert.ToSingle(strArray2[6]),
                                            Convert.ToSingle(strArray2[7])),
                                        new Quaternion(Convert.ToSingle(strArray2[8]),
                                            Convert.ToSingle(strArray2[9]), Convert.ToSingle(strArray2[10]),
                                            Convert.ToSingle(strArray2[11])));
                                }
                                else if (strArray2[1].StartsWith("racingEnd"))
                                {
                                    obj3 = (GameObject)Instantiate(
                                        (GameObject)ResourcesCache.RCLoadGO("racingEnd"),
                                        new Vector3(Convert.ToSingle(strArray2[5]), Convert.ToSingle(strArray2[6]),
                                            Convert.ToSingle(strArray2[7])),
                                        new Quaternion(Convert.ToSingle(strArray2[8]),
                                            Convert.ToSingle(strArray2[9]), Convert.ToSingle(strArray2[10]),
                                            Convert.ToSingle(strArray2[11])));
                                }
                            }
                            else if (strArray2[0].StartsWith("racing"))
                            {
                                obj3 = (GameObject)Instantiate((GameObject)ResourcesCache.RCLoadGO(strArray2[1]),
                                    new Vector3(Convert.ToSingle(strArray2[5]), Convert.ToSingle(strArray2[6]),
                                        Convert.ToSingle(strArray2[7])),
                                    new Quaternion(Convert.ToSingle(strArray2[8]), Convert.ToSingle(strArray2[9]),
                                        Convert.ToSingle(strArray2[10]), Convert.ToSingle(strArray2[11])));
                            }

                            if (strArray2[2] != "default" &&
                                (strArray2[0].StartsWith("custom") ||
                                 strArray2[0].StartsWith("base") && strArray2.Length > 15 ||
                                 strArray2[0].StartsWith("photon") && strArray2.Length > 15))
                            {
                                foreach (var renderer in obj3.GetComponentsInChildren<Renderer>())
                                {
                                    if (!(renderer.name.Contains("Particle System") &&
                                          obj3.name.Contains("aot_supply")))
                                    {
                                        renderer.material = (Material)ResourcesCache.RCLoadM(strArray2[2]);
                                        renderer.material.mainTextureScale =
                                            new Vector2(
                                                renderer.material.mainTextureScale.x *
                                                Convert.ToSingle(strArray2[10]),
                                                renderer.material.mainTextureScale.y *
                                                Convert.ToSingle(strArray2[11]));
                                    }
                                }
                            }

                            if (strArray2[0].StartsWith("custom") ||
                                strArray2[0].StartsWith("base") && strArray2.Length > 15 ||
                                strArray2[0].StartsWith("photon") && strArray2.Length > 15)
                            {
                                num15 = obj3.transform.localScale.x * Convert.ToSingle(strArray2[3]);
                                num15 -= 0.001f;
                                num16 = obj3.transform.localScale.y * Convert.ToSingle(strArray2[4]);
                                num17 = obj3.transform.localScale.z * Convert.ToSingle(strArray2[5]);
                                obj3.transform.localScale = new Vector3(num15, num16, num17);
                                if (strArray2[6] != "0")
                                {
                                    color = new Color(Convert.ToSingle(strArray2[7]),
                                        Convert.ToSingle(strArray2[8]), Convert.ToSingle(strArray2[9]), 1f);
                                    foreach (var filter in obj3.GetComponentsInChildren<MeshFilter>())
                                    {
                                        mesh = filter.mesh;
                                        colorArray = new Color[mesh.vertexCount];
                                        num18 = 0;
                                        while (num18 < mesh.vertexCount)
                                        {
                                            colorArray[num18] = color;
                                            num18++;
                                        }

                                        mesh.colors = colorArray;
                                    }
                                }

                                obj3.name = strArray2[0] + "," + strArray2[1] + "," + strArray2[2] + "," +
                                            strArray2[3] + "," + strArray2[4] + "," + strArray2[5] + "," +
                                            strArray2[6] + "," + strArray2[7] + "," + strArray2[8] + "," +
                                            strArray2[9] + "," + strArray2[10] + "," + strArray2[11];
                            }
                            else if (strArray2[0].StartsWith("misc"))
                            {
                                if (strArray2[1].StartsWith("barrier") || strArray2[1].StartsWith("racing"))
                                {
                                    num15 = obj3.transform.localScale.x * Convert.ToSingle(strArray2[2]);
                                    num15 -= 0.001f;
                                    num16 = obj3.transform.localScale.y * Convert.ToSingle(strArray2[3]);
                                    num17 = obj3.transform.localScale.z * Convert.ToSingle(strArray2[4]);
                                    obj3.transform.localScale = new Vector3(num15, num16, num17);
                                    obj3.name = strArray2[0] + "," + strArray2[1] + "," + strArray2[2] + "," +
                                                strArray2[3] + "," + strArray2[4];
                                }
                                else if (strArray2[1].StartsWith("region"))
                                {
                                    num15 = obj3.transform.localScale.x * Convert.ToSingle(strArray2[3]);
                                    num15 -= 0.001f;
                                    num16 = obj3.transform.localScale.y * Convert.ToSingle(strArray2[4]);
                                    num17 = obj3.transform.localScale.z * Convert.ToSingle(strArray2[5]);
                                    obj3.transform.localScale = new Vector3(num15, num16, num17);
                                    obj3.name = strArray2[0] + "," + strArray2[1] + "," + strArray2[2] + "," +
                                                strArray2[3] + "," + strArray2[4] + "," + strArray2[5];
                                }
                            }
                            else if (strArray2[0].StartsWith("racing"))
                            {
                                num15 = obj3.transform.localScale.x * Convert.ToSingle(strArray2[2]);
                                num15 -= 0.001f;
                                num16 = obj3.transform.localScale.y * Convert.ToSingle(strArray2[3]);
                                num17 = obj3.transform.localScale.z * Convert.ToSingle(strArray2[4]);
                                obj3.transform.localScale = new Vector3(num15, num16, num17);
                                obj3.name = strArray2[0] + "," + strArray2[1] + "," + strArray2[2] + "," +
                                            strArray2[3] + "," + strArray2[4];
                            }
                            else if (!(!strArray2[0].StartsWith("photon") || strArray2[1].StartsWith("Cannon")))
                            {
                                obj3.name = strArray2[0] + "," + strArray2[1] + "," + strArray2[2] + "," +
                                            strArray2[3];
                            }
                            else
                            {
                                obj3.name = strArray2[0] + "," + strArray2[1];
                            }

                            linkHash[3].Add(obj3.GetInstanceID(), strArray[num13]);
                        }
                        else if (strArray2[0].StartsWith("map") && strArray2[1].StartsWith("disablebounds"))
                        {
                            settings[186] = 1;
                            if (!linkHash[3].ContainsKey("mapbounds"))
                            {
                                linkHash[3].Add("mapbounds", "map,disablebounds");
                            }
                        }
                    }

                    FGM.unloadAssets();
                    settings[77] = string.Empty;
                }
                else if (UnityEngine.GUI.Button(new Rect(205f, 500f, 60f, 30f), "Exit"))
                {
                    Screen.lockCursor = false;
                    Screen.showCursor = true;
                    IN_GAME_MAIN_CAMERA.gametype = GAMETYPE.STOP;
                    inputManager.menuOn = false;
                    Destroy(GameObjectCache.Find("MultiplayerManager"));
                    Application.LoadLevel("menu");
                    Destroy(GameObjectCache.Find("LevelEditor"));
                }
                else if (UnityEngine.GUI.Button(new Rect(15f, 70f, 115f, 30f), "Copy to Clipboard"))
                {
                    str2 = string.Empty;
                    num19 = 0;
                    foreach (string str3 in linkHash[3].Values)
                    {
                        num19++;
                        str2 = str2 + str3 + ";\n";
                    }

                    editor = new TextEditor
                    {
                        content = new GUIContent(str2)
                    };
                    editor.SelectAll();
                    editor.Copy();
                }
                else if (UnityEngine.GUI.Button(new Rect(175f, 70f, 115f, 30f), "View Script"))
                {
                    settings[68] = 102;
                }

                if ((int)settings[68] == 102)
                {
                    str2 = string.Empty;
                    num19 = 0;
                    foreach (string str3 in linkHash[3].Values)
                    {
                        num19++;
                        str2 = str2 + str3 + ";\n";
                    }

                    num20 = Screen.width / 2 - 110.5f;
                    num21 = Screen.height / 2 - 250f;
                    UnityEngine.GUI.DrawTexture(new Rect(num20 + 2f, num21 + 2f, 217f, 496f), ColorCache.Blue);
                    UnityEngine.GUI.Box(new Rect(num20, num21, 221f, 500f), string.Empty);
                    if (UnityEngine.GUI.Button(new Rect(num20 + 10f, num21 + 460f, 60f, 30f), "Copy"))
                    {
                        editor = new TextEditor
                        {
                            content = new GUIContent(str2)
                        };
                        editor.SelectAll();
                        editor.Copy();
                    }
                    else if (UnityEngine.GUI.Button(new Rect(num20 + 151f, num21 + 460f, 60f, 30f), "Done"))
                    {
                        settings[68] = 100;
                    }

                    UnityEngine.GUI.TextArea(new Rect(num20 + 5f, num21 + 5f, 211f, 415f), str2);
                    UnityEngine.GUI.Label(new Rect(num20 + 10f, num21 + 430f, 150f, 20f),
                        "Object Count: " + Convert.ToString(num19), "Label");
                }
            }
            else if ((int)settings[68] == 101)
            {
                UnityEngine.GUI.Label(new Rect(92f, 50f, 180f, 20f), "Level Editor Rebinds:", "Label");
                UnityEngine.GUI.Label(new Rect(12f, 80f, 145f, 20f), "Forward:", "Label");
                UnityEngine.GUI.Label(new Rect(12f, 105f, 145f, 20f), "Back:", "Label");
                UnityEngine.GUI.Label(new Rect(12f, 130f, 145f, 20f), "Left:", "Label");
                UnityEngine.GUI.Label(new Rect(12f, 155f, 145f, 20f), "Right:", "Label");
                UnityEngine.GUI.Label(new Rect(12f, 180f, 145f, 20f), "Up:", "Label");
                UnityEngine.GUI.Label(new Rect(12f, 205f, 145f, 20f), "Down:", "Label");
                UnityEngine.GUI.Label(new Rect(12f, 230f, 145f, 20f), "Toggle Cursor:", "Label");
                UnityEngine.GUI.Label(new Rect(12f, 255f, 145f, 20f), "Place Object:", "Label");
                UnityEngine.GUI.Label(new Rect(12f, 280f, 145f, 20f), "Delete Object:", "Label");
                UnityEngine.GUI.Label(new Rect(12f, 305f, 145f, 20f), "Movement-Slow:", "Label");
                UnityEngine.GUI.Label(new Rect(12f, 330f, 145f, 20f), "Rotate Forward:", "Label");
                UnityEngine.GUI.Label(new Rect(12f, 355f, 145f, 20f), "Rotate Backward:", "Label");
                UnityEngine.GUI.Label(new Rect(12f, 380f, 145f, 20f), "Rotate Left:", "Label");
                UnityEngine.GUI.Label(new Rect(12f, 405f, 145f, 20f), "Rotate Right:", "Label");
                UnityEngine.GUI.Label(new Rect(12f, 430f, 145f, 20f), "Rotate CCW:", "Label");
                UnityEngine.GUI.Label(new Rect(12f, 455f, 145f, 20f), "Rotate CW:", "Label");
                UnityEngine.GUI.Label(new Rect(12f, 480f, 145f, 20f), "Movement-Speedup:", "Label");
                for (num13 = 0; num13 < 17; num13++)
                {
                    var num22 = 80f + 25f * num13;
                    num23 = 117 + num13;
                    if (num13 == 16)
                    {
                        num23 = 161;
                    }

                    if (UnityEngine.GUI.Button(new Rect(135f, num22, 60f, 20f), (string)settings[num23], "box"))
                    {
                        settings[num23] = "waiting...";
                        settings[100] = num23;
                    }
                }

                if ((int)settings[100] != 0)
                {
                    current = Event.current;
                    flag4 = false;
                    str4 = "waiting...";
                    if (current.type == EventType.KeyDown && current.keyCode != KeyCode.None)
                    {
                        flag4 = true;
                        str4 = current.keyCode.ToString();
                    }
                    else if (Input.GetKey(KeyCode.LeftShift))
                    {
                        flag4 = true;
                        str4 = KeyCode.LeftShift.ToString();
                    }
                    else if (Input.GetKey(KeyCode.RightShift))
                    {
                        flag4 = true;
                        str4 = KeyCode.RightShift.ToString();
                    }
                    else if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                    {
                        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
                        {
                            flag4 = true;
                            str4 = "Scroll Up";
                        }
                        else
                        {
                            flag4 = true;
                            str4 = "Scroll Down";
                        }
                    }
                    else
                    {
                        num13 = 0;
                        while (num13 < 7)
                        {
                            if (Input.GetKeyDown((KeyCode)(323 + num13)))
                            {
                                flag4 = true;
                                str4 = "Mouse" + Convert.ToString(num13);
                            }

                            num13++;
                        }
                    }

                    if (flag4)
                    {
                        for (num13 = 0; num13 < 17; num13++)
                        {
                            num23 = 117 + num13;
                            if (num13 == 16)
                            {
                                num23 = 161;
                            }

                            if ((int)settings[100] == num23)
                            {
                                settings[num23] = str4;
                                settings[100] = 0;
                                inputRC.setInputLevel(num13, str4);
                            }
                        }
                    }
                }

                if (UnityEngine.GUI.Button(new Rect(100f, 515f, 110f, 30f), "Save Controls"))
                {
                    PlayerPrefs.SetString("lforward", (string)settings[117]);
                    PlayerPrefs.SetString("lback", (string)settings[118]);
                    PlayerPrefs.SetString("lleft", (string)settings[119]);
                    PlayerPrefs.SetString("lright", (string)settings[120]);
                    PlayerPrefs.SetString("lup", (string)settings[121]);
                    PlayerPrefs.SetString("ldown", (string)settings[122]);
                    PlayerPrefs.SetString("lcursor", (string)settings[123]);
                    PlayerPrefs.SetString("lplace", (string)settings[124]);
                    PlayerPrefs.SetString("ldel", (string)settings[125]);
                    PlayerPrefs.SetString("lslow", (string)settings[126]);
                    PlayerPrefs.SetString("lrforward", (string)settings[127]);
                    PlayerPrefs.SetString("lrback", (string)settings[128]);
                    PlayerPrefs.SetString("lrleft", (string)settings[129]);
                    PlayerPrefs.SetString("lrright", (string)settings[130]);
                    PlayerPrefs.SetString("lrccw", (string)settings[131]);
                    PlayerPrefs.SetString("lrcw", (string)settings[132]);
                    PlayerPrefs.SetString("lfast", (string)settings[161]);
                }
            }

            if ((int)settings[64] != 105 && (int)settings[64] != 106)
            {
                UnityEngine.GUI.Label(new Rect(num11 + 13f, 445f, 125f, 20f), "Scale Multipliers:", "Label");
                UnityEngine.GUI.Label(new Rect(num11 + 13f, 470f, 50f, 22f), "Length:", "Label");
                settings[72] = UnityEngine.GUI.TextField(new Rect(num11 + 58f, 470f, 40f, 20f), (string)settings[72]);
                UnityEngine.GUI.Label(new Rect(num11 + 13f, 495f, 50f, 20f), "Width:", "Label");
                settings[70] = UnityEngine.GUI.TextField(new Rect(num11 + 58f, 495f, 40f, 20f), (string)settings[70]);
                UnityEngine.GUI.Label(new Rect(num11 + 13f, 520f, 50f, 22f), "Height:", "Label");
                settings[71] = UnityEngine.GUI.TextField(new Rect(num11 + 58f, 520f, 40f, 20f), (string)settings[71]);
                if ((int)settings[64] <= 106)
                {
                    UnityEngine.GUI.Label(new Rect(num11 + 155f, 554f, 50f, 22f), "Tiling:", "Label");
                    settings[79] = UnityEngine.GUI.TextField(new Rect(num11 + 200f, 554f, 40f, 20f), (string)settings[79]);
                    settings[80] = UnityEngine.GUI.TextField(new Rect(num11 + 245f, 554f, 40f, 20f), (string)settings[80]);
                    UnityEngine.GUI.Label(new Rect(num11 + 219f, 570f, 10f, 22f), "x:", "Label");
                    UnityEngine.GUI.Label(new Rect(num11 + 264f, 570f, 10f, 22f), "y:", "Label");
                    UnityEngine.GUI.Label(new Rect(num11 + 155f, 445f, 50f, 20f), "Color:", "Label");
                    UnityEngine.GUI.Label(new Rect(num11 + 155f, 470f, 10f, 20f), "R:", "Label");
                    UnityEngine.GUI.Label(new Rect(num11 + 155f, 495f, 10f, 20f), "G:", "Label");
                    UnityEngine.GUI.Label(new Rect(num11 + 155f, 520f, 10f, 20f), "B:", "Label");
                    settings[73] = UnityEngine.GUI.HorizontalSlider(new Rect(num11 + 170f, 475f, 100f, 20f),
                        (float)settings[73], 0f, 1f);
                    settings[74] = UnityEngine.GUI.HorizontalSlider(new Rect(num11 + 170f, 500f, 100f, 20f),
                        (float)settings[74], 0f, 1f);
                    settings[75] = UnityEngine.GUI.HorizontalSlider(new Rect(num11 + 170f, 525f, 100f, 20f),
                        (float)settings[75], 0f, 1f);
                    UnityEngine.GUI.Label(new Rect(num11 + 13f, 554f, 57f, 22f), "Material:", "Label");
                    if (UnityEngine.GUI.Button(new Rect(num11 + 66f, 554f, 60f, 20f), (string)settings[69]))
                    {
                        settings[78] = 1;
                    }

                    if ((int)settings[78] == 1)
                    {
                        string[] strArray4 = { "bark", "bark2", "bark3", "bark4" };
                        string[] strArray5 = { "wood1", "wood2", "wood3", "wood4" };
                        string[] strArray6 = { "grass", "grass2", "grass3", "grass4" };
                        string[] strArray7 = { "brick1", "brick2", "brick3", "brick4" };
                        string[] strArray8 = { "metal1", "metal2", "metal3", "metal4" };
                        string[] strArray9 = { "rock1", "rock2", "rock3" };
                        string[] strArray10 =
                        {
                                    "stone1", "stone2", "stone3", "stone4", "stone5", "stone6", "stone7", "stone8",
                                    "stone9", "stone10"
                                };
                        string[] strArray11 =
                            {"earth1", "earth2", "ice1", "lava1", "crystal1", "crystal2", "empty"};
                        var strArray12 = new string[0];
                        var list2 = new List<string[]>
                                {
                                    strArray4,
                                    strArray5,
                                    strArray6,
                                    strArray7,
                                    strArray8,
                                    strArray9,
                                    strArray10,
                                    strArray11
                                };
                        string[] strArray13 =
                            {"bark", "wood", "grass", "brick", "metal", "rock", "stone", "misc", "transparent"};
                        var index = 78;
                        var num25 = 69;
                        num20 = Screen.width / 2 - 110.5f;
                        num21 = Screen.height / 2 - 220f;
                        var num26 = (int)settings[185];
                        num27 = 10f + 104f * (list2[num26].Length / 3 + 1);
                        num27 = Math.Max(num27, 280f);
                        UnityEngine.GUI.DrawTexture(new Rect(num20 + 2f, num21 + 2f, 208f, 446f), ColorCache.Blue);
                        UnityEngine.GUI.Box(new Rect(num20, num21, 212f, 450f), string.Empty);
                        for (num13 = 0; num13 < list2.Count; num13++)
                        {
                            num28 = num13 / 3;
                            num29 = num13 % 3;
                            if (UnityEngine.GUI.Button(new Rect(num20 + 5f + 69f * num29, num21 + 5f + 30 * num28, 64f, 25f),
                                strArray13[num13], "box"))
                            {
                                settings[185] = num13;
                            }
                        }

                        scroll2 = UnityEngine.GUI.BeginScrollView(new Rect(num20, num21 + 110f, 225f, 290f), scroll2,
                            new Rect(num20, num21 + 110f, 212f, num27), true, true);
                        if (num26 != 8)
                        {
                            for (num13 = 0; num13 < list2[num26].Length; num13++)
                            {
                                num28 = num13 / 3;
                                num29 = num13 % 3;
                                UnityEngine.GUI.DrawTexture(
                                    new Rect(num20 + 5f + 69f * num29, num21 + 115f + 104f * num28, 64f, 64f),
                                    FGM.RCLoadTexture("p" + list2[num26][num13]));
                                if (UnityEngine.GUI.Button(
                                    new Rect(num20 + 5f + 69f * num29, num21 + 184f + 104f * num28, 64f, 30f),
                                    list2[num26][num13]))
                                {
                                    settings[num25] = list2[num26][num13];
                                    settings[index] = 0;
                                }
                            }
                        }

                        UnityEngine.GUI.EndScrollView();
                        if (UnityEngine.GUI.Button(new Rect(num20 + 24f, num21 + 410f, 70f, 30f), "Default"))
                        {
                            settings[num25] = "default";
                            settings[index] = 0;
                        }
                        else if (UnityEngine.GUI.Button(new Rect(num20 + 118f, num21 + 410f, 70f, 30f), "Done"))
                        {
                            settings[index] = 0;
                        }
                    }

                    flag5 = false;
                    if ((int)settings[76] == 1)
                    {
                        flag5 = true;
                        textured = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                        textured.SetPixel(0, 0,
                            new Color((float)settings[73], (float)settings[74], (float)settings[75], 1f));
                        textured.Apply();
                        UnityEngine.GUI.DrawTexture(new Rect(num11 + 235f, 445f, 30f, 20f), textured, ScaleMode.StretchToFill);
                        Destroy(textured);
                    }

                    flag6 = UnityEngine.GUI.Toggle(new Rect(num11 + 193f, 445f, 40f, 20f), flag5, "On");
                    if (flag5 != flag6)
                    {
                        if (flag6)
                        {
                            settings[76] = 1;
                        }
                        else
                        {
                            settings[76] = 0;
                        }
                    }
                }
            }

            if (UnityEngine.GUI.Button(new Rect(num11 + 5f, 10f, 60f, 25f), "General", "box"))
            {
                settings[64] = 101;
            }
            else if (UnityEngine.GUI.Button(new Rect(num11 + 70f, 10f, 70f, 25f), "Geometry", "box"))
            {
                settings[64] = 102;
            }
            else if (UnityEngine.GUI.Button(new Rect(num11 + 145f, 10f, 65f, 25f), "Buildings", "box"))
            {
                settings[64] = 103;
            }
            else if (UnityEngine.GUI.Button(new Rect(num11 + 215f, 10f, 50f, 25f), "Nature", "box"))
            {
                settings[64] = 104;
            }
            else if (UnityEngine.GUI.Button(new Rect(num11 + 5f, 45f, 70f, 25f), "Spawners", "box"))
            {
                settings[64] = 105;
            }
            else if (UnityEngine.GUI.Button(new Rect(num11 + 80f, 45f, 70f, 25f), "Racing", "box"))
            {
                settings[64] = 108;
            }
            else if (UnityEngine.GUI.Button(new Rect(num11 + 155f, 45f, 40f, 25f), "Misc", "box"))
            {
                settings[64] = 107;
            }
            else if (UnityEngine.GUI.Button(new Rect(num11 + 200f, 45f, 70f, 25f), "Credits", "box"))
            {
                settings[64] = 106;
            }

            if ((int)settings[64] == 101)
            {
                GameObject obj5;
                scroll = UnityEngine.GUI.BeginScrollView(new Rect(num11, 80f, 305f, 350f), scroll,
                    new Rect(num11, 80f, 300f, 470f), true, true);
                UnityEngine.GUI.Label(new Rect(num11 + 100f, 80f, 120f, 20f), "General Objects:", "Label");
                UnityEngine.GUI.Label(new Rect(num11 + 108f, 245f, 120f, 20f), "Spawn Points:", "Label");
                UnityEngine.GUI.Label(new Rect(num11 + 7f, 415f, 290f, 60f),
                    "* The above titan spawn points apply only to randomly spawned titans specified by the Random Titan #.",
                    "Label");
                UnityEngine.GUI.Label(new Rect(num11 + 7f, 470f, 290f, 60f),
                    "* If team mode is disabled both cyan and magenta spawn points will be randomly chosen for players.",
                    "Label");
                UnityEngine.GUI.DrawTexture(new Rect(num11 + 27f, 110f, 64f, 64f), FGM.RCLoadTexture("psupply"));
                UnityEngine.GUI.DrawTexture(new Rect(num11 + 118f, 110f, 64f, 64f), FGM.RCLoadTexture("pcannonwall"));
                UnityEngine.GUI.DrawTexture(new Rect(num11 + 209f, 110f, 64f, 64f), FGM.RCLoadTexture("pcannonground"));
                UnityEngine.GUI.DrawTexture(new Rect(num11 + 27f, 275f, 64f, 64f), FGM.RCLoadTexture("pspawnt"));
                UnityEngine.GUI.DrawTexture(new Rect(num11 + 118f, 275f, 64f, 64f), FGM.RCLoadTexture("pspawnplayerC"));
                UnityEngine.GUI.DrawTexture(new Rect(num11 + 209f, 275f, 64f, 64f), FGM.RCLoadTexture("pspawnplayerM"));
                if (UnityEngine.GUI.Button(new Rect(num11 + 27f, 179f, 64f, 60f), "Supply"))
                {
                    flag2 = true;
                    obj5 = (GameObject)Resources.Load("aot_supply");
                    FGM.selectedObj = (GameObject)Instantiate(obj5);
                    FGM.selectedObj.name = "base,aot_supply";
                }
                else if (UnityEngine.GUI.Button(new Rect(num11 + 118f, 179f, 64f, 60f), "Cannon \nWall"))
                {
                    flag2 = true;
                    obj5 = (GameObject)ResourcesCache.RCLoadGO("CannonWallProp");
                    FGM.selectedObj = (GameObject)Instantiate(obj5);
                    FGM.selectedObj.name = "photon,CannonWall";
                }
                else if (UnityEngine.GUI.Button(new Rect(num11 + 209f, 179f, 64f, 60f), "Cannon\n Ground"))
                {
                    flag2 = true;
                    obj5 = (GameObject)ResourcesCache.RCLoadGO("CannonGroundProp");
                    FGM.selectedObj = (GameObject)Instantiate(obj5);
                    FGM.selectedObj.name = "photon,CannonGround";
                }
                else if (UnityEngine.GUI.Button(new Rect(num11 + 27f, 344f, 64f, 60f), "Titan"))
                {
                    flag2 = true;
                    flag3 = true;
                    obj5 = (GameObject)ResourcesCache.RCLoadGO("titan");
                    FGM.selectedObj = (GameObject)Instantiate(obj5);
                    FGM.selectedObj.name = "spawnpoint,titan";
                }
                else if (UnityEngine.GUI.Button(new Rect(num11 + 118f, 344f, 64f, 60f), "Player \nCyan"))
                {
                    flag2 = true;
                    flag3 = true;
                    obj5 = (GameObject)ResourcesCache.RCLoadGO("playerC");
                    FGM.selectedObj = (GameObject)Instantiate(obj5);
                    FGM.selectedObj.name = "spawnpoint,playerC";
                }
                else if (UnityEngine.GUI.Button(new Rect(num11 + 209f, 344f, 64f, 60f), "Player \nMagenta"))
                {
                    flag2 = true;
                    flag3 = true;
                    obj5 = (GameObject)ResourcesCache.RCLoadGO("playerM");
                    FGM.selectedObj = (GameObject)Instantiate(obj5);
                    FGM.selectedObj.name = "spawnpoint,playerM";
                }

                UnityEngine.GUI.EndScrollView();
            }
            else
            {
                GameObject obj6;
                if ((int)settings[64] == 107)
                {
                    UnityEngine.GUI.DrawTexture(new Rect(num11 + 30f, 90f, 64f, 64f), FGM.RCLoadTexture("pbarrier"));
                    UnityEngine.GUI.DrawTexture(new Rect(num11 + 30f, 199f, 64f, 64f), FGM.RCLoadTexture("pregion"));
                    UnityEngine.GUI.Label(new Rect(num11 + 110f, 243f, 200f, 22f), "Region Name:", "Label");
                    UnityEngine.GUI.Label(new Rect(num11 + 110f, 179f, 200f, 22f), "Disable Map Bounds:", "Label");
                    var flag7 = false;
                    if ((int)settings[186] == 1)
                    {
                        flag7 = true;
                        if (!linkHash[3].ContainsKey("mapbounds"))
                        {
                            linkHash[3].Add("mapbounds", "map,disablebounds");
                        }
                    }
                    else if (linkHash[3].ContainsKey("mapbounds"))
                    {
                        linkHash[3].Remove("mapbounds");
                    }

                    if (UnityEngine.GUI.Button(new Rect(num11 + 30f, 159f, 64f, 30f), "Barrier"))
                    {
                        flag2 = true;
                        flag3 = true;
                        obj6 = (GameObject)ResourcesCache.RCLoadGO("barrierEditor");
                        FGM.selectedObj = (GameObject)Instantiate(obj6);
                        FGM.selectedObj.name = "misc,barrier";
                    }
                    else if (UnityEngine.GUI.Button(new Rect(num11 + 30f, 268f, 64f, 30f), "Region"))
                    {
                        if ((string)settings[191] == string.Empty)
                        {
                            settings[191] = "Region" + Random.Range(10000, 99999);
                        }

                        flag2 = true;
                        flag3 = true;
                        obj6 = (GameObject)ResourcesCache.RCLoadGO("regionEditor");
                        FGM.selectedObj = (GameObject)Instantiate(obj6);
                        obj4 = (GameObject)Instantiate(Resources.Load("UI/LabelNameOverHead"));
                        obj4.name = "RegionLabel";
                        if (!float.TryParse((string)settings[71], out num31))
                        {
                            settings[71] = "1";
                        }

                        if (!float.TryParse((string)settings[70], out num31))
                        {
                            settings[70] = "1";
                        }

                        if (!float.TryParse((string)settings[72], out num31))
                        {
                            settings[72] = "1";
                        }

                        obj4.transform.parent = FGM.selectedObj.transform;
                        num14 = 1f;
                        if (Convert.ToSingle((string)settings[71]) > 100f)
                        {
                            num14 = 0.8f;
                        }
                        else if (Convert.ToSingle((string)settings[71]) > 1000f)
                        {
                            num14 = 0.5f;
                        }

                        obj4.transform.localPosition = new Vector3(0f, num14, 0f);
                        obj4.transform.localScale = new Vector3(5f / Convert.ToSingle((string)settings[70]),
                            5f / Convert.ToSingle((string)settings[71]),
                            5f / Convert.ToSingle((string)settings[72]));
                        obj4.GetComponent<UILabel>().text = (string)settings[191];
                        FGM.selectedObj.AddComponent<RCRegionLabel>();
                        FGM.selectedObj.GetComponent<RCRegionLabel>().myLabel = obj4;
                        FGM.selectedObj.name = "misc,region," + (string)settings[191];
                    }

                    settings[191] = UnityEngine.GUI.TextField(new Rect(num11 + 200f, 243f, 75f, 20f), (string)settings[191]);
                    var flag8 = UnityEngine.GUI.Toggle(new Rect(num11 + 240f, 179f, 40f, 20f), flag7, "On");
                    if (flag8 != flag7)
                    {
                        if (flag8)
                        {
                            settings[186] = 1;
                        }
                        else
                        {
                            settings[186] = 0;
                        }
                    }
                }
                else if ((int)settings[64] == 105)
                {
                    float num32;
                    GameObject obj7;
                    UnityEngine.GUI.Label(new Rect(num11 + 95f, 85f, 130f, 20f), "Custom Spawners:", "Label");
                    UnityEngine.GUI.DrawTexture(new Rect(num11 + 7.8f, 110f, 64f, 64f), FGM.RCLoadTexture("ptitan"));
                    UnityEngine.GUI.DrawTexture(new Rect(num11 + 79.6f, 110f, 64f, 64f), FGM.RCLoadTexture("pabnormal"));
                    UnityEngine.GUI.DrawTexture(new Rect(num11 + 151.4f, 110f, 64f, 64f), FGM.RCLoadTexture("pjumper"));
                    UnityEngine.GUI.DrawTexture(new Rect(num11 + 223.2f, 110f, 64f, 64f), FGM.RCLoadTexture("pcrawler"));
                    UnityEngine.GUI.DrawTexture(new Rect(num11 + 7.8f, 224f, 64f, 64f), FGM.RCLoadTexture("ppunk"));
                    UnityEngine.GUI.DrawTexture(new Rect(num11 + 79.6f, 224f, 64f, 64f), FGM.RCLoadTexture("pannie"));
                    if (UnityEngine.GUI.Button(new Rect(num11 + 7.8f, 179f, 64f, 30f), "Titan"))
                    {
                        if (!float.TryParse((string)settings[83], out num32))
                        {
                            settings[83] = "30";
                        }

                        flag2 = true;
                        flag3 = true;
                        obj7 = (GameObject)ResourcesCache.RCLoadGO("spawnTitan");
                        FGM.selectedObj = (GameObject)Instantiate(obj7);
                        num30 = (int)settings[84];
                        FGM.selectedObj.name = "photon,spawnTitan," + (string)settings[83] + "," + num30;
                    }
                    else if (UnityEngine.GUI.Button(new Rect(num11 + 79.6f, 179f, 64f, 30f), "Aberrant"))
                    {
                        if (!float.TryParse((string)settings[83], out num32))
                        {
                            settings[83] = "30";
                        }

                        flag2 = true;
                        flag3 = true;
                        obj7 = (GameObject)ResourcesCache.RCLoadGO("spawnAbnormal");
                        FGM.selectedObj = (GameObject)Instantiate(obj7);
                        num30 = (int)settings[84];
                        FGM.selectedObj.name = "photon,spawnAbnormal," + (string)settings[83] + "," + num30;
                    }
                    else if (UnityEngine.GUI.Button(new Rect(num11 + 151.4f, 179f, 64f, 30f), "Jumper"))
                    {
                        if (!float.TryParse((string)settings[83], out num32))
                        {
                            settings[83] = "30";
                        }

                        flag2 = true;
                        flag3 = true;
                        obj7 = (GameObject)ResourcesCache.RCLoadGO("spawnJumper");
                        FGM.selectedObj = (GameObject)Instantiate(obj7);
                        num30 = (int)settings[84];
                        FGM.selectedObj.name = "photon,spawnJumper," + (string)settings[83] + "," + num30;
                    }
                    else if (UnityEngine.GUI.Button(new Rect(num11 + 223.2f, 179f, 64f, 30f), "Crawler"))
                    {
                        if (!float.TryParse((string)settings[83], out num32))
                        {
                            settings[83] = "30";
                        }

                        flag2 = true;
                        flag3 = true;
                        obj7 = (GameObject)ResourcesCache.RCLoadGO("spawnCrawler");
                        FGM.selectedObj = (GameObject)Instantiate(obj7);
                        num30 = (int)settings[84];
                        FGM.selectedObj.name = "photon,spawnCrawler," + (string)settings[83] + "," + num30;
                    }
                    else if (UnityEngine.GUI.Button(new Rect(num11 + 7.8f, 293f, 64f, 30f), "Punk"))
                    {
                        if (!float.TryParse((string)settings[83], out num32))
                        {
                            settings[83] = "30";
                        }

                        flag2 = true;
                        flag3 = true;
                        obj7 = (GameObject)ResourcesCache.RCLoadGO("spawnPunk");
                        FGM.selectedObj = (GameObject)Instantiate(obj7);
                        num30 = (int)settings[84];
                        FGM.selectedObj.name = "photon,spawnPunk," + (string)settings[83] + "," + num30;
                    }
                    else if (UnityEngine.GUI.Button(new Rect(num11 + 79.6f, 293f, 64f, 30f), "Annie"))
                    {
                        if (!float.TryParse((string)settings[83], out num32))
                        {
                            settings[83] = "30";
                        }

                        flag2 = true;
                        flag3 = true;
                        obj7 = (GameObject)ResourcesCache.RCLoadGO("spawnAnnie");
                        FGM.selectedObj = (GameObject)Instantiate(obj7);
                        num30 = (int)settings[84];
                        FGM.selectedObj.name = "photon,spawnAnnie," + (string)settings[83] + "," + num30;
                    }

                    UnityEngine.GUI.Label(new Rect(num11 + 7f, 379f, 140f, 22f), "Spawn Timer:", "Label");
                    settings[83] = UnityEngine.GUI.TextField(new Rect(num11 + 100f, 379f, 50f, 20f), (string)settings[83]);
                    UnityEngine.GUI.Label(new Rect(num11 + 7f, 356f, 140f, 22f), "Endless spawn:", "Label");
                    UnityEngine.GUI.Label(new Rect(num11 + 7f, 405f, 290f, 80f),
                        "* The above settings apply only to the next placed spawner. You can have unique spawn times and settings for each individual titan spawner.",
                        "Label");
                    var flag9 = false;
                    if ((int)settings[84] == 1)
                    {
                        flag9 = true;
                    }

                    flag10 = UnityEngine.GUI.Toggle(new Rect(num11 + 100f, 356f, 40f, 20f), flag9, "On");
                    if (flag9 != flag10)
                    {
                        if (flag10)
                        {
                            settings[84] = 1;
                        }
                        else
                        {
                            settings[84] = 0;
                        }
                    }
                }
                else
                {
                    string[] strArray14;
                    if ((int)settings[64] == 102)
                    {
                        strArray14 = new[]
                        {
                                    "cuboid", "plane", "sphere", "cylinder", "capsule", "pyramid", "cone", "prism", "arc90",
                                    "arc180", "torus", "tube"
                                };
                        for (num13 = 0; num13 < strArray14.Length; num13++)
                        {
                            num29 = num13 % 4;
                            num28 = num13 / 4;
                            UnityEngine.GUI.DrawTexture(new Rect(num11 + 7.8f + 71.8f * num29, 90f + 114f * num28, 64f, 64f),
                                FGM.RCLoadTexture("p" + strArray14[num13]));
                            if (UnityEngine.GUI.Button(new Rect(num11 + 7.8f + 71.8f * num29, 159f + 114f * num28, 64f, 30f),
                                strArray14[num13]))
                            {
                                flag2 = true;
                                obj6 = (GameObject)ResourcesCache.RCLoadGO(strArray14[num13]);
                                FGM.selectedObj = (GameObject)Instantiate(obj6);
                                FGM.selectedObj.name = "custom," + strArray14[num13];
                            }
                        }
                    }
                    else
                    {
                        List<string> list4;
                        GameObject obj8;
                        if ((int)settings[64] == 103)
                        {
                            list4 = new List<string> { "arch1", "house1" };
                            strArray14 = new[]
                            {
                                        "tower1", "tower2", "tower3", "tower4", "tower5", "house1", "house2", "house3",
                                        "house4", "house5", "house6", "house7", "house8", "house9", "house10", "house11",
                                        "house12", "house13", "house14", "pillar1", "pillar2", "village1", "village2",
                                        "windmill1", "arch1", "canal1", "castle1", "church1", "cannon1", "statue1",
                                        "statue2", "wagon1",
                                        "elevator1", "bridge1", "dummy1", "spike1", "wall1", "wall2", "wall3", "wall4",
                                        "arena1", "arena2", "arena3", "arena4"
                                    };
                            num27 = 110f + 114f * ((strArray14.Length - 1) / 4);
                            scroll = UnityEngine.GUI.BeginScrollView(new Rect(num11, 90f, 303f, 350f), scroll,
                                new Rect(num11, 90f, 300f, num27), true, true);
                            for (num13 = 0; num13 < strArray14.Length; num13++)
                            {
                                num29 = num13 % 4;
                                num28 = num13 / 4;
                                UnityEngine.GUI.DrawTexture(
                                    new Rect(num11 + 7.8f + 71.8f * num29, 90f + 114f * num28, 64f, 64f),
                                    FGM.RCLoadTexture("p" + strArray14[num13]));
                                if (UnityEngine.GUI.Button(
                                    new Rect(num11 + 7.8f + 71.8f * num29, 159f + 114f * num28, 64f, 30f),
                                    strArray14[num13]))
                                {
                                    flag2 = true;
                                    obj8 = (GameObject)ResourcesCache.RCLoadGO(strArray14[num13]);
                                    FGM.selectedObj = (GameObject)Instantiate(obj8);
                                    if (list4.Contains(strArray14[num13]))
                                    {
                                        FGM.selectedObj.name = "customb," + strArray14[num13];
                                    }
                                    else
                                    {
                                        FGM.selectedObj.name = "custom," + strArray14[num13];
                                    }
                                }
                            }

                            UnityEngine.GUI.EndScrollView();
                        }
                        else if ((int)settings[64] == 104)
                        {
                            list4 = new List<string> { "tree0" };
                            strArray14 = new[]
                            {
                                        "leaf0", "leaf1", "leaf2", "field1", "field2", "tree0", "tree1", "tree2", "tree3",
                                        "tree4", "tree5", "tree6", "tree7", "log1", "log2", "trunk1",
                                        "boulder1", "boulder2", "boulder3", "boulder4", "boulder5", "cave1", "cave2"
                                    };
                            num27 = 110f + 114f * ((strArray14.Length - 1) / 4);
                            scroll = UnityEngine.GUI.BeginScrollView(new Rect(num11, 90f, 303f, 350f), scroll,
                                new Rect(num11, 90f, 300f, num27), true, true);
                            for (num13 = 0; num13 < strArray14.Length; num13++)
                            {
                                num29 = num13 % 4;
                                num28 = num13 / 4;
                                UnityEngine.GUI.DrawTexture(
                                    new Rect(num11 + 7.8f + 71.8f * num29, 90f + 114f * num28, 64f, 64f),
                                    FGM.RCLoadTexture("p" + strArray14[num13]));
                                if (UnityEngine.GUI.Button(
                                    new Rect(num11 + 7.8f + 71.8f * num29, 159f + 114f * num28, 64f, 30f),
                                    strArray14[num13]))
                                {
                                    flag2 = true;
                                    obj8 = (GameObject)ResourcesCache.RCLoadGO(strArray14[num13]);
                                    FGM.selectedObj = (GameObject)Instantiate(obj8);
                                    if (list4.Contains(strArray14[num13]))
                                    {
                                        FGM.selectedObj.name = "customb," + strArray14[num13];
                                    }
                                    else
                                    {
                                        FGM.selectedObj.name = "custom," + strArray14[num13];
                                    }
                                }
                            }

                            UnityEngine.GUI.EndScrollView();
                        }
                        else if ((int)settings[64] == 108)
                        {
                            string[] strArray15 =
                            {
                                        "Cuboid", "Plane", "Sphere", "Cylinder", "Capsule", "Pyramid", "Cone", "Prism",
                                        "Arc90", "Arc180", "Torus", "Tube"
                                    };
                            strArray14 = new string[12];
                            for (num13 = 0; num13 < strArray14.Length; num13++)
                            {
                                strArray14[num13] = "start" + strArray15[num13];
                            }

                            num27 = 110f + 114f * ((strArray14.Length - 1) / 4);
                            num27 *= 4f;
                            num27 += 200f;
                            scroll = UnityEngine.GUI.BeginScrollView(new Rect(num11, 90f, 303f, 350f), scroll,
                                new Rect(num11, 90f, 300f, num27), true, true);
                            UnityEngine.GUI.Label(new Rect(num11 + 90f, 90f, 200f, 22f), "Racing Start Barrier");
                            var num33 = 125;
                            for (num13 = 0; num13 < strArray14.Length; num13++)
                            {
                                num29 = num13 % 4;
                                num28 = num13 / 4;
                                UnityEngine.GUI.DrawTexture(
                                    new Rect(num11 + 7.8f + 71.8f * num29, num33 + 114f * num28, 64f, 64f),
                                    FGM.RCLoadTexture("p" + strArray14[num13]));
                                if (UnityEngine.GUI.Button(
                                    new Rect(num11 + 7.8f + 71.8f * num29, num33 + 69f + 114f * num28, 64f, 30f),
                                    strArray15[num13]))
                                {
                                    flag2 = true;
                                    flag3 = true;
                                    obj8 = (GameObject)ResourcesCache.RCLoadGO(strArray14[num13]);
                                    FGM.selectedObj = (GameObject)Instantiate(obj8);
                                    FGM.selectedObj.name = "racing," + strArray14[num13];
                                }
                            }

                            num33 += 114 * (strArray14.Length / 4) + 10;
                            UnityEngine.GUI.Label(new Rect(num11 + 93f, num33, 200f, 22f), "Racing End Trigger");
                            num33 += 35;
                            for (num13 = 0; num13 < strArray14.Length; num13++)
                            {
                                strArray14[num13] = "end" + strArray15[num13];
                            }

                            for (num13 = 0; num13 < strArray14.Length; num13++)
                            {
                                num29 = num13 % 4;
                                num28 = num13 / 4;
                                UnityEngine.GUI.DrawTexture(
                                    new Rect(num11 + 7.8f + 71.8f * num29, num33 + 114f * num28, 64f, 64f),
                                    FGM.RCLoadTexture("p" + strArray14[num13]));
                                if (UnityEngine.GUI.Button(
                                    new Rect(num11 + 7.8f + 71.8f * num29, num33 + 69f + 114f * num28, 64f, 30f),
                                    strArray15[num13]))
                                {
                                    flag2 = true;
                                    flag3 = true;
                                    obj8 = (GameObject)ResourcesCache.RCLoadGO(strArray14[num13]);
                                    FGM.selectedObj = (GameObject)Instantiate(obj8);
                                    FGM.selectedObj.name = "racing," + strArray14[num13];
                                }
                            }

                            num33 += 114 * (strArray14.Length / 4) + 10;
                            UnityEngine.GUI.Label(new Rect(num11 + 113f, num33, 200f, 22f), "Kill Trigger");
                            num33 += 35;
                            for (num13 = 0; num13 < strArray14.Length; num13++)
                            {
                                strArray14[num13] = "kill" + strArray15[num13];
                            }

                            for (num13 = 0; num13 < strArray14.Length; num13++)
                            {
                                num29 = num13 % 4;
                                num28 = num13 / 4;
                                UnityEngine.GUI.DrawTexture(
                                    new Rect(num11 + 7.8f + 71.8f * num29, num33 + 114f * num28, 64f, 64f),
                                    FGM.RCLoadTexture("p" + strArray14[num13]));
                                if (UnityEngine.GUI.Button(
                                    new Rect(num11 + 7.8f + 71.8f * num29, num33 + 69f + 114f * num28, 64f, 30f),
                                    strArray15[num13]))
                                {
                                    flag2 = true;
                                    flag3 = true;
                                    obj8 = (GameObject)ResourcesCache.RCLoadGO(strArray14[num13]);
                                    FGM.selectedObj = (GameObject)Instantiate(obj8);
                                    FGM.selectedObj.name = "racing," + strArray14[num13];
                                }
                            }

                            num33 += 114 * (strArray14.Length / 4) + 10;
                            UnityEngine.GUI.Label(new Rect(num11 + 95f, num33, 200f, 22f), "Checkpoint Trigger");
                            num33 += 35;
                            for (num13 = 0; num13 < strArray14.Length; num13++)
                            {
                                strArray14[num13] = "checkpoint" + strArray15[num13];
                            }

                            for (num13 = 0; num13 < strArray14.Length; num13++)
                            {
                                num29 = num13 % 4;
                                num28 = num13 / 4;
                                UnityEngine.GUI.DrawTexture(
                                    new Rect(num11 + 7.8f + 71.8f * num29, num33 + 114f * num28, 64f, 64f),
                                    FGM.RCLoadTexture("p" + strArray14[num13]));
                                if (UnityEngine.GUI.Button(
                                    new Rect(num11 + 7.8f + 71.8f * num29, num33 + 69f + 114f * num28, 64f, 30f),
                                    strArray15[num13]))
                                {
                                    flag2 = true;
                                    flag3 = true;
                                    obj8 = (GameObject)ResourcesCache.RCLoadGO(strArray14[num13]);
                                    FGM.selectedObj = (GameObject)Instantiate(obj8);
                                    FGM.selectedObj.name = "racing," + strArray14[num13];
                                }
                            }

                            UnityEngine.GUI.EndScrollView();
                        }
                        else if ((int)settings[64] == 106)
                        {
                            UnityEngine.GUI.Label(new Rect(num11 + 10f, 80f, 200f, 22f), "- Tree 2 designed by Ken P.",
                                "Label");
                            UnityEngine.GUI.Label(new Rect(num11 + 10f, 105f, 250f, 22f),
                                "- Tower 2, House 5 designed by Matthew Santos", "Label");
                            UnityEngine.GUI.Label(new Rect(num11 + 10f, 130f, 200f, 22f), "- Cannon retextured by Mika",
                                "Label");
                            UnityEngine.GUI.Label(new Rect(num11 + 10f, 155f, 200f, 22f), "- Arena 1,2,3 & 4 created by Gun",
                                "Label");
                            UnityEngine.GUI.Label(new Rect(num11 + 10f, 180f, 250f, 22f),
                                "- Cannon Wall/Ground textured by Bellfox", "Label");
                            UnityEngine.GUI.Label(new Rect(num11 + 10f, 205f, 250f, 120f),
                                "- House 7 - 14, Statue1, Statue2, Wagon1, Wall 1, Wall 2, Wall 3, Wall 4, CannonWall, CannonGround, Tower5, Bridge1, Dummy1, Spike1 created by meecube",
                                "Label");
                        }
                    }
                }
            }

            if (flag2 && FGM.selectedObj != null)
            {
                float y;
                float num37;
                float num38;
                float num39;
                float z;
                float num41;
                string name;
                if (!float.TryParse((string)settings[70], out num31))
                {
                    settings[70] = "1";
                }

                if (!float.TryParse((string)settings[71], out num31))
                {
                    settings[71] = "1";
                }

                if (!float.TryParse((string)settings[72], out num31))
                {
                    settings[72] = "1";
                }

                if (!float.TryParse((string)settings[79], out num31))
                {
                    settings[79] = "1";
                }

                if (!float.TryParse((string)settings[80], out num31))
                {
                    settings[80] = "1";
                }

                if (!flag3)
                {
                    var a = 1f;
                    if ((string)settings[69] != "default")
                    {
                        if (((string)settings[69]).StartsWith("transparent"))
                        {
                            float num35;
                            if (float.TryParse(((string)settings[69]).Substring(11), out num35))
                            {
                                a = num35;
                            }

                            foreach (var renderer2 in FGM.selectedObj.GetComponentsInChildren<Renderer>())
                            {
                                renderer2.material = (Material)ResourcesCache.RCLoadM("transparent");
                                renderer2.material.mainTextureScale = new Vector2(
                                    renderer2.material.mainTextureScale.x *
                                    Convert.ToSingle((string)settings[79]),
                                    renderer2.material.mainTextureScale.y *
                                    Convert.ToSingle((string)settings[80]));
                            }
                        }
                        else
                        {
                            foreach (var renderer2 in FGM.selectedObj.GetComponentsInChildren<Renderer>())
                            {
                                if (!(renderer2.name.Contains("Particle System") &&
                                      FGM.selectedObj.name.Contains("aot_supply")))
                                {
                                    renderer2.material = (Material)ResourcesCache.RCLoadM((string)settings[69]);
                                    renderer2.material.mainTextureScale = new Vector2(
                                        renderer2.material.mainTextureScale.x *
                                        Convert.ToSingle((string)settings[79]),
                                        renderer2.material.mainTextureScale.y *
                                        Convert.ToSingle((string)settings[80]));
                                }
                            }
                        }
                    }

                    y = 1f;
                    foreach (var filter in FGM.selectedObj.GetComponentsInChildren<MeshFilter>())
                    {
                        if (FGM.selectedObj.name.StartsWith("customb"))
                        {
                            if (y < filter.mesh.bounds.size.y)
                            {
                                y = filter.mesh.bounds.size.y;
                            }
                        }
                        else if (y < filter.mesh.bounds.size.z)
                        {
                            y = filter.mesh.bounds.size.z;
                        }
                    }

                    num37 = FGM.selectedObj.transform.localScale.x * Convert.ToSingle((string)settings[70]);
                    num37 -= 0.001f;
                    num38 = FGM.selectedObj.transform.localScale.y * Convert.ToSingle((string)settings[71]);
                    num39 = FGM.selectedObj.transform.localScale.z * Convert.ToSingle((string)settings[72]);
                    FGM.selectedObj.transform.localScale = new Vector3(num37, num38, num39);
                    if ((int)settings[76] == 1)
                    {
                        color = new Color((float)settings[73], (float)settings[74], (float)settings[75],
                            a);
                        foreach (var filter in FGM.selectedObj.GetComponentsInChildren<MeshFilter>())
                        {
                            mesh = filter.mesh;
                            colorArray = new Color[mesh.vertexCount];
                            num18 = 0;
                            while (num18 < mesh.vertexCount)
                            {
                                colorArray[num18] = color;
                                num18++;
                            }

                            mesh.colors = colorArray;
                        }
                    }

                    z = FGM.selectedObj.transform.localScale.z;
                    if (FGM.selectedObj.name.Contains("boulder2") || FGM.selectedObj.name.Contains("boulder3") ||
                        FGM.selectedObj.name.Contains("field2"))
                    {
                        z *= 0.01f;
                    }

                    num41 = 10f + z * y * 1.2f / 2f;
                    FGM.selectedObj.transform.position = new Vector3(
                        Camera.main.transform.position.x + Camera.main.transform.forward.x * num41,
                        Camera.main.transform.position.y + Camera.main.transform.forward.y * 10f,
                        Camera.main.transform.position.z + Camera.main.transform.forward.z * num41);
                    FGM.selectedObj.transform.rotation =
                        Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
                    name = FGM.selectedObj.name;
                    var strArray3 = new string[21];
                    strArray3[0] = name;
                    strArray3[1] = ",";
                    strArray3[2] = (string)settings[69];
                    strArray3[3] = ",";
                    strArray3[4] = (string)settings[70];
                    strArray3[5] = ",";
                    strArray3[6] = (string)settings[71];
                    strArray3[7] = ",";
                    strArray3[8] = (string)settings[72];
                    strArray3[9] = ",";
                    strArray3[10] = settings[76].ToString();
                    strArray3[11] = ",";
                    var num42 = (float)settings[73];
                    strArray3[12] = num42.ToString();
                    strArray3[13] = ",";
                    num42 = (float)settings[74];
                    strArray3[14] = num42.ToString();
                    strArray3[15] = ",";
                    strArray3[16] = ((float)settings[75]).ToString();
                    strArray3[17] = ",";
                    strArray3[18] = (string)settings[79];
                    strArray3[19] = ",";
                    strArray3[20] = (string)settings[80];
                    FGM.selectedObj.name = string.Concat(strArray3);
                    FGM.unloadAssetsEditor();
                }
                else if (FGM.selectedObj.name.StartsWith("misc"))
                {
                    if (FGM.selectedObj.name.Contains("barrier") || FGM.selectedObj.name.Contains("region") ||
                        FGM.selectedObj.name.Contains("racing"))
                    {
                        y = 1f;
                        num37 = FGM.selectedObj.transform.localScale.x * Convert.ToSingle((string)settings[70]);
                        num37 -= 0.001f;
                        num38 = FGM.selectedObj.transform.localScale.y * Convert.ToSingle((string)settings[71]);
                        num39 = FGM.selectedObj.transform.localScale.z * Convert.ToSingle((string)settings[72]);
                        FGM.selectedObj.transform.localScale = new Vector3(num37, num38, num39);
                        z = FGM.selectedObj.transform.localScale.z;
                        num41 = 10f + z * y * 1.2f / 2f;
                        FGM.selectedObj.transform.position = new Vector3(
                            Camera.main.transform.position.x + Camera.main.transform.forward.x * num41,
                            Camera.main.transform.position.y + Camera.main.transform.forward.y * 10f,
                            Camera.main.transform.position.z + Camera.main.transform.forward.z * num41);
                        if (!FGM.selectedObj.name.Contains("region"))
                        {
                            FGM.selectedObj.transform.rotation = Quaternion.Euler(0f,
                                Camera.main.transform.rotation.eulerAngles.y, 0f);
                        }

                        name = FGM.selectedObj.name;
                        FGM.selectedObj.name = name + "," + (string)settings[70] + "," + (string)settings[71] +
                                           "," + (string)settings[72];
                    }
                }
                else if (FGM.selectedObj.name.StartsWith("racing"))
                {
                    y = 1f;
                    num37 = FGM.selectedObj.transform.localScale.x * Convert.ToSingle((string)settings[70]);
                    num37 -= 0.001f;
                    num38 = FGM.selectedObj.transform.localScale.y * Convert.ToSingle((string)settings[71]);
                    num39 = FGM.selectedObj.transform.localScale.z * Convert.ToSingle((string)settings[72]);
                    FGM.selectedObj.transform.localScale = new Vector3(num37, num38, num39);
                    z = FGM.selectedObj.transform.localScale.z;
                    num41 = 10f + z * y * 1.2f / 2f;
                    FGM.selectedObj.transform.position = new Vector3(
                        Camera.main.transform.position.x + Camera.main.transform.forward.x * num41,
                        Camera.main.transform.position.y + Camera.main.transform.forward.y * 10f,
                        Camera.main.transform.position.z + Camera.main.transform.forward.z * num41);
                    FGM.selectedObj.transform.rotation =
                        Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
                    name = FGM.selectedObj.name;
                    FGM.selectedObj.name = name + "," + (string)settings[70] + "," + (string)settings[71] + "," +
                                       (string)settings[72];
                }
                else
                {
                    FGM.selectedObj.transform.position = new Vector3(
                        Camera.main.transform.position.x + Camera.main.transform.forward.x * 10f,
                        Camera.main.transform.position.y + Camera.main.transform.forward.y * 10f,
                        Camera.main.transform.position.z + Camera.main.transform.forward.z * 10f);
                    FGM.selectedObj.transform.rotation =
                        Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
                }

                Screen.lockCursor = true;
                UnityEngine.GUI.FocusControl(null);
            }
        }
    }
}