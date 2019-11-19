using System;
using System.Collections.Generic;
using System.Text;

namespace NWrath.Synergy.Common.Extensions
{
    public static class CommonExtensions
    {
        #region Strings

        public static string ToFormat(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static bool IsEmpty(this string obj)
        {
            return string.IsNullOrEmpty(obj);
        }

        public static bool NotEmpty(this string obj)
        {
            return !string.IsNullOrEmpty(obj);
        }

        public static StringBuilder ToStringBuilder(this string obj)
        {
            return new StringBuilder(obj);
        }

        public static string StringJoin(this string[] collection, string separator = ",")
        {
            return string.Join(separator, collection);
        }

        public static string StringJoin(this IEnumerable<string> collection, string separator = ",")
        {
            return string.Join(separator, collection);
        }

        #endregion Strings

        #region Transform

        public static TResult Extract<TSource, TResult>(this TSource obj, Func<TSource, TResult> extract)
        {
            var result = extract(obj);

            return result;
        }

        public static TSource Apply<TSource>(this TSource obj, Action<TSource> apply)
        {
            apply(obj);

            return obj;
        }

        #endregion Transform

        #region Cast

        public static TResult CastTo<TResult>(this object obj)
        {
            return (TResult)obj;
        }

        public static TResult CastAs<TResult>(this object obj)
            where TResult : class
        {
            return obj as TResult;
        }

        public static TResult CastAs<TResult>(this object obj, TResult defaultValue)
            where TResult : class
        {
            return obj as TResult ?? defaultValue;
        }

        #endregion Cast

        #region If

        public static TSource If<TSource>(
            this TSource obj,
            TSource expected,
            Action<TSource> then,
            Action<TSource> otherwise = null
            )
        {
            if (EqualityComparer<TSource>.Default.Equals(obj, expected))
            {
                then(obj);
            }
            else
            {
                otherwise?.Invoke(obj);
            }

            return obj;
        }

        public static TResult If<TSource, TResult>(
            this TSource obj,
            TSource expected,
            Func<TSource, TResult> then,
            Func<TSource, TResult> otherwise = null,
            TResult defaultValue = default(TResult)
            )
        {
            if (EqualityComparer<TSource>.Default.Equals(obj, expected))
            {
                return then(obj);
            }

            var result = otherwise == null
                ? defaultValue
                : otherwise(obj);

            return result;
        }

        public static TSource If<TSource>(
            this TSource obj,
            Func<TSource, bool> condition,
            Action<TSource> then,
            Action<TSource> otherwise = null
            )
        {
            if (condition(obj))
            {
                then(obj);
            }
            else
            {
                otherwise?.Invoke(obj);
            }

            return obj;
        }

        public static TResult If<TSource, TResult>(
            this TSource obj,
            Func<TSource, bool> condition,
            Func<TSource, TResult> then,
            Func<TSource, TResult> otherwise = null,
            TResult defaultValue = default(TResult)
            )
        {
            if (condition(obj))
            {
                return then(obj);
            }

            var result = otherwise == null
                ? defaultValue
                : otherwise(obj);

            return result;
        }

        #endregion If
    }
}