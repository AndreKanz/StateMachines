namespace Contracts.Serialization
{
    public interface ISerializer
    {
        #region Methods

        void Serialize<T>(T obj, string path);
        T Deserialize<T>(string path);

        #endregion
    }
}