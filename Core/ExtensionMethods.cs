using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public static class ExtensionMethods
    {
        #region Methods

        public static TValue Execute<TKey, TValue>(this IDictionary<TKey, Func<TValue>> funcMap, TKey key)
        {
            return key != null && funcMap.ContainsKey(key)
                ? funcMap[key]()
                : default(TValue);
        }

        public static TValue Execute<TKey, TParameter, TValue>(this IDictionary<TKey, Func<TParameter, TValue>> funcMap, TKey key, TParameter parameter)
        {
            return key != null && funcMap.ContainsKey(key)
                ? funcMap[key](parameter)
                : default(TValue);
        }

        public static void Execute<TKey>(this IDictionary<TKey, Action> funcMap, TKey key)
        {
            if (key != null && funcMap.ContainsKey(key))
                funcMap[key]();
        }

        public static void Execute<TKey, TParameter>(this IDictionary<TKey, Action<TParameter>> funcMap, TKey key, TParameter parameter)
        {
            if (key != null && funcMap.ContainsKey(key))
                funcMap[key](parameter);
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
                action(item);
        }

        public static TKey GetKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TValue value)
        {
            var item = dictionary
                .FirstOrDefault(x => x.Value.Equals(value));

            return !item.Equals(null)
                ?  item.Key
                :  default(TKey);
        }

        #endregion
    }
}