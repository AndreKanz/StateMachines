namespace Contracts.Serialization
{
    public interface IBinaryStrategy
    {
        #region Methods

        byte[] Serialize<T>(T item);
        T Deserialize<T>(byte[] data);

        #endregion
    }
}