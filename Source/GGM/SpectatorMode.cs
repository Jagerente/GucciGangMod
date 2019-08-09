using System;
using System.Collections.Generic;
using System.IO;
using GGM.Config;
using UnityEngine;

namespace GGM
{
    public class SpectatorMode : MonoBehaviour
    {
        private readonly string _path = Application.dataPath + "/SaveCameraSetting.cfg";
        public static SpectatorMode Instance;
        private const KeyCode _buttonEnable = KeyCode.M;
        private const KeyCode _buttonShowGUI = KeyCode.N;
        private const KeyCode _buttonUp = KeyCode.W;
        private const KeyCode _buttonDown = KeyCode.S;
        private const KeyCode _buttonRight = KeyCode.D;
        private const KeyCode _buttonLeft = KeyCode.A;
        public static bool ONEnable;
        private static bool _showGui = true;
        private static bool _multiButton = true;
        private Transform _baseT;
        private float _speedCamera = 0.05f;
        private float _speedButton = 0.2f;
        private Dictionary<string, Timered> _timered;
        public static bool SpecMode;

        private float SpeedCamera => _speedCamera * Time.deltaTime * 62f;

        private void Awake()
        {
            Instance = this;
            _timered = new Dictionary<string, Timered>();
            _timered.Add("up", new Timered());
            _timered.Add("down", new Timered());
            _timered.Add("right", new Timered());
            _timered.Add("left", new Timered());
            Save_Load_Settings(false);
        }

        private void Save_Load_Settings(bool flag)
        {
            if (flag)
            {
                var str = "smooth:" + _multiButton + "\nspeedbutton:" + _speedButton + "\nspeedcamera:" + _speedCamera;
                File.WriteAllText(_path, str, System.Text.Encoding.UTF8);
            }
            else
            {
                var info = new FileInfo(_path);
                if (info.Exists)
                {
                    var lines = File.ReadAllLines(_path, System.Text.Encoding.UTF8);
                    foreach (var str in lines)
                    {
                        if (str.Trim() != "" && str.Contains(":"))
                        {
                            var keys = str.Split(':');
                            var key = keys[0].Trim(); var value = keys[1].Trim();
                            if (key == "smooth")
                            {
                                _multiButton = value == "True";
                            }
                            else if (key == "speedbutton")
                            {
                                _speedButton = Convert.ToSingle(value);
                            }
                            else if (key == "speedcamera")
                            {
                                _speedCamera = Convert.ToSingle(value);
                            }
                        }
                    }
                }
            }
        }

