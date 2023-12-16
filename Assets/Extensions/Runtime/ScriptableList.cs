using System;
using System.Collections.Generic;
using UnityEngine;

namespace MbsCore.Extensions.Runtime
{
    [Serializable]
    public class ScriptableList<T> : List<T>, ISerializationCallbackReceiver where T : ScriptableObject
    {
        [SerializeField] private T[] _scriptableObjects = Array.Empty<T>();
		
        public void OnBeforeSerialize()
        {
            _scriptableObjects = ToArray();
        }
        
        public void OnAfterDeserialize()
        {
            Clear();
            AddRange(_scriptableObjects);
        }
    }
}