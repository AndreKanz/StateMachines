using Contracts.Serialization;
using System;
using System.IO;

namespace Core.Serialization
{
    internal class BinarySerializer : ISerializer
    {
        #region Properties

        private IBinaryStrategy Strategy { get; }

        #endregion

        #region Constructors

        internal BinarySerializer(IBinaryStrategy strategy)
        {
            Strategy = strategy;
        }

        #endregion

        #region Methods

        public void Serialize<T>(T obj, string path)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            File.WriteAllBytes(path, Strategy.Serialize(obj));
        }

        public T Deserialize<T>(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));

            return Strategy.Deserialize<T>(File.ReadAllBytes(path));
        }

        #endregion
    }
}