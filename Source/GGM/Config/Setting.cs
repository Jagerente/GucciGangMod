namespace GGM.Config
{
    public abstract class Setting<T> : ISetting
    {
        public T Value;

        public T Default { get; }
        public string Key { get; protected set; }

        public Setting(string key, T defVAl)
        {
            Key = key;
            Default = defVAl;
            Settings.AddSetting(this);
        }

        public abstract void Load();

        public abstract void Save();

        public void SetValue(T val)
        {
            Value = val;
            Save();
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator T(Setting<T> set)
        {
            return set.Value;
        }
    }
}