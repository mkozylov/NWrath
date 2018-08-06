using NWrath.Synergy.Common.Extensions;
using System;
using System.Linq;
using System.Reflection;

namespace NWrath.Synergy.Reflection.Extensions
{
    public static class CommonReflectionExtensions
    {
        public static MemberInfo[] GetPublicMembers(this Type type)
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.Instance;

            return type.GetFields(bindingFlags).Cast<MemberInfo>()
                       .Concat(type.GetProperties(bindingFlags))
                       .ToArray();
        }

        public static Type GetMemberType(this MemberInfo member)
        {
            return member.MemberType == MemberTypes.Property
                    ? member.CastTo<PropertyInfo>().PropertyType
                    : member.CastTo<FieldInfo>().FieldType;
        }

        public static object GetMemberValue(this MemberInfo member, object target)
        {
            return member.MemberType == MemberTypes.Property
                    ? member.CastTo<PropertyInfo>().GetValue(target)
                    : member.CastTo<FieldInfo>().GetValue(target);
        }

        public static void SetMemberValue(this MemberInfo member, object target, object value)
        {
            if (member.MemberType == MemberTypes.Property)
            {
                member.CastTo<PropertyInfo>().SetValue(target, value);
            }
            else
            {
                member.CastTo<FieldInfo>().SetValue(target, value);
            }
        }

        public static object CallGenericMethod<TObj>(this TObj instance, string methodName, Type[] genericParamTypes, params object[] args)
            where TObj : class
        {
            return instance.GetType()
                           .GetTypeInfo()
                           .GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                           .First(m => m.Name == methodName && m.IsGenericMethod)
                           .MakeGenericMethod(genericParamTypes)
                           .Invoke(instance, args);
        }

        public static object CallStaticGenericMethod<TObj>(this TObj instance, string methodName, Type[] genericParamTypes, params object[] args)
        {
            return instance.GetType()
                           .CallStaticGenericMethod(methodName, genericParamTypes, args);
        }

        public static object CallStaticGenericMethod(this Type type, string methodName, Type[] genericParamTypes, params object[] args)
        {
            return type.GetTypeInfo()
                       .GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                       .First(x => x.Name == methodName
                                && x.IsGenericMethod
                                && x.GetGenericArguments().Length == genericParamTypes.Length)
                       .MakeGenericMethod(genericParamTypes)
                       .Invoke(null, args);
        }

        public static MethodInfo GetStaticGenericMethod(
            this Type type,
            string methodName,
            int genericParamCount,
            int argsCount
            )
        {
            return type.GetTypeInfo()
                       .GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static)
                       .First(x => x.Name == methodName
                                && x.IsGenericMethod
                                && x.GetGenericArguments().Length == genericParamCount
                                && x.GetParameters().Length == argsCount);
        }
    }
}