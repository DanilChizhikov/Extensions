using System;
using System.Collections.Generic;
using UnityEngine;

namespace MbsCore.Extensions.Runtime
{
    [Serializable]
    public class ScriptableList<T> : List<T>, ISerializationCallbackReceiver where T : ScriptableObject
    {
        [SerializeField] private T[] _items = Array.Empty<T>();
		
        public void OnBeforeSerialize()
        {
            _items = ToArray();
        }
        
        public void OnAfterDeserialize()
        {
            Clear();
            AddRange(_items);
        }
    }
}