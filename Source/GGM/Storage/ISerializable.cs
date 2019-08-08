namespace GGM.Storage
{
    public interface ISerializable
    {
        void Deserialize(string key, IDataStorage storage);

        void Serialize(string key, IDataStorage storage);
    }
}