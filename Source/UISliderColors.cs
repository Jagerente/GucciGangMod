using UnityEngine;

[AddComponentMenu("NGUI/Examples/Slider Colors"), RequireComponent(typeof(UISlider)), ExecuteInEditMode]
public class UISliderColors : MonoBehaviour
{
    public Color[] colors = {Color.red, Color.yellow, Color.green};
    private UISlider mSlider;
    public UISprite sprite;

    private void Start()
    {
        mSlider = GetComponent<UISlider>();
        Update();
    }

    private void Update()
    {
        if (sprite != null && colors.Length != 0)
        {
            var f = mSlider.sliderValue * (colors.Length - 1);
            var index = Mathf.FloorToInt(f);
            var color = colors[0];
            if (index >= 0)
            {
                if (index + 1 < colors.Length)
                {
                    var t = f - index;
                    color = Color.Lerp(colors[index], colors[index + 1], t);
                }
                else if (index < colors.Length)
                {
                    color = colors[index];
                }
                else
                {
                    color = colors[colors.Length - 1];
                }
            }

            color.a = sprite.color.a;
            sprite.color = color;
        }
    }
}