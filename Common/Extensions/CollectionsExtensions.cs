using System;
using System.Collections.Generic;
using System.Linq;

namespace NWrath.Synergy.Common.Extensions.Collections
{
    public static class CollectionsExtensions
    {
        #region Dictionary

        public static TValue TryGet<TKey, TValue>(this Dictionary<TKey, TValue> store, TKey key)
        {
            return store.ContainsKey(key)
                ? store[key]
                : default(TValue);
        }

        public static TValue TryGet<TKey, TValue>(this Dictionary<TKey, TValue> store, TKey key, TValue defaultValue)
        {
            return store.ContainsKey(key)
                ? store[key]
                : defaultValue;
        }

        public static TValue AddOrUpdate<TKey, TValue>(
            this Dictionary<TKey, TValue> store,
            TKey key,
            TValue value,
            Func<TKey, TValue, TValue> updateValueFactory
            )
        {
            if (!store.ContainsKey(key))
            {
                store.Add(key, value);

                return value;
            }

            var updated = updateValueFactory(key, store[key]);

            store[key] = updated;

            return updated;
        }

        public static TValue GetOrAdd<TKey, TValue>(
            this Dictionary<TKey, TValue> store,
            TKey key,
            Func<TKey, TValue> valueFactory
            )
        {
            if (!store.ContainsKey(key))
            {
                var value = valueFactory(key);

                store.Add(key, value);

                return value;
            }

            return store[key];
        }

        public static T GetOrAdd<T>(this Dictionary<string, object> store, string key, Func<string, T> valueFactory)
        {
            if (!store.ContainsKey(key))
            {
                var value = valueFactory(key);

                store.Add(key, value);

                return value;
            }

            return (T)store[key];
        }

        public static T TryGet<T>(this Dictionary<string, object> store, string key)
        {
            return (T)store.TryGet(key);
        }

        public static T TryGet<T>(this Dictionary<string, object> store)
        {
            return (T)store.TryGet(typeof(T).Name);
        }

        public static T TryGet<T>(this Dictionary<string, object> store, string key, T defaultValue)
        {
            return store.ContainsKey(key)
                ? (T)store[key]
                : defaultValue;
        }

        public static T TryGet<T>(this Dictionary<string, object> store, string key, Func<string, T> defaultValueFactory)
        {
            return store.ContainsKey(key)
                ? (T)store[key]
                : defaultValueFactory(key);
        }

        public static void Add<T>(this Dictionary<string, object> store, string key, T val)
        {
            store[key] = val;
        }

        public static void Add<T>(this Dictionary<string, object> store, T val)
        {
            store[typeof(T).Name] = val;
        }

        #endregion Dictionary

        #region Empty

        public static bool IsEmpty<TSource>(this TSource[] collection)
        {
            return collection == null || collection.Length == 0;
        }

        public static bool IsEmpty<TSource>(this List<TSource> collection)
        {
            return collection == null || collection.Count == 0;
        }

        public static bool NotEmpty<TSource>(this TSource[] collection)
        {
            return !IsEmpty(collection);
        }

        public static bool NotEmpty<TSource>(this List<TSource> collection)
        {
            return !IsEmpty(collection);
        }

        #endregion Empty

        #region Each

        public static IEnumerable<TSource> Each<TSource>(this IEnumerable<TSource> collection, Action<TSource, int> action)
        {
            return Each<IEnumerable<TSource>, TSource>(collection, action);
        }

        public static IEnumerable<TSource> Each<TSource>(this IEnumerable<TSource> collection, Action<TSource> action)
        {
            return Each<IEnumerable<TSource>, TSource>(collection, action);
        }

        public static TSource[] Each<TSource>(this TSource[] collection, Action<TSource, int> action)
        {
            return Each<TSource[], TSource>(collection, action);
        }

        public static TSource[] Each<TSource>(this TSource[] collection, Action<TSource> action)
        {
            return Each<TSource[], TSource>(collection, action);
        }

        public static List<TSource> Each<TSource>(this List<TSource> collection, Action<TSource, int> action)
        {
            return Each<List<TSource>, TSource>(collection.ToList(), action);
        }

        public static List<TSource> Each<TSource>(this List<TSource> collection, Action<TSource> action)
        {
            return Each<List<TSource>, TSource>(collection.ToList(), action);
        }

        private static TCollection Each<TCollection, TItem>(this TCollection collection, Action<TItem, int> action)
            where TCollection : IEnumerable<TItem>
        {
            var i = 0;

            foreach (var item in collection)
            {
                action(item, i++);
            }

            return collection;
        }

        private static TCollection Each<TCollection, TItem>(TCollection collection, Action<TItem> action)
            where TCollection : IEnumerable<TItem>
        {
            foreach (var item in collection)
            {
                action(item);
            }

            return collection;
        }

        #endregion Each
    }
}