//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using UnityEngine;

[AddComponentMenu("NGUI/UI/Input (Saved)")]
public class UIInputSaved : UIInput
{
    public string playerPrefsField;

    private void Awake()
    {
        onSubmit = new OnSubmit(SaveToPlayerPrefs);
        if (!string.IsNullOrEmpty(playerPrefsField) && PlayerPrefs.HasKey(playerPrefsField))
        {
            text = PlayerPrefs.GetString(playerPrefsField);
        }
    }

    private void OnApplicationQuit()
    {
        SaveToPlayerPrefs(text);
    }

    private void SaveToPlayerPrefs(string val)
    {
        if (!string.IsNullOrEmpty(playerPrefsField))
        {
            PlayerPrefs.SetString(playerPrefsField, val);
        }
    }

    public override string text
    {
        get
        {
            return base.text;
        }
        set
        {
            base.text = value;
            SaveToPlayerPrefs(value);
        }
    }
}

