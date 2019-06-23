using UnityEngine;

namespace GGM.Storage
{
    public class PrefStorage : IDataStorage
    {
        public bool GetBool(string key, bool defVal = false)
        {
            return PlayerPrefs.GetInt(key, defVal ? 1 : 0) == 1;
        }

        public float GetFloat(string key, float defVal = 0f)
        {
            return PlayerPrefs.GetFloat(key, defVal);
        }

        public int GetInt(string key, int defVal = 0)
        {
            return PlayerPrefs.GetInt(key, defVal);
        }

        public string GetString(string key, string defVal = "")
        {
            return PlayerPrefs.GetString(key, defVal);
        }

        public void SetBool(string key, bool val)
        {
            PlayerPrefs.SetInt(key, val ? 1 : 0);
        }

        public void SetFloat(string key, float val)
        {
            PlayerPrefs.SetFloat(key, val);
        }

        public void SetInt(string key, int val)
        {
            PlayerPrefs.SetInt(key, val);
        }

        public void SetString(string key, string val)
        {
            PlayerPrefs.SetString(key, val);
        }
    }
}