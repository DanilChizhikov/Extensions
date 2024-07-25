using UnityEngine;

namespace MbsCore.Extensions
{
    public class Holder : MonoBehaviour
    {
        [SerializeReference] private SerializedType _type1 = new();
    }
}