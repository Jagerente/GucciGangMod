namespace GGM.Config
{
    public class FloatSetting : Setting<float>
    {
        public FloatSetting(string key, float def = 0f) : base(key, def) { }

        public override void Load()
        {
            Value = Settings.Storage.GetFloat(Key, Default);
        }

        public override void Save()
        {
            Settings.Storage.SetFloat(Key, Value);
        }
    }
}