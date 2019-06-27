using System;
using UnityEngine;

public class FengCustomInputs : MonoBehaviour
{
    public bool allowDuplicates;
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
    public KeyCode[] default_inputKeys;
    public string[] DescriptionString;
    private bool[] inputBool;
    public KeyCode[] inputKey;
    public string[] inputString;
    [HideInInspector]
    public bool[] isInput;
    [HideInInspector]
    public bool[] isInputDown;
    [HideInInspector]
    public bool[] isInputUp;
    [HideInInspector]
    public bool[] joystickActive;
    [HideInInspector]
    public string[] joystickString;
    private float lastInterval;
    public bool menuOn = true;
    public bool mouseAxisOn;
    public bool mouseButtonsOn = true;
    public GUISkin OurSkin;
    private bool[] tempjoy1;
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
            }
        }
    }

    private void drawButtons1()
    {
        var flag = false;
        for (var i = 0; i < DescriptionString.Length; i++)
        {
            if (!joystickActive[i] && inputKey[i] == KeyCode.None)
            {
                joystickString[i] = "#";
            }
            var flag2 = inputBool[i];
            if (Event.current.type == EventType.KeyDown && inputBool[i])
            {
                inputKey[i] = Event.current.keyCode;
                inputBool[i] = false;
                inputString[i] = inputKey[i].ToString();
                joystickActive[i] = false;
                joystickString[i] = "#";
                saveInputs();
                checDoubles(inputKey[i], i, 1);
            }
            if (mouseButtonsOn)
            {
                var num2 = 0x143;
                for (var k = 0; k < 6; k++)
                {
                    if (Input.GetMouseButton(k) && inputBool[i])
                    {
                        num2 += k;
                        inputKey[i] = (KeyCode) num2;
                        inputBool[i] = false;
                        inputString[i] = inputKey[i].ToString();
                        joystickActive[i] = false;
                        joystickString[i] = "#";
                        saveInputs();
                        checDoubles(inputKey[i], i, 1);
                    }
                }
            }
            for (var j = 350; j < 0x199; j++)
            {
                if (Input.GetKey((KeyCode) j) && inputBool[i])
                {
                    inputKey[i] = (KeyCode) j;
                    inputBool[i] = false;
                    inputString[i] = inputKey[i].ToString();
                    joystickActive[i] = false;
                    joystickString[i] = "#";
                    saveInputs();
                    checDoubles(inputKey[i], i, 1);
                }
            }
            if (mouseAxisOn)
            {
                if (Input.GetAxis("MouseUp") == 1f && inputBool[i])
                {
                    inputKey[i] = KeyCode.None;
                    inputBool[i] = false;
                    joystickActive[i] = true;
                    joystickString[i] = "MouseUp";
                    inputString[i] = "Mouse Up";
                    saveInputs();
                    checDoubleAxis(joystickString[i], i, 1);
                }
                if (Input.GetAxis("MouseDown") == 1f && inputBool[i])
                {
                    inputKey[i] = KeyCode.None;
                    inputBool[i] = false;
                    joystickActive[i] = true;
                    joystickString[i] = "MouseDown";
                    inputString[i] = "Mouse Down";
                    saveInputs();
                    checDoubleAxis(joystickString[i], i, 1);
                }
                if (Input.GetAxis("MouseLeft") == 1f && inputBool[i])
                {
                    inputKey[i] = KeyCode.None;
                    inputBool[i] = false;
                    joystickActive[i] = true;
                    joystickString[i] = "MouseLeft";
                    inputBool[i] = false;
                    inputString[i] = "Mouse Left";
                    saveInputs();
                    checDoubleAxis(joystickString[i], i, 1);
                }
                if (Input.GetAxis("MouseRight") == 1f && inputBool[i])
                {
                    inputKey[i] = KeyCode.None;
                    inputBool[i] = false;
                    joystickActive[i] = true;
                    joystickString[i] = "MouseRight";
                    inputString[i] = "Mouse Right";
                    saveInputs();
                    checDoubleAxis(joystickString[i], i, 1);
                }
            }
            if (mouseButtonsOn)
            {
                if (Input.GetAxis("MouseScrollUp") > 0f && inputBool[i])
                {
                    inputKey[i] = KeyCode.None;
                    inputBool[i] = false;
                    joystickActive[i] = true;
                    joystickString[i] = "MouseScrollUp";
                    inputBool[i] = false;
                    inputString[i] = "Mouse scroll Up";
                    saveInputs();
                    checDoubleAxis(joystickString[i], i, 1);
                }
                if (Input.GetAxis("MouseScrollDown") > 0f && inputBool[i])
                {
                    inputKey[i] = KeyCode.None;
                    inputBool[i] = false;
                    joystickActive[i] = true;
                    joystickString[i] = "MouseScrollDown";
                    inputBool[i] = false;
                    inputString[i] = "Mouse scroll Down";
                    saveInputs();
                    checDoubleAxis(joystickString[i], i, 1);
                }
            }
            if (Input.GetAxis("JoystickUp") > 0.5f && inputBool[i])
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "JoystickUp";
                inputString[i] = "Joystick Up";
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("JoystickDown") > 0.5f && inputBool[i])
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "JoystickDown";
                inputString[i] = "Joystick Down";
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("JoystickLeft") > 0.5f && inputBool[i])
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "JoystickLeft";
                inputString[i] = "Joystick Left";
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("JoystickRight") > 0.5f && inputBool[i])
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "JoystickRight";
                inputString[i] = "Joystick Right";
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_3a") > 0.8f && inputBool[i])
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_3a";
                inputString[i] = "Joystick Axis 3 +";
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_3b") > 0.8f && inputBool[i])
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_3b";
                inputString[i] = "Joystick Axis 3 -";
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_4a") > 0.8f && inputBool[i])
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_4a";
                inputString[i] = "Joystick Axis 4 +";
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_4b") > 0.8f && inputBool[i])
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_4b";
                inputString[i] = "Joystick Axis 4 -";
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_5b") > 0.8f && inputBool[i])
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_5b";
                inputString[i] = "Joystick Axis 5 -";
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_6b") > 0.8f && inputBool[i])
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_6b";
                inputString[i] = "Joystick Axis 6 -";
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_7a") > 0.8f && inputBool[i])
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_7a";
                inputString[i] = "Joystick Axis 7 +";
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_7b") > 0.8f && inputBool[i])
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_7b";
                inputString[i] = "Joystick Axis 7 -";
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_8a") > 0.8f && inputBool[i])
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_8a";
                inputString[i] = "Joystick Axis 8 +";
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (Input.GetAxis("Joystick_8b") > 0.8f && inputBool[i])
            {
                inputKey[i] = KeyCode.None;
                inputBool[i] = false;
                joystickActive[i] = true;
                joystickString[i] = "Joystick_8b";
                inputString[i] = "Joystick Axis 8 -";
                saveInputs();
                checDoubleAxis(joystickString[i], i, 1);
            }
            if (flag2 != inputBool[i])
            {
                flag = true;
            }
        }
        if (flag)
        {
            showKeyMap();
        }
    }

    public string getKeyRC(int i)
    {
        return inputString[i];
    }

    private void inputSetBools()
    {
        for (var i = 0; i < DescriptionString.Length; i++)
        {
            if (Input.GetKey(inputKey[i]) || joystickActive[i] && Input.GetAxis(joystickString[i]) > 0.95f)
            {
                isInput[i] = true;
            }
            else
            {
                isInput[i] = false;
            }
            if (Input.GetKeyDown(inputKey[i]))
            {
                isInputDown[i] = true;
            }
            else
            {
                isInputDown[i] = false;
            }
            if (joystickActive[i] && Input.GetAxis(joystickString[i]) > 0.95f)
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
            if (!tempjoy1[i] && joystickActive[i] && Input.GetAxis(joystickString[i]) < 0.1f)
            {
                isInputDown[i] = false;
                tempjoy1[i] = true;
            }
            if (Input.GetKeyUp(inputKey[i]))
            {
                isInputUp[i] = true;
            }
            else
            {
                isInputUp[i] = false;
            }
        }
    }

    public void justUPDATEME()
    {
        Update();
    }

    private void loadConfig()
    {
        var str = PlayerPrefs.GetString("KeyCodes");
        var str2 = PlayerPrefs.GetString("Joystick_input");
        var str3 = PlayerPrefs.GetString("Names_input");
        char[] separator = { '*' };
        var strArray = str.Split(separator);
        char[] chArray2 = { '*' };
        joystickString = str2.Split(chArray2);
        char[] chArray3 = { '*' };
        inputString = str3.Split(chArray3);
        for (var i = 0; i < DescriptionString.Length; i++)
        {
            int num2;
            int.TryParse(strArray[i], out num2);
            inputKey[i] = (KeyCode) num2;
            if (joystickString[i] == "#")
            {
                joystickActive[i] = false;
            }
            else
            {
                joystickActive[i] = true;
            }
        }
    }

    private void OnGUI()
    {
        if (menuOn)
        {
            drawButtons1();
        }
    }

    private void reset2defaults()
    {
        if (default_inputKeys.Length != DescriptionString.Length)
        {
            default_inputKeys = new KeyCode[DescriptionString.Length];
        }
        var str = string.Empty;
        var str2 = string.Empty;
        var str3 = string.Empty;
        for (var i = DescriptionString.Length - 1; i > -1; i--)
        {
            str = (int) default_inputKeys[i] + "*" + str;
            str2 = str2 + "#*";
            str3 = default_inputKeys[i] + "*" + str3;
            PlayerPrefs.SetString("KeyCodes", str);
            PlayerPrefs.SetString("Joystick_input", str2);
            PlayerPrefs.SetString("Names_input", str3);
            PlayerPrefs.SetInt("KeyLength", DescriptionString.Length);
        }
    }

    private void saveInputs()
    {
        var str = string.Empty;
        var str2 = string.Empty;
        var str3 = string.Empty;
        for (var i = DescriptionString.Length - 1; i > -1; i--)
        {
            str = (int) inputKey[i] + "*" + str;
            str2 = joystickString[i] + "*" + str2;
            str3 = inputString[i] + "*" + str3;
        }
        PlayerPrefs.SetString("KeyCodes", str);
        PlayerPrefs.SetString("Joystick_input", str2);
        PlayerPrefs.SetString("Names_input", str3);
        PlayerPrefs.SetInt("KeyLength", DescriptionString.Length);
    }

    public void setKeyRC(int i, string setting)
    {
        if (setting == "Scroll Up" || setting == "Scroll Down")
        {
            if (setting == "Scroll Up")
            {
                joystickString[i] = "MouseScrollUp";
                inputString[i] = "Mouse scroll Up";
            }
            else if (setting == "Scroll Down")
            {
                joystickString[i] = "MouseScrollDown";
                inputString[i] = "Mouse scroll Down";
            }
            inputKey[i] = KeyCode.None;
            inputBool[i] = false;
            joystickActive[i] = true;
            inputBool[i] = false;
            saveInputs();
            checDoubleAxis(joystickString[i], i, 1);
        }
        else
        {
            inputKey[i] = (KeyCode) Enum.Parse(typeof(KeyCode), setting);
            inputBool[i] = false;
            inputString[i] = inputKey[i].ToString();
            joystickActive[i] = false;
            joystickString[i] = "#";
            saveInputs();
            checDoubles(inputKey[i], i, 1);
        }
    }

    public void setNameRC(int i, string str)
    {
        inputString[i] = str;
    }

    public void setToDefault()
    {
        reset2defaults();
        loadConfig();
        saveInputs();
        PlayerPrefs.SetFloat("MouseSensitivity", 0.5f);
        PlayerPrefs.SetString("version", UIMainReferences.ServerKey);
        PlayerPrefs.SetInt("invertMouseY", 1);
        PlayerPrefs.SetInt("cameraTilt", 1);
        PlayerPrefs.SetFloat("GameQuality", 0.9f);
    }

    public void showKeyMap()
    {
        for (var i = 0; i < DescriptionString.Length; i++)
        {
            if (GGM.Caching.GameObjectCache.Find("CInput" + i) != null)
            {
                GGM.Caching.GameObjectCache.Find("CInput" + i).transform.Find("Label").gameObject.GetComponent<UILabel>().text = inputString[i];
            }
        }
        if (GGM.Caching.GameObjectCache.Find("ChangeQuality") != null)
        {
            GGM.Caching.GameObjectCache.Find("ChangeQuality").GetComponent<UISlider>().sliderValue = PlayerPrefs.GetFloat("GameQuality");
        }
        if (GGM.Caching.GameObjectCache.Find("MouseSensitivity") != null)
        {
            GGM.Caching.GameObjectCache.Find("MouseSensitivity").GetComponent<UISlider>().sliderValue = PlayerPrefs.GetFloat("MouseSensitivity");
        }
        if (GGM.Caching.GameObjectCache.Find("CheckboxSettings") != null)
        {
            GGM.Caching.GameObjectCache.Find("CheckboxSettings").GetComponent<UICheckbox>().isChecked = PlayerPrefs.GetInt("invertMouseY") != 1;
        }
        if (GGM.Caching.GameObjectCache.Find("CheckboxCameraTilt") != null)
        {
            GGM.Caching.GameObjectCache.Find("CheckboxCameraTilt").GetComponent<UICheckbox>().isChecked = PlayerPrefs.GetInt("cameraTilt") == 1;
        }
    }

    private void Start()
    {
        inputBool = new bool[DescriptionString.Length];
        inputString = new string[DescriptionString.Length];
        inputKey = new KeyCode[DescriptionString.Length];
        joystickActive = new bool[DescriptionString.Length];
        joystickString = new string[DescriptionString.Length];
        isInput = new bool[DescriptionString.Length];
        isInputDown = new bool[DescriptionString.Length];
        isInputUp = new bool[DescriptionString.Length];
        tempLength = PlayerPrefs.GetInt("KeyLength");
        tempjoy1 = new bool[DescriptionString.Length];
        if (!PlayerPrefs.HasKey("version"))
        {
            setToDefault();
        }
        tempLength = PlayerPrefs.GetInt("KeyLength");
        if (PlayerPrefs.HasKey("KeyCodes") && tempLength == DescriptionString.Length)
        {
            loadConfig();
        }
        else
        {
            setToDefault();
        }
        for (var i = 0; i < DescriptionString.Length; i++)
        {
            isInput[i] = false;
            isInputDown[i] = false;
            isInputUp[i] = false;
            tempjoy1[i] = true;
        }
    }

    public void startListening(int n)
    {
        inputBool[n] = true;
        lastInterval = Time.realtimeSinceStartup;
    }

    private void Update()
    {
        if (!menuOn)
        {
            inputSetBools();
        }
    }
}

