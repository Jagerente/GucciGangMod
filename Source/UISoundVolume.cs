//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Sound Volume"), RequireComponent(typeof(UISlider))]
public class UISoundVolume : MonoBehaviour
{
    private UISlider mSlider;

    private void Awake()
    {
        mSlider = GetComponent<UISlider>();
        mSlider.sliderValue = NGUITools.soundVolume;
        mSlider.eventReceiver = gameObject;
    }

    private void OnSliderChange(float val)
    {
        NGUITools.soundVolume = val;
    }
}

