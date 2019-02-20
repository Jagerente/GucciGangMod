//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class CheckBoxCamera : MonoBehaviour
{
    public CAMERA_TYPE camera;

    private void OnSelectionChange(bool yes)
    {
        if (yes)
        {
            IN_GAME_MAIN_CAMERA.cameraMode = camera;
            PlayerPrefs.SetString("cameraType", camera.ToString().ToUpper());
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("cameraType"))
        {
            if (camera.ToString().ToUpper() == PlayerPrefs.GetString("cameraType").ToUpper())
            {
                GetComponent<UICheckbox>().isChecked = true;
            }
            else
            {
                GetComponent<UICheckbox>().isChecked = false;
            }
        }
    }
}

