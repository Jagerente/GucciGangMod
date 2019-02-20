//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

public class SliderMouseSensitivity : MonoBehaviour
{
    private bool init;

    private void OnSliderChange()
    {
        if (!init)
        {
            init = true;
            if (PlayerPrefs.HasKey("MouseSensitivity"))
            {
                gameObject.GetComponent<UISlider>().sliderValue = PlayerPrefs.GetFloat("MouseSensitivity");
            }
            else
            {
                PlayerPrefs.SetFloat("MouseSensitivity", gameObject.GetComponent<UISlider>().sliderValue);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("MouseSensitivity", gameObject.GetComponent<UISlider>().sliderValue);
        }
        IN_GAME_MAIN_CAMERA.sensitivityMulti = PlayerPrefs.GetFloat("MouseSensitivity");
    }
}

