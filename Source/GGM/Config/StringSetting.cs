namespace GGM.Config
{
    public class StringSetting : Setting<string>
    {
        public StringSetting(string key, string defVal = "") : base(key, defVal)
        {
        }

        public override void Load()
        {
            Value = Settings.Storage.GetString(Key, Default);
        }

        public override void Save()
        {
            Settings.Storage.SetString(Key, Value);
        }
    }
}