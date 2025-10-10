using System;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace MbsCore.Extensions.Editor
{
    [CustomPropertyDrawer(typeof(ScriptableList<>))]
    public partial class ScriptableListDrawer : PropertyDrawer
    {
        private const float CreateFieldRightOffset = 100f;
        private const string ErrorMessage = "ScriptableList can only be used within the inheritors of ScriptableObject";
        private const float ErrorMessageHeight = 25f;
        
        private static readonly Type s_scriptableListTypeDefinition = typeof(ScriptableList<>);
        
        private ReorderableList _reorderable;
        private DrawerMediator _drawerMediator;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Init(property);
            if (_reorderable == null)
            {
                EditorGUI.HelpBox(position, ErrorMessage, MessageType.Error);
                return;
            }

            _reorderable.DoList(position);
            position.y += _reorderable.GetHeight() - EditorGUIUtility.singleLineHeight;
            position.height = EditorGUIUtility.singleLineHeight;
            position.width -= CreateFieldRightOffset;
            _drawerMediator.DrawCreateField(position);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Init(property);
            if (_reorderable != null)
            {
                return _reorderable.GetHeight() + EditorGUIUtility.standardVerticalSpacing;
            }

            return ErrorMessageHeight;
        }

        private void Init(SerializedProperty property)
        {
            if (_reorderable != null ||
                !(property.serializedObject.targetObject is ScriptableObject targetObject))
            {
                return;
            }

            ValidateGenericType(fieldInfo.FieldType, out Type listArgumentType);
            IList list = fieldInfo.GetValue(property.serializedObject.targetObject) as IList;
            _reorderable = new ReorderableList(list, listArgumentType, true, true, true, true);
            _drawerMediator = CreateMediator(property.displayName, listArgumentType, _reorderable, targetObject);
        }
    }
}