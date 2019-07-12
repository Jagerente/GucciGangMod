using System;
using GGM.Config;
using UnityEngine;

public class ChangeQuality : MonoBehaviour
{
    private bool init;
    public static bool isTiltShiftOn;

    private void OnSliderChange()
    {
        if (!init)
        {
            init = true;
            if (PlayerPrefs.HasKey("GameQuality"))
            {
                gameObject.GetComponent<UISlider>().sliderValue = PlayerPrefs.GetFloat("GameQuality");
            }
            else
            {
                PlayerPrefs.SetFloat("GameQuality", gameObject.GetComponent<UISlider>().sliderValue);
            }
        }
        else
        {
            PlayerPrefs.SetFloat("GameQuality", gameObject.GetComponent<UISlider>().sliderValue);
        }
    }

    public static void setCurrentQuality()
    {
    }
}

