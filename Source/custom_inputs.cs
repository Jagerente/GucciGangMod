//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class custom_inputs : MonoBehaviour
{
    public bool allowDuplicates;
    public KeyCode[] alt_default_inputKeys;
    private float AltInputBox_X = 120f;
    private bool altInputson;
    [HideInInspector]
    public float analogFeel_down;
    public float analogFeel_gravity = 0.2f;
    [HideInInspector]
    public float analogFeel_jump;
    [HideInInspector]
    public float analogFeel_left;
    [HideInInspector]
    public float analogFeel_right;
    public float analogFeel_sensitivity = 0.8f;
    [HideInInspector]
    public float analogFeel_up;
    public float Boxes_Y = 300f;
    public float BoxesMargin_Y = 30f;
    private float buttonHeight = 20f;
    public int buttonSize = 200;
    public KeyCode[] default_inputKeys;
    private float DescBox_X = -320f;
    private float DescriptionBox_X;
    public int DescriptionSize = 200;
    public string[] DescriptionString;
    private bool[] inputBool;
    private bool[] inputBool2;
    private float InputBox_X = -100f;
    private float InputBox1_X;
    private float InputBox2_X;
    private KeyCode[] inputKey;
    private KeyCode[] inputKey2;
    private string[] inputString;
    private string[] inputString2;
    [HideInInspector]
    public bool[] isInput;
    [HideInInspector]
    public bool[] isInputDown;
    [HideInInspector]
    public bool[] isInputUp;
    [HideInInspector]
    public bool[] joystickActive;
    [HideInInspector]
    public bool[] joystickActive2;
    [HideInInspector]
    public string[] joystickString;
    [HideInInspector]
    public string[] joystickString2;
    private float lastInterval;
    public bool menuOn;
    public bool mouseAxisOn;
    public bool mouseButtonsOn = true;
    public GUISkin OurSkin;
    private float resetbuttonLocX = -100f;
    public float resetbuttonLocY = 600f;
    public string resetbuttonText = "Reset to defaults";
    private float resetbuttonX;
    private bool tempbool;
    private bool[] tempjoy1;
    private bool[] tempjoy2;
    private string tempkeyPressed;
    private int tempLength;

    private void checDoubleAxis(string testAxisString, int o, int p)
    {
        if (!allowDuplicates)
        {
            for (var i = 0; i < DescriptionString.Length; i++)
            {
                if (testAxisString == joystickString[i] && (i != o || p == 2))
                {
                    inputKey[i] = KeyCode.None;
                    inputBool[i] = false;
                    inputString[i] = inputKey[i].ToString();
                    joystickActive[i] = false;
                    joystickString[i] = "#";
                    saveInputs();
                }
                if (testAxisString == joystickString2[i] && (i != o || p == 1))
                {
                    inputKey2[i] = KeyCode.None;
                    inputBool2[i] = false;
                    inputString2[i] = inputKey2[i].ToString();
                    joystickActive2[i] = false;
                    joystickString2[i] = "#";
                    saveInputs();
                }
            }
        }
    }

    private void checDoubles(KeyCode testkey, int o, int p)
    {
        if (!allowDuplicates)
        {
            for (var i = 0; i < DescriptionString.Length; i++)
            {
                if (testkey == inputKey[i] && (i != o || p == 2))
                {
                    inputKey[i] = KeyCode.None;
                    inputBool[i] = false;
                    inputString[i] = inputKey[i].ToString();
                    joystickActive[i] = false;
                    joystickString[i] = "#";
                    saveInputs();
                }
                if (testkey == inputKey2[i] && (i != o || p == 1))
                {
                    inputKey2[i] = KeyCode.None;
                    inputBool2[i] = false;
                    inputString2[i] = inputKey2[i].ToString();
                    joystickActive2[i] = false;
                    joystickString2[i] = "#";
                    saveInputs();
                }
            }
        }
    }

    private void drawButtons1()
    {
        var y = Boxes_Y;
        var x = Input.mousePosition.x;
        var num3 = Input.mousePosition.y;
        var point = GUI.matrix.inverse.MultiplyPoint3x4(new Vector3(x, Screen.height - num3, 1f));
        GUI.skin = OurSkin;
        GUI.Box(new Rect(0f, 0f, Screen.width, Screen.height), string.Empty);
        GUI.Box(new Rect(60f, 60f, Screen.width - 120, Screen.height - 120), string.Empty, "window");
        GUI.Label(new Rect(DescriptionBox_X, y - 10f, DescriptionSize, buttonHeight), "name", "textfield");
        GUI.Label(new Rect(InputBox1_X, y - 10f, DescriptionSize, buttonHeight), "input", "textfield");
        GUI.Label(new Rect(InputBox2_X, y - 10f, DescriptionSize, buttonHeight), "alt input", "textfield");
        for (var i = 0; i < DescriptionString.Length; i++)
        {
            y += BoxesMargin_Y;
            GUI.Label(new Rect(DescriptionBox_X, y, DescriptionSize, buttonHeight), DescriptionString[i], "box");
            var position = new Rect(InputBox1_X, y, buttonSize, buttonHeight);
            GUI.Button(position, inputString[i]);
            if (!joystickActive[i] && inputKey[i] == KeyCode.None)
            {
                joystickString[i] = "#";
            }
            if (inputBool[i])
            {
                GUI.Toggle(position, true, string.Empty, OurSkin.button);
            }
            if (position.Contains(point) && Input.GetMouseButtonUp(0) && !tempbool)
            {
                tempbool = true;
                inputBool[i] = true;
                lastInterval = Time.realtimeSinceStartup;
            }
            if (GUI.Button(new Rect(resetbuttonX, resetbuttonLocY, buttonSize, buttonHeight), resetbuttonText) && Input.GetMouseButtonUp(0))
            {
                PlayerPrefs.DeleteAll();
                reset2defaults();
                loadConfig();
                saveInputs();
            }
            if (Event.current.type == EventType.KeyDown && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey[i] = Event.current.keyCode;
                inputBool[i] = false;
                inputString[i] = inputKey[i].ToString();
                tempbool = false;
                joystickActive[i] = false;
                joystickString[i] = "#";
                saveInputs();
                checDoubles(inputKey[i], i, 1);
            }
            if (mouseButtonsOn)
            {
                var num5 = 323;
                for (var k = 0; k < 6; k++)
                {
                    if (Input.GetMouseButton(k) && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
                    {
                        num5 += k;
                        inputKey[i] = (KeyCode) num5;
                        inputBool[i] = false;
                        inputString[i] = inputKey[i].ToString();
                        joystickActive[i] = false;
                        joystickString[i] = "#";
                        saveInputs();
                        checDoubles(inputKey[i], i, 1);
                    }
                }
            }
            for (var j = 350; j < 409; j++)
            {
                if (Input.GetKey((KeyCode) j) && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
                {
                    inputKey[i] = (KeyCode) j;
                    inputBool[i] = false;
                    inputString[i] = inputKey[i].ToString();
                    tempbool = false;
                    joystickActive[i] = false;
                    joystickString[i] = "#";
                    saveInputs();
                    checDoubles(inputKey[i], i, 1);
                }
            }
            if (mouseAxisOn)
            {
                if (Input.GetAxis("MouseUp") == 1f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
                {
                    inputKey[i] = KeyCode.None;
                    inputBool[i] = false;
                    joystickActive[i] = true;
                    joystickString[i] = "MouseUp";
                    inputString[i] = "Mouse Up";
                    tempbool = false;
                    saveInputs();
                    checDoubleAxis(joystickString[i], i, 1);
                }
                if (Input.GetAxis("MouseDown") == 1f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
                {
                    inputKey[i] = KeyCode.None;
                    inputBool[i] = false;
                    joystickActive[i] = true;
                    joystickString[i] = "MouseDown";
                    inputString[i] = "Mouse Down";
                    tempbool = false;
                    saveInputs();
                    checDoubleAxis(joystickString[i], i, 1);
                }
                if (Input.GetAxis("MouseLeft") == 1f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
                {
                    inputKey[i] = KeyCode.None;
                    inputBool[i] = false;
                    joystickActive[i] = true;
                    joystickString[i] = "MouseLeft";
                    inputBool[i] = false;
                    inputString[i] = "Mouse Left";
                    tempbool = false;
                    saveInputs();
                    checDoubleAxis(joystickString[i], i, 1);
                }
                if (Input.GetAxis("MouseRight") == 1f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
                {
                    inputKey[i] = KeyCode.None;
                    inputBool[i] = false;
                    joystickActive[i] = true;
                    joystickString[i] = "MouseRight";
                    inputString[i] = "Mouse Right";
                    tempbool = false;
                    saveInputs();
                    checDoubleAxis(joystickString[i], i, 1);
                }
            }
            if (mouseButtonsOn)
            {
                if (Input.GetAxis("MouseScrollUp") > 0f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
                {
                    inputKey[i] = KeyCode.None;
                    inputBool[i] = false;
                    joystickActive[i] = true;
                    joystickString[i] = "MouseScrollUp";
                    inputBool[i] = false;
                    inputString[i] = "Mouse scroll Up";
                    tempbool = false;
                    saveInputs();
                    checDoubleAxis(joystickString[i], i, 1);
                }
                if (Input.GetAxis("MouseScrollDown") > 0f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
                {
                    inputKey[i] = KeyCode.None;
                    inputBool[i] = false;
                    joystickActive[i] = true;
                    joystickString[i] = "MouseScrollDown";
                    inputBool[i] = false;
                    inputString[i] = "Mouse scroll Down";
                    tempbool = false;
                    saveInputs();
                    checDoubleAxis(joystickString[i], i, 1);
                }
            }
            if (Input.GetAxis("JoystickUp") > 0.5f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "JoystickUp";
                inputString[i] = "Joystick Up";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("JoystickDown") > 0.5f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "JoystickDown";
                inputString[i] = "Joystick Down";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("JoystickLeft") > 0.5f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "JoystickLeft";
                inputString[i] = "Joystick Left";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("JoystickRight") > 0.5f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "JoystickRight";
                inputString[i] = "Joystick Right";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_3a") > 0.8f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_3a";
                inputString[i] = "Joystick Axis 3 +";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_3b") > 0.8f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_3b";
                inputString[i] = "Joystick Axis 3 -";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_4a") > 0.8f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_4a";
                inputString[i] = "Joystick Axis 4 +";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_4b") > 0.8f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_4b";
                inputString[i] = "Joystick Axis 4 -";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_5b") > 0.8f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_5b";
                inputString[i] = "Joystick Axis 5 -";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_6b") > 0.8f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_6b";
                inputString[i] = "Joystick Axis 6 -";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_7a") > 0.8f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_7a";
                inputString[i] = "Joystick Axis 7 +";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_7b") > 0.8f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_7b";
                inputString[i] = "Joystick Axis 7 -";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_8a") > 0.8f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_8a";
                inputString[i] = "Joystick Axis 8 +";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_8b") > 0.8f && inputBool[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_8b";
                inputString[i] = "Joystick Axis 8 -";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
        }
    }

    private void drawButtons2()
    {
        var y = Boxes_Y;
        var x = Input.mousePosition.x;
        var num3 = Input.mousePosition.y;
        var point = GUI.matrix.inverse.MultiplyPoint3x4(new Vector3(x, Screen.height - num3, 1f));
        GUI.skin = OurSkin;
        for (var i = 0; i < DescriptionString.Length; i++)
        {
            y += BoxesMargin_Y;
            var position = new Rect(InputBox2_X, y, buttonSize, buttonHeight);
            GUI.Button(position, inputString2[i]);
            if (!joystickActive2[i] && inputKey2[i] == KeyCode.None)
            {
                joystickString2[i] = "#";
            }
            if (inputBool2[i])
            {
                GUI.Toggle(position, true, string.Empty, OurSkin.button);
            }
            if (position.Contains(point) && Input.GetMouseButtonUp(0) && !tempbool)
            {
                tempbool = true;
                inputBool2[i] = true;
                lastInterval = Time.realtimeSinceStartup;
            }
            if (Event.current.type == EventType.KeyDown && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey2[i] = Event.current.keyCode;
                inputBool2[i] = false;
                inputString2[i] = inputKey2[i].ToString();
                tempbool = false;
                joystickActive2[i] = false;
                joystickString2[i] = "#";
                saveInputs();
                checDoubles(inputKey2[i], i, 2);
            }
            if (mouseButtonsOn)
            {
                var num5 = 323;
                for (var k = 0; k < 6; k++)
                {
                    if (Input.GetMouseButton(k) && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
                    {
                        num5 += k;
                        inputKey2[i] = (KeyCode) num5;
                        inputBool2[i] = false;
                        inputString2[i] = inputKey2[i].ToString();
                        joystickActive2[i] = false;
                        joystickString2[i] = "#";
                        saveInputs();
                        checDoubles(inputKey2[i], i, 2);
                    }
                }
            }
            for (var j = 350; j < 409; j++)
            {
                if (Input.GetKey((KeyCode) j) && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
                {
                    inputKey2[i] = (KeyCode) j;
                    inputBool2[i] = false;
                    inputString2[i] = inputKey2[i].ToString();
                    tempbool = false;
                    joystickActive2[i] = false;
                    joystickString2[i] = "#";
                    saveInputs();
                    checDoubles(inputKey2[i], i, 2);
                }
            }
            if (mouseAxisOn)
            {
                if (Input.GetAxis("MouseUp") == 1f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
                {
                    inputKey2[i] = KeyCode.None;
                    inputBool2[i] = false;
                    joystickActive2[i] = true;
                    joystickString2[i] = "MouseUp";
                    inputString2[i] = "Mouse Up";
                    tempbool = false;
                    saveInputs();
                    checDoubleAxis(joystickString2[i], i, 2);
                }
                if (Input.GetAxis("MouseDown") == 1f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
                {
                    inputKey2[i] = KeyCode.None;
                    inputBool2[i] = false;
                    joystickActive2[i] = true;
                    joystickString2[i] = "MouseDown";
                    inputString2[i] = "Mouse Down";
                    tempbool = false;
                    saveInputs();
                    checDoubleAxis(joystickString2[i], i, 2);
                }
                if (Input.GetAxis("MouseLeft") == 1f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
                {
                    inputKey2[i] = KeyCode.None;
                    inputBool2[i] = false;
                    joystickActive2[i] = true;
                    joystickString2[i] = "MouseLeft";
                    inputBool2[i] = false;
                    inputString2[i] = "Mouse Left";
                    tempbool = false;
                    saveInputs();
                    checDoubleAxis(joystickString2[i], i, 2);
                }
                if (Input.GetAxis("MouseRight") == 1f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
                {
                    inputKey2[i] = KeyCode.None;
                    inputBool2[i] = false;
                    joystickActive2[i] = true;
                    joystickString2[i] = "MouseRight";
                    inputString2[i] = "Mouse Right";
                    tempbool = false;
                    saveInputs();
                    checDoubleAxis(joystickString2[i], i, 2);
                }
            }
            if (mouseButtonsOn)
            {
                if (Input.GetAxis("MouseScrollUp") > 0f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
                {
                    inputKey2[i] = KeyCode.None;
                    inputBool2[i] = false;
                    joystickActive2[i] = true;
                    joystickString2[i] = "MouseScrollUp";
                    inputBool2[i] = false;
                    inputString2[i] = "Mouse scroll Up";
                    tempbool = false;
                    saveInputs();
                    checDoubleAxis(joystickString2[i], i, 2);
                }
                if (Input.GetAxis("MouseScrollDown") > 0f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
                {
                    inputKey2[i] = KeyCode.None;
                    inputBool2[i] = false;
                    joystickActive2[i] = true;
                    joystickString2[i] = "MouseScrollDown";
                    inputBool2[i] = false;
                    inputString2[i] = "Mouse scroll Down";
                    tempbool = false;
                    saveInputs();
                    checDoubleAxis(joystickString2[i], i, 2);
                }
            }
            if (Input.GetAxis("JoystickUp") > 0.5f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey2[i] = KeyCode.None;
                inputBool2[i] = false;
                joystickActive2[i] = true;
                joystickString2[i] = "JoystickUp";
                inputString2[i] = "Joystick Up";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString2[i], i, 2);
            }
            if (Input.GetAxis("JoystickDown") > 0.5f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey2[i] = KeyCode.None;
                inputBool2[i] = false;
                joystickActive2[i] = true;
                joystickString2[i] = "JoystickDown";
                inputString2[i] = "Joystick Down";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString2[i], i, 2);
            }
            if (Input.GetAxis("JoystickLeft") > 0.5f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey2[i] = KeyCode.None;
                inputBool2[i] = false;
                joystickActive2[i] = true;
                joystickString2[i] = "JoystickLeft";
                inputBool2[i] = false;
                inputString2[i] = "Joystick Left";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString2[i], i, 2);
            }
            if (Input.GetAxis("JoystickRight") > 0.5f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey2[i] = KeyCode.None;
                inputBool2[i] = false;
                joystickActive2[i] = true;
                joystickString2[i] = "JoystickRight";
                inputString2[i] = "Joystick Right";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString2[i], i, 2);
            }
            if (Input.GetAxis("Joystick_3a") > 0.8f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey2[i] = KeyCode.None;
                inputBool2[i] = false;
                joystickActive2[i] = true;
                joystickString2[i] = "Joystick_3a";
                inputString2[i] = "Joystick Axis 3 +";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString2[i], i, 2);
            }
            if (Input.GetAxis("Joystick_3b") > 0.8f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey2[i] = KeyCode.None;
                inputBool2[i] = false;
                joystickActive2[i] = true;
                joystickString2[i] = "Joystick_3b";
                inputString2[i] = "Joystick Axis 3 -";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString2[i], i, 2);
            }
            if (Input.GetAxis("Joystick_4a") > 0.8f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey2[i] = KeyCode.None;
                inputBool2[i] = false;
                joystickActive2[i] = true;
                joystickString2[i] = "Joystick_4a";
                inputString2[i] = "Joystick Axis 4 +";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString2[i], i, 2);
            }
            if (Input.GetAxis("Joystick_4b") > 0.8f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey2[i] = KeyCode.None;
                inputBool2[i] = false;
                joystickActive2[i] = true;
                joystickString2[i] = "Joystick_4b";
                inputString2[i] = "Joystick Axis 4 -";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString2[i], i, 2);
            }
            if (Input.GetAxis("Joystick_5b") > 0.8f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey2[i] = KeyCode.None;
                inputBool2[i] = false;
                joystickActive2[i] = true;
                joystickString2[i] = "Joystick_5b";
                inputString2[i] = "Joystick Axis 5 -";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString2[i], i, 2);
            }
            if (Input.GetAxis("Joystick_6b") > 0.8f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey2[i] = KeyCode.None;
                inputBool2[i] = false;
                joystickActive2[i] = true;
                joystickString2[i] = "Joystick_6b";
                inputString2[i] = "Joystick Axis 6 -";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString2[i], i, 2);
            }
            if (Input.GetAxis("Joystick_7a") > 0.8f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey2[i] = KeyCode.None;
                inputBool2[i] = false;
                joystickActive2[i] = true;
                joystickString2[i] = "Joystick_7a";
                inputString2[i] = "Joystick Axis 7 +";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString2[i], i, 2);
            }
            if (Input.GetAxis("Joystick_7b") > 0.8f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey2[i] = KeyCode.None;
                inputBool2[i] = false;
                joystickActive2[i] = true;
                joystickString2[i] = "Joystick_7b";
                inputString2[i] = "Joystick Axis 7 -";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString2[i], i, 2);
            }
            if (Input.GetAxis("Joystick_8a") > 0.8f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey2[i] = KeyCode.None;
                inputBool2[i] = false;
                joystickActive2[i] = true;
                joystickString2[i] = "Joystick_8a";
                inputString2[i] = "Joystick Axis 8 +";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString2[i], i, 2);
            }
            if (Input.GetAxis("Joystick_8b") > 0.8f && inputBool2[i] && Event.current.keyCode != KeyCode.Escape)
            {
                inputKey2[i] = KeyCode.None;
                inputBool2[i] = false;
                joystickActive2[i] = true;
                joystickString2[i] = "Joystick_8b";
                inputString2[i] = "Joystick Axis 8 -";
                tempbool = false;
                saveInputs();
                checDoubleAxis(joystickString2[i], i, 2);
            }
        }
    }

    private void inputSetBools()
    {
        for (var i = 0; i < DescriptionString.Length; i++)
        {
            if (Input.GetKey(inputKey[i]) || joystickActive[i] && Input.GetAxis(joystickString[i]) > 0.95f || Input.GetKey(inputKey2[i]) || joystickActive2[i] && Input.GetAxis(joystickString2[i]) > 0.95f)
            {
                isInput[i] = true;
            }
            else
            {
                isInput[i] = false;
            }
            if (Input.GetKeyDown(inputKey[i]) || Input.GetKeyDown(inputKey2[i]))
            {
                isInputDown[i] = true;
            }
            else
            {
                isInputDown[i] = false;
            }
            if (joystickActive[i] && Input.GetAxis(joystickString[i]) > 0.95f || joystickActive2[i] && Input.GetAxis(joystickString2[i]) > 0.95f)
            {
                if (!tempjoy1[i])
                {
                    isInputDown[i] = false;
                }
                if (tempjoy1[i])
                {
                    isInputDown[i] = true;
                    tempjoy1[i] = false;
                }
            }
            if (!tempjoy1[i] && joystickActive[i] && Input.GetAxis(joystickString[i]) < 0.1f && joystickActive2[i] && Input.GetAxis(joystickString2[i]) < 0.1f)
            {
                isInputDown[i] = false;
                tempjoy1[i] = true;
            }
            if (!tempjoy1[i] && !joystickActive[i] && joystickActive2[i] && Input.GetAxis(joystickString2[i]) < 0.1f)
            {
                isInputDown[i] = false;
                tempjoy1[i] = true;
            }
            if (!tempjoy1[i] && !joystickActive2[i] && joystickActive[i] && Input.GetAxis(joystickString[i]) < 0.1f)
            {
                isInputDown[i] = false;
                tempjoy1[i] = true;
            }
            if (Input.GetKeyUp(inputKey[i]) || Input.GetKeyUp(inputKey2[i]))
            {
                isInputUp[i] = true;
            }
            else
            {
                isInputUp[i] = false;
            }
            if (joystickActive[i] && Input.GetAxis(joystickString[i]) > 0.95f || joystickActive2[i] && Input.GetAxis(joystickString2[i]) > 0.95f)
            {
                if (tempjoy2[i])
                {
                    isInputUp[i] = false;
                }
                if (!tempjoy2[i])
                {
                    isInputUp[i] = false;
                    tempjoy2[i] = true;
                }
            }
            if (tempjoy2[i] && joystickActive[i] && Input.GetAxis(joystickString[i]) < 0.1f && joystickActive2[i] && Input.GetAxis(joystickString2[i]) < 0.1f)
            {
                isInputUp[i] = true;
                tempjoy2[i] = false;
            }
            if (tempjoy2[i] && !joystickActive[i] && joystickActive2[i] && Input.GetAxis(joystickString2[i]) < 0.1f)
            {
                isInputUp[i] = true;
                tempjoy2[i] = false;
            }
            if (tempjoy2[i] && !joystickActive2[i] && joystickActive[i] && Input.GetAxis(joystickString[i]) < 0.1f)
            {
                isInputUp[i] = true;
                tempjoy2[i] = false;
            }
        }
    }

    private void loadConfig()
    {
        var str = PlayerPrefs.GetString("KeyCodes");
        var str2 = PlayerPrefs.GetString("Joystick_input");
        var str3 = PlayerPrefs.GetString("Names_input");
        var str4 = PlayerPrefs.GetString("KeyCodes2");
        var str5 = PlayerPrefs.GetString("Joystick_input2");
        var str6 = PlayerPrefs.GetString("Names_input2");
        var separator = new[] { '*' };
        var strArray = str.Split(separator);
        var chArray2 = new[] { '*' };
        joystickString = str2.Split(chArray2);
        var chArray3 = new[] { '*' };
        inputString = str3.Split(chArray3);
        var chArray4 = new[] { '*' };
        var strArray2 = str4.Split(chArray4);
        var chArray5 = new[] { '*' };
        joystickString2 = str5.Split(chArray5);
        var chArray6 = new[] { '*' };
        inputString2 = str6.Split(chArray6);
        for (var i = 0; i < DescriptionString.Length; i++)
        {
            int num2;
            int num3;
            int.TryParse(strArray[i], out num2);
            inputKey[i] = (KeyCode) num2;
            int.TryParse(strArray2[i], out num3);
            inputKey2[i] = (KeyCode) num3;
            if (joystickString[i] == "#")
            {
                joystickActive[i] = false;
            }
            else
            {
                joystickActive[i] = true;
            }
            if (joystickString2[i] == "#")
            {
                joystickActive2[i] = false;
            }
            else
            {
                joystickActive2[i] = true;
            }
        }
    }

    private void OnGUI()
    {
        if (Time.realtimeSinceStartup > lastInterval + 3f)
        {
            tempbool = false;
        }
        if (menuOn)
        {
            drawButtons1();
            if (altInputson)
            {
                drawButtons2();
            }
        }
    }

    private void reset2defaults()
    {
        if (default_inputKeys.Length != DescriptionString.Length)
        {
            default_inputKeys = new KeyCode[DescriptionString.Length];
        }
        if (alt_default_inputKeys.Length != default_inputKeys.Length)
        {
            alt_default_inputKeys = new KeyCode[default_inputKeys.Length];
        }
        var str = string.Empty;
        var str2 = string.Empty;
        var str3 = string.Empty;
        var str4 = string.Empty;
        var str5 = string.Empty;
        var str6 = string.Empty;
        for (var i = DescriptionString.Length - 1; i > -1; i--)
        {
            str = (int) default_inputKeys[i] + "*" + str;
            str2 = str2 + "#*";
            str3 = default_inputKeys[i].ToString() + "*" + str3;
            PlayerPrefs.SetString("KeyCodes", str);
            PlayerPrefs.SetString("Joystick_input", str2);
            PlayerPrefs.SetString("Names_input", str3);
            str4 = (int) alt_default_inputKeys[i] + "*" + str4;
            str5 = str5 + "#*";
            str6 = alt_default_inputKeys[i].ToString() + "*" + str6;
            PlayerPrefs.SetString("KeyCodes2", str4);
            PlayerPrefs.SetString("Joystick_input2", str5);
            PlayerPrefs.SetString("Names_input2", str6);
            PlayerPrefs.SetInt("KeyLength", DescriptionString.Length);
        }
    }

    private void saveInputs()
    {
        var str = string.Empty;
        var str2 = string.Empty;
        var str3 = string.Empty;
        var str4 = string.Empty;
        var str5 = string.Empty;
        var str6 = string.Empty;
        for (var i = DescriptionString.Length - 1; i > -1; i--)
        {
            str = (int) inputKey[i] + "*" + str;
            str2 = joystickString[i] + "*" + str2;
            str3 = inputString[i] + "*" + str3;
            str4 = (int) inputKey2[i] + "*" + str4;
            str5 = joystickString2[i] + "*" + str5;
            str6 = inputString2[i] + "*" + str6;
        }
        PlayerPrefs.SetString("KeyCodes", str);
        PlayerPrefs.SetString("Joystick_input", str2);
        PlayerPrefs.SetString("Names_input", str3);
        PlayerPrefs.SetString("KeyCodes2", str4);
        PlayerPrefs.SetString("Joystick_input2", str5);
        PlayerPrefs.SetString("Names_input2", str6);
        PlayerPrefs.SetInt("KeyLength", DescriptionString.Length);
    }

    private void Start()
    {
        if (alt_default_inputKeys.Length == default_inputKeys.Length)
        {
            altInputson = true;
        }
        inputBool = new bool[DescriptionString.Length];
        inputString = new string[DescriptionString.Length];
        inputKey = new KeyCode[DescriptionString.Length];
        joystickActive = new bool[DescriptionString.Length];
        joystickString = new string[DescriptionString.Length];
        inputBool2 = new bool[DescriptionString.Length];
        inputString2 = new string[DescriptionString.Length];
        inputKey2 = new KeyCode[DescriptionString.Length];
        joystickActive2 = new bool[DescriptionString.Length];
        joystickString2 = new string[DescriptionString.Length];
        isInput = new bool[DescriptionString.Length];
        isInputDown = new bool[DescriptionString.Length];
        isInputUp = new bool[DescriptionString.Length];
        tempLength = PlayerPrefs.GetInt("KeyLength");
        tempjoy1 = new bool[DescriptionString.Length];
        tempjoy2 = new bool[DescriptionString.Length];
        if (!PlayerPrefs.HasKey("KeyCodes") || !PlayerPrefs.HasKey("KeyCodes2"))
        {
            reset2defaults();
        }
        tempLength = PlayerPrefs.GetInt("KeyLength");
        if (PlayerPrefs.HasKey("KeyCodes") && tempLength == DescriptionString.Length)
        {
            loadConfig();
        }
        else
        {
            PlayerPrefs.DeleteAll();
            reset2defaults();
            loadConfig();
            saveInputs();
        }
        for (var i = 0; i < DescriptionString.Length; i++)
        {
            isInput[i] = false;
            isInputDown[i] = false;
            isInputUp[i] = false;
            tempjoy1[i] = true;
            tempjoy2[i] = false;
        }
    }

    private void Update()
    {
        DescriptionBox_X = Screen.width / 2 + DescBox_X;
        InputBox1_X = Screen.width / 2 + InputBox_X;
        InputBox2_X = Screen.width / 2 + AltInputBox_X;
        resetbuttonX = Screen.width / 2 + resetbuttonLocX;
        if (!menuOn)
        {
            inputSetBools();
        }
        if (Input.GetKeyDown("escape"))
        {
            if (menuOn)
            {
                Time.timeScale = 1f;
                tempbool = false;
                menuOn = false;
                saveInputs();
            }
            else
            {
                Time.timeScale = 0f;
                menuOn = true;
                Screen.showCursor = true;
                Screen.lockCursor = false;
            }
        }
    }
}

