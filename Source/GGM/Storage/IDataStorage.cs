namespace GGM.Storage
{
    public interface IDataStorage
    {
        //Methods for loading
        //T GetCustom<T>(string key) where T : ISerializable; 
        bool GetBool(string key, bool defVal = false);
        float GetFloat(string key, float defVal = 0f);
        int GetInt(string key, int defVal = 0);
        string GetString(string key, string defVal = "");

        //Methods for saving
        // void SetCustom<T>(T saveObj, string key) where T : ISerializable;
        void SetBool(string key, bool val);
        void SetFloat(string key, float val);
        void SetInt(string key, int val);
        void SetString(string key, string val);
    }
}