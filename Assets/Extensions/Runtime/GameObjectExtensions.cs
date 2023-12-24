using UnityEngine;

namespace MbsCore.Extensions.Runtime
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
    }
}
