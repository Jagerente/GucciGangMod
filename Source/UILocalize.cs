using UnityEngine;

[AddComponentMenu("NGUI/UI/Localize"), RequireComponent(typeof(UIWidget))]
public class UILocalize : MonoBehaviour
{
    public string key;
    private string mLanguage;
    private bool mStarted;

    public void Localize()
    {
        Localization instance = Localization.instance;
        var component = GetComponent<UIWidget>();
        var label = component as UILabel;
        var sprite = component as UISprite;
        if (string.IsNullOrEmpty(mLanguage) && string.IsNullOrEmpty(key) && label != null)
        {
            key = label.text;
        }

        var str = !string.IsNullOrEmpty(key) ? instance.Get(key) : string.Empty;
        if (label != null)
        {
            UIInput input = NGUITools.FindInParents<UIInput>(label.gameObject);
            if (input != null && input.label == label)
            {
                input.defaultText = str;
            }
            else
            {
                label.text = str;
            }
        }
        else if (sprite != null)
        {
            sprite.spriteName = str;
            sprite.MakePixelPerfect();
        }

        mLanguage = instance.currentLanguage;
    }

    private void OnEnable()
    {
        if (mStarted && Localization.instance != null)
        {
            Localize();
        }
    }

    private void OnLocalize(Localization loc)
    {
        if (mLanguage != loc.currentLanguage)
        {
            Localize();
        }
    }

    private void Start()
    {
        mStarted = true;
        if (Localization.instance != null)
        {
            Localize();
        }
    }
}