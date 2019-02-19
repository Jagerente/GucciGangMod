using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace GGM
{
    public class SpectatorMode : UnityEngine.MonoBehaviour
    {
        readonly string path = Application.dataPath + "/SaveCameraSetting.cfg";
        public static SpectatorMode instance;
        static KeyCode buttonEnable = KeyCode.M;
        static KeyCode buttonShowGUI = KeyCode.N;
        static KeyCode buttonUP = KeyCode.W;
        static KeyCode buttonDOWN = KeyCode.S;
        static KeyCode buttonRIGHT = KeyCode.D;
        static KeyCode buttonLEFT = KeyCode.A;
        public static bool onEnable = false;
        static bool showGui = true;
        static bool multi_button = true;
        Transform baseT;
        float speed_camera = 0.05f;
        float speed_button = 0.2f;
        Dictionary<string, Timered> timered;

        float speedCamera
        {
            get { return ((speed_camera * Time.deltaTime) * 62f); }
        }
        void Awake()
        {
            instance = this;
            timered = new Dictionary<string, Timered>();
            timered.Add("up", new Timered());
            timered.Add("down", new Timered());
            timered.Add("right", new Timered());
            timered.Add("left", new Timered());
            Save_Load_Settings(false);
        }
        void Save_Load_Settings(bool flag)
        {
            if (flag)
            {
                string str = "smooth:" + multi_button + "\nspeedbutton:" + speed_button + "\nspeedcamera:" + speed_camera;
                File.WriteAllText(path, str, System.Text.Encoding.UTF8);
            }
            else
            {
                FileInfo info = new FileInfo(path);
                if (info.Exists)
                {
                    string[] lines = File.ReadAllLines(path, System.Text.Encoding.UTF8);
                    foreach (string str in lines)
                    {
                        if (str.Trim() != "" && str.Contains(":"))
                        {
                            string[] keys = str.Split(new char[] { ':' });
                            string key = keys[0].Trim(); string value = keys[1].Trim();
                            if (key == "smooth")
                            {
                                multi_button = (value == "True");
                            }
                            else if (key == "speedbutton")
                            {
                                speed_button = Convert.ToSingle(value);
                            }
                            else if (key == "speedcamera")
                            {
                                speed_camera = Convert.ToSingle(value);
                            }
                        }
                    }
                }
            }
        }
        void Activate(bool flag)
        {
            if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.STOP)
            {
                GameObject gm = GameObject.Find("MainCamera");
                if (gm != null)
                {
                    baseT = gm.transform;
                    if (flag)
                    {
                        Screen.showCursor = true;
                        Screen.lockCursor = false;
                        gm.GetComponent<IN_GAME_MAIN_CAMERA>().setSpectorMode(false);

                    }
                    else
                    {
                        Screen.showCursor = false;
                        Screen.lockCursor = true;
                        gm.GetComponent<IN_GAME_MAIN_CAMERA>().setSpectorMode(true);
                    }
                }
            }
        }
        void Update()
        {
            if (onEnable)
            {
                if (Input.mousePosition.x < (Screen.width * 0.4f))
                {
                    float num5 = (-((((Screen.width * 0.4f) - Input.mousePosition.x) / ((float)Screen.width)) * 0.4f) * this.speedCamera) * 150f;
                    this.baseT.Rotate(0f, num5, 0f, Space.World);
                }
                else if (Input.mousePosition.x > (Screen.width * 0.6f))
                {
                    float num5 = ((((Input.mousePosition.x - (Screen.width * 0.6f)) / ((float)Screen.width)) * 0.4f) * this.speedCamera) * 150f;
                    this.baseT.Rotate(0f, num5, 0f, Space.World);
                }
                if (Input.mousePosition.y < (Screen.height * 0.4f))
                {
                    float num5 = (((((Screen.height * 0.4f) - Input.mousePosition.y) / ((float)Screen.height)) * 0.4f) * this.speedCamera) * 150f;
                    this.baseT.Rotate(num5, 0f, 0f, Space.Self);
                }
                else if (Input.mousePosition.y > (Screen.height * 0.6f))
                {
                    float num5 = (-(((Input.mousePosition.y - (Screen.height * 0.6f)) / ((float)Screen.height)) * 0.4f) * this.speedCamera) * 150f;
                    this.baseT.Rotate(num5, 0f, 0f, Space.Self);
                }

                if (Input.GetKey(buttonUP))
                {
                    baseT.position += (Vector3)((baseT.forward * (speed_button * timered["up"].valle(100))) * Time.deltaTime);
                }
                else if (Input.GetKey(buttonDOWN))
                {
                    baseT.position -= (Vector3)((baseT.forward * (speed_button * timered["down"].valle(100))) * Time.deltaTime);
                }
                else if (multi_button)
                {
                    if (timered["up"].valueLocal > 0)
                    {
                        baseT.position += (Vector3)((baseT.forward * (speed_button * timered["up"].valueLocal)) * Time.deltaTime);
                        timered["up"].stop();
                    }
                    if (timered["down"].valueLocal > 0)
                    {
                        baseT.position -= (Vector3)((baseT.forward * (speed_button * timered["down"].valueLocal)) * Time.deltaTime);
                        timered["down"].stop();
                    }


                }
                if (Input.GetKey(buttonLEFT))
                {
                    baseT.position -= (Vector3)((baseT.right * (speed_button * timered["left"].valle(100))) * Time.deltaTime);
                }
                else if (Input.GetKey(buttonRIGHT))
                {
                    baseT.position += (Vector3)((baseT.right * (speed_button * timered["right"].valle(100))) * Time.deltaTime);
                }
                else if (multi_button)
                {
                    if (timered["left"].valueLocal > 0)
                    {
                        baseT.position -= (Vector3)((baseT.right * (speed_button * timered["left"].valueLocal)) * Time.deltaTime);
                        timered["left"].stop();
                    }
                    if (timered["right"].valueLocal > 0)
                    {
                        baseT.position += (Vector3)((baseT.right * (speed_button * timered["right"].valueLocal)) * Time.deltaTime);
                        timered["right"].stop();
                    }
                }
            }
        }
        void LateUpdate()
        {
            if (Input.GetKeyDown(buttonEnable) && Settings.SpecMode) { onEnable = !onEnable; Activate(onEnable); }
            if (Input.GetKeyDown(buttonShowGUI) && Settings.SpecMode) { showGui = !showGui; }
        }
        void OnGUI()
        {
            if (onEnable && showGui)
            {
                Rect rect = new Rect(Screen.width - 250, Screen.height - 130, 250, 130);
                float labelwidth = 100f;
                GUILayout.BeginArea(rect, GUI.skin.box);
                GUILayout.Label("Show GUI:" + buttonShowGUI.ToString() + " Disable mode:" + buttonEnable.ToString() + "\nMove: Up:" + buttonUP + " Down:" + buttonDOWN + " Left:" + buttonLEFT + " Right:" + buttonRIGHT);
                GUILayout.BeginHorizontal();
                GUILayout.Label("Speed camera:", GUILayout.Width(labelwidth));
                speed_camera = GUILayout.HorizontalSlider(speed_camera, 0.01f, 0.2f);
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("Speed button:", GUILayout.Width(labelwidth));
                speed_button = GUILayout.HorizontalSlider(speed_button, 0.01f, 1f);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                multi_button = GUILayout.Toggle(multi_button, "Smooth button");
                if (GUILayout.Button("Save"))
                {
                    Save_Load_Settings(true);
                }
                GUILayout.EndHorizontal();
                GUILayout.EndArea();
            }
        }
        class Timered
        {
            readonly static float time_static = 0.1f;
            float dinamic_time = 0f;
            public float valueLocal = 0f;
            readonly static float multiplication = 1.2f;
            float end_value = 0f;
            void Update()
            {
                if (end_value > valueLocal)
                {
                    if (valueLocal < 0)
                    {
                        valueLocal = 0;
                    }
                    dinamic_time += Time.deltaTime;
                    if (dinamic_time > time_static)
                    {
                        dinamic_time = 0f;
                        valueLocal = (multiplication + valueLocal) * multiplication;
                    }
                }
            }
            public float valle(float set_value)
            {
                if (multi_button)
                {
                    end_value = set_value;
                    Update();
                    return valueLocal;
                }
                return set_value;
            }
            public void stop()
            {
                if (valueLocal > 0)
                {
                    if (valueLocal < 0)
                    {
                        valueLocal = 0;
                    }
                    dinamic_time += Time.deltaTime;
                    if (dinamic_time > time_static)
                    {
                        dinamic_time = 0f;
                        valueLocal = (valueLocal - multiplication) / multiplication;
                    }
                }
            }
        }
    }
}
