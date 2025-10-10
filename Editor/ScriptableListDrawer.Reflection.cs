using System;
using System.Reflection;
using UnityEditorInternal;
using UnityEngine;

namespace MbsCore.Extensions.Editor
{
	public partial class ScriptableListDrawer
	{
		private static readonly MethodInfo s_createMediatorGenericMethod =
				typeof(ScriptableListDrawer)
						.GetMethod(
								nameof(CreateMediatorGeneric),
								BindingFlags.Static | BindingFlags.NonPublic
						);

		private static readonly MethodInfo s_getOptimizedGUIBlockMethod =
				typeof(UnityEditor.Editor)
						.GetMethod(
								"GetOptimizedGUIBlock",
								BindingFlags.Instance | BindingFlags.NonPublic
						);

		private static readonly MethodInfo s_onOptimizedInspectorGUIMethod =
				typeof(UnityEditor.Editor)
						.GetMethod(
								"OnOptimizedInspectorGUI",
								BindingFlags.Instance | BindingFlags.NonPublic
						);

		private static readonly object[] s_createMethodArguments = new object[3];
		private static readonly object[] s_onOptimizedInspectorGUIMethodArguments = new object[1];
		private static readonly object[] s_optimizedGUIBlockMethodArguments = new object[3];


		private static DrawerMediator CreateMediator(string label,
		                                             Type type,
		                                             ReorderableList reorderableList,
		                                             ScriptableObject scriptableObject)
		{
			s_createMethodArguments[0] = label;
			s_createMethodArguments[1] = reorderableList;
			s_createMethodArguments[2] = scriptableObject;
			object createdMediator = s_createMediatorGenericMethod
			                         .MakeGenericMethod(type)
			                         .Invoke(null, s_createMethodArguments);

			return createdMediator as DrawerMediator;
		}

		private static DrawerMediator CreateMediatorGeneric<T>(string label,
		                                                       ReorderableList reorderableList,
		                                                       ScriptableObject scriptableObject)
				where T : ScriptableObject
		{
			return new DrawerMediator<T>(label, reorderableList, scriptableObject);
		}

		private static void ValidateGenericType(Type checkedType, out Type genericTypes)
		{
			while (checkedType != null)
			{
				if (checkedType.IsGenericType && checkedType.GetGenericTypeDefinition() == s_scriptableListTypeDefinition)
				{
					genericTypes = checkedType.GetGenericArguments()[0];

					return;
				}

				checkedType = checkedType.BaseType;
			}

			genericTypes = null;
		}

		private static bool GetOptimizedGUIBlock(UnityEditor.Editor editor,
		                                         bool isDirty,
		                                         bool isVisible,
		                                         out float height)
		{
			s_optimizedGUIBlockMethodArguments[0] = isDirty;
			s_optimizedGUIBlockMethodArguments[1] = isVisible;
			bool result = (bool) s_getOptimizedGUIBlockMethod.Invoke(editor, s_optimizedGUIBlockMethodArguments);
			height = (float) s_optimizedGUIBlockMethodArguments[2];

			return result;
		}

		private static void OnOptimizedInspectorGUI(UnityEditor.Editor editor, Rect position)
		{
			s_onOptimizedInspectorGUIMethodArguments[0] = position;
			s_onOptimizedInspectorGUIMethod.Invoke(editor, s_onOptimizedInspectorGUIMethodArguments);
		}
	}
}