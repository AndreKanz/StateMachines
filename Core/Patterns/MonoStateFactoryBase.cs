using Contracts.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Patterns
{
    public abstract class MonoStateFactoryBase<TKey, TValue> : IAbstractFactory<TKey, TValue>
    {
        #region Properties

        public TKey[] Keys => ConstructorMap.Keys.ToArray();

        protected abstract IDictionary<TKey, Func<TValue>> ConstructorMap { get; }

        #endregion

        #region Methods

        public TValue CreateInstance(TKey key)
        {
            return key != null && ConstructorMap.ContainsKey(key)
                ?  ConstructorMap[key]()
                :  default(TValue);
        }

        #endregion
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class MonoStateFactoryBase<TKey, TParameter, TValue> : IAbstractFactory<TKey, TParameter, TValue>
    {
        #region Properties

        public TKey[] Keys => ConstructorMap.Keys.ToArray();

        protected abstract IDictionary<TKey, Func<TParameter, TValue>> ConstructorMap { get; }

        #endregion

        #region Methods

        public TValue CreateInstance(TKey key, TParameter parameter)
        {
            return key != null && parameter != null && ConstructorMap.ContainsKey(key)
                ?  ConstructorMap[key](parameter)
                :  default(TValue);
        }

        #endregion
    }
}