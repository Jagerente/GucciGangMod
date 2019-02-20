//Fixed With [DOGE]DEN aottg Sources fixer
//Doge Guardians FTW
//DEN is OP as fuck.
//Farewell Cowboy

using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Internal/Localization")]
public class Localization : MonoBehaviour
{
    public TextAsset[] languages;
    private Dictionary<string, string> mDictionary = new Dictionary<string, string>();
    private static Localization mInstance;
    private string mLanguage;
    public string startingLanguage = "English";

    private void Awake()
    {
        if (mInstance == null)
        {
            mInstance = this;
            DontDestroyOnLoad(gameObject);
            currentLanguage = PlayerPrefs.GetString("Language", startingLanguage);
            if ((string.IsNullOrEmpty(mLanguage) && (languages != null)) && (languages.Length > 0))
            {
                currentLanguage = languages[0].name;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public string Get(string key)
    {
        string str;
        return (!mDictionary.TryGetValue(key, out str) ? key : str);
    }

    private void Load(TextAsset asset)
    {
        mLanguage = asset.name;
        PlayerPrefs.SetString("Language", mLanguage);
        mDictionary = new ByteReader(asset).ReadDictionary();
        UIRoot.Broadcast("OnLocalize", this);
    }

    public static string Localize(string key)
    {
        return ((instance == null) ? key : instance.Get(key));
    }

    private void OnDestroy()
    {
        if (mInstance == this)
        {
            mInstance = null;
        }
    }

    private void OnEnable()
    {
        if (mInstance == null)
        {
            mInstance = this;
        }
    }

    public string currentLanguage
    {
        get
        {
            return mLanguage;
        }
        set
        {
            if (mLanguage != value)
            {
                startingLanguage = value;
                if (!string.IsNullOrEmpty(value))
                {
                    if (languages != null)
                    {
                        var index = 0;
                        var length = languages.Length;
                        while (index < length)
                        {
                            var asset = languages[index];
                            if ((asset != null) && (asset.name == value))
                            {
                                Load(asset);
                                return;
                            }
                            index++;
                        }
                    }
                    var asset2 = Resources.Load(value, typeof(TextAsset)) as TextAsset;
                    if (asset2 != null)
                    {
                        Load(asset2);
                        return;
                    }
                }
                mDictionary.Clear();
                PlayerPrefs.DeleteKey("Language");
            }
        }
    }

    public static Localization instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType(typeof(Localization)) as Localization;
                if (mInstance == null)
                {
                    var target = new GameObject("_Localization");
                    DontDestroyOnLoad(target);
                    mInstance = target.AddComponent<Localization>();
                }
            }
            return mInstance;
        }
    }

    public static bool isActive
    {
        get
        {
            return (mInstance != null);
        }
    }
}

