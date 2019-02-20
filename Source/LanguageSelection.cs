//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[RequireComponent(typeof(UIPopupList)), AddComponentMenu("NGUI/Interaction/Language Selection")]
public class LanguageSelection : MonoBehaviour
{
    private UIPopupList mList;

    private void OnLanguageSelection(string language)
    {
        if (Localization.instance != null)
        {
            Localization.instance.currentLanguage = language;
        }
    }

    private void Start()
    {
        mList = GetComponent<UIPopupList>();
        UpdateList();
        mList.eventReceiver = gameObject;
        mList.functionName = "OnLanguageSelection";
    }

    private void UpdateList()
    {
        if (Localization.instance != null && Localization.instance.languages != null)
        {
            mList.items.Clear();
            var index = 0;
            var length = Localization.instance.languages.Length;
            while (index < length)
            {
                var asset = Localization.instance.languages[index];
                if (asset != null)
                {
                    mList.items.Add(asset.name);
                }
                index++;
            }
            mList.selection = Localization.instance.currentLanguage;
        }
    }
}

