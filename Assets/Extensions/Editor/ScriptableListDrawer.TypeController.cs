using System;
using MbsCore.Extensions.Runtime;
using UnityEngine;

namespace MbsCore.Extensions.Editor
{
    public partial class ScriptableListDrawer
    {
        private static class TypeController<T> where T : ScriptableObject
        {
            private static readonly Type s_processingType = typeof(T);

            private static string[] s_names;
            private static Type[] s_types;

            public static string[] Names
            {
                get
                {
                    if (s_names == null)
                    {
                        int count = Types.Length;
                        s_names = new string[count];
                        for (var i = 0; i < count; i++)
                        {
                            s_names[i] = Types[i].Name;
                        }
                    }

                    return s_names;
                }
            }

            private static Type[] Types
            {
                get
                {
                    if (s_types == null)
                    {
                        s_types = s_processingType.GetAssignableTypes();
                    }

                    return s_types;
                }
            }

            public static T CreateInstance(string typeName)
            {
                foreach (Type type in Types)
                {
                    if (type.Name == typeName)
                    {
                        ScriptableObject asset = ScriptableObject.CreateInstance(type);
                        return asset as T;
                    }
                }

                return null;
            }

            private static bool Validate(Type type)
            {
                return !type.IsAbstract && s_processingType.IsAssignableFrom(type);
            }
        }
    }
}