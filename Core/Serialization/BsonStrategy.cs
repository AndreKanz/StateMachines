using Contracts.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.IO;

namespace Core.Serialization
{
    internal class BsonStrategy : IBinaryStrategy
    {
        #region Methods

        public byte[] Serialize<T>(T item)
        {
            using (var stream = new MemoryStream())
            using (var writer = new BsonWriter(stream))
            {
                JsonSerializer
                    .Create(new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
                    .Serialize(writer, item);

                return stream.ToArray();
            }
        }

        public T Deserialize<T>(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            using (var reader = new BsonReader(stream))
                return new JsonSerializer().Deserialize<T>(reader);
        }

        #endregion
    }
}