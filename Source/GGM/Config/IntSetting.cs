namespace GGM.Config
{
    public class IntSetting : Setting<int>
    {
        public IntSetting(string key, int defVal = 0) : base(key, defVal)
        {
        }

        public override void Load()
        {
            Value = Settings.Storage.GetInt(Key, Default);
        }

        public override void Save()
        {
            Settings.Storage.SetInt(Key, Value);
        }
    }
}