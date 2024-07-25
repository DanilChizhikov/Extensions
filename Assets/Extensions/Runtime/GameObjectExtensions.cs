using System.Collections.Generic;
using UnityEngine;

namespace MbsCore.Extensions
{
    public static class GameObjectExtensions
    {
        public static GameObject SetLayer(this GameObject source, int layer, bool includeChild = false)
        {
            source.layer = layer;
            if (includeChild)
            {
                int childCount = source.transform.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    Transform child = source.transform.GetChild(i);
                    child.gameObject.SetLayer(layer, true);
                }
            }
            
            return source;
        }

        public static int GetChildNonAlloc(this GameObject gameObject, ref GameObject[] childGameObjects, bool recursive = false)
        {
            int arraySize = childGameObjects.Length;
            int childCount = gameObject.GetChildNonAlloc(0, ref childGameObjects);
            if (recursive && childCount < arraySize)
            {
                for (int i = 0; i < childCount; i++)
                {
                    GameObject childGameObject = childGameObjects[i];
                    childCount += childGameObject.GetChildNonAlloc(childCount, ref childGameObjects);
                    if (childCount >= arraySize)
                    {
                        break;
                    }
                }
            }
            
            return childCount;
        }

        public static List<GameObject> GetChild(this GameObject gameObject, bool recursive = false)
        {
            var result = new List<GameObject>(gameObject.GetChildEnumerable());
            if (recursive)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    result.AddRange(result[i].GetChildEnumerable());
                }
            }

            return result;
        }

        private static int GetChildNonAlloc(this GameObject gameObject, int beginIndex, ref GameObject[] childGameObjects)
        {
            int childCount = Mathf.Min(childGameObjects.Length - beginIndex, gameObject.transform.childCount);
            for (int i = 0; i < childCount; i++)
            {
                childGameObjects[i + beginIndex] = gameObject.transform.GetChild(i).gameObject;
            }

            return childCount;
        }

        private static IEnumerable<GameObject> GetChildEnumerable(this GameObject gameObject)
        {
            int childCount = gameObject.transform.childCount;
            var result = new HashSet<GameObject>(childCount);
            for (int i = 0; i < childCount; i++)
            {
                result.Add(gameObject.transform.GetChild(i).gameObject);
            }

            return result;
        }
    }
}
