using GGM.Storage;
using System.Collections.Generic;

namespace GGM.Config
{
    public static class Settings
    {
        public static IDataStorage Storage = null;
        private static Queue<ISetting> _allSettings = null;

        #region Booleans
        public static BoolSetting Chat = new BoolSetting("Chat", true);
        public static BoolSetting ChatMajorBold = new BoolSetting("ChatMajorBold", false);
        public static BoolSetting ChatMajorItalic = new BoolSetting("ChatMajorItalic", false);
        public static BoolSetting ChatMinorBold = new BoolSetting("ChatMinorBold", false);
        public static BoolSetting ChatMinorItalic = new BoolSetting("ChatMinorItalic", false);

        #endregion

        #region Floats
        #endregion

        #region Integers
        #endregion

        #region Strings
        public static StringSetting ChatMajorColor = new StringSetting("ChatMajorColor", string.Empty);
        public static StringSetting ChatMinorColor = new StringSetting("ChatMinorColor", string.Empty);
        public static StringSetting ChatSize = new StringSetting("ChatSize", string.Empty);
        #endregion

        static Settings()
        {
            Load();
        }

        public static void AddSetting(ISetting set)
        {
            if (_allSettings == null)
                _allSettings = new Queue<ISetting>();
            _allSettings.Enqueue(set);
        }

        private static void CreateStorage()
        {
            int choice = UnityEngine.PlayerPrefs.GetInt("StorageType", 0);
            switch (choice)
            {
                case 0:
                    Storage = new PrefStorage();
                    break;

                default:
                    break;
            }
        }

        public static void Load()
        {
            if (Storage == null)
                CreateStorage();
            foreach(ISetting set in _allSettings)
            {
                set.Load();
            }
        }

        public static void Save()
        {
            foreach(ISetting set in _allSettings)
            {
                set.Save();
            }
        }
    }
}