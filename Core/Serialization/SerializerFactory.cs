using Contracts.Serialization;
using Core.Patterns;
using System;
using System.Collections.Generic;

namespace Core.Serialization
{
    public class SerializerFactory : MonoStateFactoryBase<string, ISerializer>
    {
        #region Fields

        private static readonly IDictionary<string, Func<ISerializer>> SerializerMap =
            new Dictionary<string, Func<ISerializer>>
            {
                { ".json",  () => new JSerializer()                            },
                { ".bson",  () => new BinarySerializer(new BsonStrategy())     }
            };

        #endregion

        #region Properties

        protected override IDictionary<string, Func<ISerializer>> ConstructorMap => SerializerMap;

        #endregion
    }
}