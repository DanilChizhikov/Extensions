using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MbsCore.Extensions.Editor
{
    public static class ScriptableObjectUtility
    {
        private const string FilterTemplate = "t: {0}";
        
        /// <summary>
        /// Returns founded ScriptableObjects implementations of type. Work only in Editor!
        /// </summary>
        public static IEnumerable<T> GetImplementations<T>() where T : ScriptableObject
        {
            var result = new HashSet<T>();
            string filter = string.Format(FilterTemplate, typeof(T));
            string[] assetGuides = AssetDatabase.FindAssets(filter);
            for (int i = assetGuides.Length - 1; i >= 0; i--)
            {
                string assetGuide = assetGuides[i];
                string assetPath = AssetDatabase.GUIDToAssetPath(assetGuide);
                var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                result.Add(asset);
            }

            return result;
        }
        
        /// <summary>
        /// Returns founded ScriptableObject implementation of type. Work only in Editor!
        /// </summary>
        public static T GetImplementation<T>() where T : ScriptableObject
        {
            string filter = string.Format(FilterTemplate, typeof(T));
            string[] assetGuides = AssetDatabase.FindAssets(filter);
            int guidesLength = assetGuides.Length;
            if (guidesLength > 0)
            {
                string assetGuide = assetGuides[0];
                string assetPath = AssetDatabase.GUIDToAssetPath(assetGuide);
                return AssetDatabase.LoadAssetAtPath<T>(assetPath);
            }

            return null;
        }
    }
}