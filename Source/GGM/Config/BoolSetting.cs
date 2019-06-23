namespace GGM.Config
{
    public class BoolSetting : Setting<bool> 
    {
        public BoolSetting(string key, bool def = false) : base(key, def) { }

        public override void Load()
        {
            Value = Settings.Storage.GetBool(Key, Default);
        }

        public override void Save()
        {
            Settings.Storage.SetBool(Key, Value);
        }
    }
}