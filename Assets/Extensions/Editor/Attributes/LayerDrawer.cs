using System.Collections.Generic;
using MbsCore.Extensions.Runtime.Attributes;
using UnityEditor;
using UnityEngine;

namespace MbsCore.Extensions.Editor.Attributes
{
    [CustomPropertyDrawer(typeof(LayerAttribute))]
    internal sealed class LayerDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Integer)
            {
                EditorGUI.HelpBox(position, "Layer can be only integer type!", MessageType.Error);
                return;
            }

            GUIContent[] options = GetOptions();
            int lastLayerIndex = -1;
            string lastLayerName = LayerMask.LayerToName(property.intValue);
            for (int i = options.Length - 1; i >= 0; i--)
            {
                if (lastLayerName == options[i].text)
                {
                    lastLayerIndex = i;
                    break;
                }
            }

            EditorGUI.BeginChangeCheck();
            int selectedLayerIndex = EditorGUI.Popup(position, label, lastLayerIndex, options);
            if (EditorGUI.EndChangeCheck())
            {
                if (selectedLayerIndex > -1 && selectedLayerIndex != lastLayerIndex)
                {
                    property.intValue = LayerMask.NameToLayer(options[selectedLayerIndex].text);
                }
            }
        }

        private GUIContent[] GetOptions()
        {
            string[] layers = UnityEditorInternal.InternalEditorUtility.layers;
            var options = new List<GUIContent>();
            for (int i = 0; i < layers.Length; i++)
            {
                options.Add(new GUIContent(layers[i]));
            }

            return options.ToArray();
        }
    }
}