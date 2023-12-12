using System;
using System.Collections.Generic;
using System.Reflection;

namespace MbsCore.Extensions.Runtime
{
    public static class TypeExtensions
    {
        private const int DefaultComparisonValue = 100;

        /// <summary>Returns non-abstract and non-generic implementations of type.</summary>
        public static Type[] GetImplementations(this Type baseType) =>
                baseType.GetAllMatchingTypes(ImplementationCondition);

        /// <summary>Returns implementations of type including non-abstract and non-generic.</summary>
        public static Type[] GetAllImplementations(this Type baseType) =>
                baseType.GetAllMatchingTypes(AllImplementationCondition);

        /// <summary>Returns types that belong to the base type.</summary>
        public static Type[] GetAssignableTypes(this Type baseType) =>
                baseType.GetAllMatchingTypes(AssignableCondition);

        /// <summary>Returns the type by its name.</summary>
        public static Type CustomGetType(this string typeName)
        {
            int splitIndex = typeName.IndexOf(' ');
            var assembly = Assembly.Load(typeName.Substring(0, splitIndex));

            return assembly.GetType(typeName.Substring(splitIndex + 1));
        }

        /// <summary>Returns the weight between the original type and its children type.</summary>
        public static int Comparison(this Type type, Type childrenType)
        {
            if (type.IsInterface)
            {
                return type.ComparisonInterface(childrenType);
            }

            return type.ComparisonClass(childrenType);
        }
        
        private static bool ImplementationCondition(Type type, Type baseType)
        {
            return baseType.IsAssignableFrom(type) && type != baseType
                   && !type.IsAbstract && !type.IsGenericType;
        }

        private static bool AllImplementationCondition(Type type, Type baseType)
        {
            return baseType.IsAssignableFrom(type) && type != baseType;
        }

        private static bool AssignableCondition(Type type, Type baseType)
        {
            return baseType.IsAssignableFrom(type) && !type.IsAbstract;
        }

        private static int ComparisonInterface(this Type type, Type childrenType)
        {
            if (childrenType == type)
            {
                return 0;
            }

            int weight = DefaultComparisonValue;
            Type[] interfaces = childrenType.GetInterfaces();
            foreach (Type interfaceType in interfaces)
            {
                if (!type.IsAssignableFrom(interfaceType))
                {
                    weight--;
                }
            }

            return weight;
        }

        private static int ComparisonClass(this Type type, Type childrenType)
        {
            if (childrenType == type)
            {
                return DefaultComparisonValue;
            }

            return ComparisonClass(type, childrenType.BaseType) + 1;
        }

        private static Type[] GetAllMatchingTypes(this Type baseType, Func<Type, Type, bool> predicate)
        {
            List<Type> matchingTypes = new List<Type>();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (predicate(type, baseType))
                    {
                        matchingTypes.Add(type);
                    }
                }
            }

            return matchingTypes.ToArray();
        }
    }
}