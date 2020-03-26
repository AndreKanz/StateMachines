using System.IO;
using Newtonsoft.Json;
using System;
using Contracts.Serialization;

namespace Core.Serialization
{
    internal class JSerializer : ISerializer
    {
        #region Methods

        public void Serialize<T>(T obj, string path) 
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            using (var file = File.CreateText(path))
                JsonSerializer.Create(
                    new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented
                    })
                    .Serialize(file, obj);
        }

        public T Deserialize<T>(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            using (var file = File.OpenText(path))
                return (T)new JsonSerializer()
                    .Deserialize(file, typeof(T));
        }

        #endregion
    }
}