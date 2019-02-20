//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class CB_cameraTilt : MonoBehaviour
{
    private bool init;

    private void OnActivate(bool result)
    {
        if (!init)
        {
            init = true;
            if (PlayerPrefs.HasKey("cameraTilt"))
            {
                gameObject.GetComponent<UICheckbox>().isChecked = PlayerPrefs.GetInt("cameraTilt") == 1;
            }
            else
            {
                PlayerPrefs.SetInt("cameraTilt", 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("cameraTilt", !result ? 0 : 1);
        }
        IN_GAME_MAIN_CAMERA.cameraTilt = PlayerPrefs.GetInt("cameraTilt");
    }
}

