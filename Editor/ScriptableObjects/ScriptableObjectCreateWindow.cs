using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DTech.Extensions.Editor
{
    public sealed class ScriptableObjectCreatorWindow : EditorWindow
    {
        private const string WindowTitle = "Scriptable Object Creator";
        private const string AssetExpansion = ".asset";
        private const int DefaultSearchCount = 20;
        private const int MaxValidateNameCount = 100;

        private static readonly MethodInfo _getActiveFolderPathMethod =
            typeof(ProjectWindowUtil).GetMethod("TryGetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic);

        private static readonly object[] _getActiveFolderPathMethodArguments = { null };
        private static readonly GUIStyle _boldStyle = EditorStyles.boldLabel;

        private readonly List<ScriptableObjectItem> _foundItems = new();
        
        private int _searchCount;
        private string _searchType = string.Empty;
        private bool _includeUnityTypes = false;
        private Vector2 _scrollPos;

        [MenuItem("Window/DTech/" + WindowTitle, false)]
        public static void ShowWindow()
        {
            var window = GetWindow<ScriptableObjectCreatorWindow>(true, WindowTitle);
            window.minSize = new Vector2(400f, 400f);
            window.Show();
        }

        private void OnEnable()
        {
            _searchCount = DefaultSearchCount;
            _includeUnityTypes = false;
            _foundItems.Clear();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("Enter Manager Name:", EditorStyles.boldLabel);
            string newSearchType = EditorGUILayout.TextField(_searchType);
            _includeUnityTypes = EditorGUILayout.Toggle("Include Unity Types", _includeUnityTypes);
            if (newSearchType != _searchType || GUILayout.Button("Search"))
            {
                _searchType = newSearchType;
                FindTypes();
            }

            EditorGUILayout.EndHorizontal();
            
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, "box");
            for (int i = 0; i < _foundItems.Count; i++)
            {
                DrawType(_foundItems[i]);
            }
            
            EditorGUILayout.EndScrollView();
            
            if (GUILayout.Button("Create Selected"))
            {
                ScriptableObjectItem[] selectedItems = _foundItems.Where(i => i.IsSelected).ToArray();
                foreach (ScriptableObjectItem item in selectedItems)
                {
                    CreateType(item.Type);
                }
            }
        }

        private void DrawType(ScriptableObjectItem item)
        {
            EditorGUILayout.BeginHorizontal("box");
            
            item.IsSelected = EditorGUILayout.Toggle(item.IsSelected, GUILayout.MaxWidth(20f));
            
            EditorGUILayout.BeginVertical();
            GUILayout.Label($"Namespace: {item.Type.Namespace}");
            GUILayout.Label(item.Type.Name, _boldStyle);
            EditorGUILayout.EndVertical();
            
            if (GUILayout.Button("Create"))
            {
                CreateType(item.Type);
            }
            
            EditorGUILayout.EndHorizontal();
        }

        private void CreateType(Type type)
        {
            if (type == null)
            {
                return;
            }

            ScriptableObject asset = CreateInstance(type);
            if (TryGetActiveFolderPath(out string path) &&
                TryGetValidateNameForCreateInstance(path, type.Name, out string validatedPath))
            {
                AssetDatabase.CreateAsset(asset, validatedPath);
                AssetDatabase.Refresh();
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = asset;
            }
            else
            {
                Debug.LogError("Failed to determine valid creation path for " + type.Name);
            }
        }

        private void FindTypes()
        {
            _foundItems.Clear();
            if (!string.IsNullOrEmpty(_searchType))
            {
                FillWithValidTypes();
            }

            Repaint();
        }

        private void FillWithValidTypes()
        {
            IReadOnlyList<Type> types = GetAllTypes();
            for (int i = 0; i < types.Count; i++)
            {
                Type type = types[i];
                if (_foundItems.Count >= _searchCount)
                {
                    break;
                }

                if (type == null)
                {
                    continue;
                }

                if (!string.IsNullOrEmpty(type.Namespace) && !_includeUnityTypes &&
                    (type.Namespace.Contains("UnityEditor") || type.Namespace.Contains("UnityEngine") || type.Namespace.Contains("Unity.")))
                {
                    continue;
                }

                var item = new ScriptableObjectItem(type);
                _foundItems.Add(item);
            }
        }

        private IReadOnlyList<Type> GetAllTypes()
        {
            var typeCollection = TypeCache.GetTypesDerivedFrom<ScriptableObject>();
            return typeCollection.Where(Validate).OrderBy(t => t.Name).ToArray();
        }

        private bool Validate(Type type)
        {
            if (string.IsNullOrEmpty(_searchType))
            {
                return false;
            }

            string typeName = type.Name;
            string upperName = string.Concat(from x in typeName where char.IsUpper(x) select x);
            return typeof(ScriptableObject).IsAssignableFrom(type) &&
                   (typeName.StartsWith(_searchType, StringComparison.OrdinalIgnoreCase) ||
                    upperName.StartsWith(_searchType, StringComparison.OrdinalIgnoreCase));
        }

        private bool TryGetActiveFolderPath(out string path)
        {
            _getActiveFolderPathMethodArguments[0] = null;
            object result = _getActiveFolderPathMethod.Invoke(null, _getActiveFolderPathMethodArguments);
            bool success = (bool)result;
            path = success ? (string)_getActiveFolderPathMethodArguments[0] : null;
            return success;
        }

        private bool TryGetValidateNameForCreateInstance(string path, string fileName, out string validatedPath)
        {
            bool validPathFound = false;
            string fullPath = path + "/" + fileName;
            string checkPath = null;

            for (int i = 0; i < MaxValidateNameCount && !validPathFound; i++)
            {
                checkPath = fullPath + (i != 0 ? $"({i})" : string.Empty) + AssetExpansion;
                validPathFound = !AssetDatabase.LoadAssetAtPath(checkPath, typeof(Object));
            }

            validatedPath = validPathFound ? checkPath : null;
            return validPathFound;
        }
    }
}