        private void Activate(bool flag)
        {
            if (IN_GAME_MAIN_CAMERA.gametype != GAMETYPE.STOP)
            {
                var gm = GameObject.Find("MainCamera");
                if (gm != null)
                {
                    _baseT = gm.transform;
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

        private void Update()
        {
            if (ONEnable)
            {
                if (Input.mousePosition.x < Screen.width * 0.4f)
                {
                    var num5 = -((Screen.width * 0.4f - Input.mousePosition.x) / Screen.width * 0.4f) * SpeedCamera * 150f;
                    _baseT.Rotate(0f, num5, 0f, Space.World);
                }
                else if (Input.mousePosition.x > Screen.width * 0.6f)
                {
                    var num5 = (Input.mousePosition.x - Screen.width * 0.6f) / Screen.width * 0.4f * SpeedCamera * 150f;
                    _baseT.Rotate(0f, num5, 0f, Space.World);
                }
                if (Input.mousePosition.y < Screen.height * 0.4f)
                {
                    var num5 = (Screen.height * 0.4f - Input.mousePosition.y) / Screen.height * 0.4f * SpeedCamera * 150f;
                    _baseT.Rotate(num5, 0f, 0f, Space.Self);
                }
                else if (Input.mousePosition.y > Screen.height * 0.6f)
                {
                    var num5 = -((Input.mousePosition.y - Screen.height * 0.6f) / Screen.height * 0.4f) * SpeedCamera * 150f;
                    _baseT.Rotate(num5, 0f, 0f, Space.Self);
                }

                if (Input.GetKey(_buttonUp))
                {
                    _baseT.position += _baseT.forward * (_speedButton * _timered["up"].Value(100)) * Time.deltaTime;
                }
                else if (Input.GetKey(_buttonDown))
                {
                    _baseT.position -= _baseT.forward * (_speedButton * _timered["down"].Value(100)) * Time.deltaTime;
                }
                else if (_multiButton)
                {
                    if (_timered["up"].ValueLocal > 0)
                    {
                        _baseT.position += _baseT.forward * (_speedButton * _timered["up"].ValueLocal) * Time.deltaTime;
                        _timered["up"].Stop();
                    }
                    if (_timered["down"].ValueLocal > 0)
                    {
                        _baseT.position -= _baseT.forward * (_speedButton * _timered["down"].ValueLocal) * Time.deltaTime;
                        _timered["down"].Stop();
                    }
                }
                if (Input.GetKey(_buttonLeft))
                {
                    _baseT.position -= _baseT.right * (_speedButton * _timered["left"].Value(100)) * Time.deltaTime;
                }
                else if (Input.GetKey(_buttonRight))
                {
                    _baseT.position += _baseT.right * (_speedButton * _timered["right"].Value(100)) * Time.deltaTime;
                }
                else if (_multiButton)
                {
                    if (_timered["left"].ValueLocal > 0)
                    {
                        _baseT.position -= _baseT.right * (_speedButton * _timered["left"].ValueLocal) * Time.deltaTime;
                        _timered["left"].Stop();
                    }
                    if (_timered["right"].ValueLocal > 0)
                    {
                        _baseT.position += _baseT.right * (_speedButton * _timered["right"].ValueLocal) * Time.deltaTime;
                        _timered["right"].Stop();
                    }
                }
            }
        }

        private void LateUpdate()
        {
            if (Input.GetKeyDown(_buttonEnable) && SpecMode) { ONEnable = !ONEnable; Activate(ONEnable); }
            if (Input.GetKeyDown(_buttonShowGUI) && SpecMode) { _showGui = !_showGui; }
        }

        private void OnGUI()
        {
            if (ONEnable && _showGui)
            {
                var rect = new Rect(Screen.width - 250, Screen.height - 130, 250, 130);
                var labelwidth = 100f;
                GUILayout.BeginArea(rect, UnityEngine.GUI.skin.box);
                GUILayout.Label("Show GUI:" + _buttonShowGUI.ToString() + " Disable mode:" + _buttonEnable.ToString() + "\nMove: Up:" + _buttonUp + " Down:" + _buttonDown + " Left:" + _buttonLeft + " Right:" + _buttonRight);
                GUILayout.BeginHorizontal();
                GUILayout.Label("Speed camera:", GUILayout.Width(labelwidth));
                _speedCamera = GUILayout.HorizontalSlider(_speedCamera, 0.01f, 0.2f);
                GUILayout.EndHorizontal();
                GUILayout.BeginHorizontal();
                GUILayout.Label("Speed button:", GUILayout.Width(labelwidth));
                _speedButton = GUILayout.HorizontalSlider(_speedButton, 0.01f, 1f);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                _multiButton = GUILayout.Toggle(_multiButton, "Smooth button");
                if (GUILayout.Button("Save"))
                {
                    Save_Load_Settings(true);
                }
                GUILayout.EndHorizontal();
                GUILayout.EndArea();
            }
        }

        private class Timered
        {
            private const float _timeStatic = 0.1f;
            private float _dynamicTime;
            public float ValueLocal;
            private static readonly float _multiplication = 1.2f;
            private float _endValue;

            private void Update()
            {
                if (_endValue > ValueLocal)
                {
                    if (ValueLocal < 0)
                    {
                        ValueLocal = 0;
                    }
                    _dynamicTime += Time.deltaTime;
                    if (_dynamicTime > _timeStatic)
                    {
                        _dynamicTime = 0f;
                        ValueLocal = (_multiplication + ValueLocal) * _multiplication;
                    }
                }
            }

            public float Value(float setValue)
            {
                if (_multiButton)
                {
                    _endValue = setValue;
                    Update();
                    return ValueLocal;
                }
                return setValue;
            }

            public void Stop()
            {
                if (ValueLocal > 0)
                {
                    if (ValueLocal < 0)
                    {
                        ValueLocal = 0;
                    }
                    _dynamicTime += Time.deltaTime;
                    if (_dynamicTime > _timeStatic)
                    {
                        _dynamicTime = 0f;
                        ValueLocal = (ValueLocal - _multiplication) / _multiplication;
                    }
                }
            }
        }
    }
}