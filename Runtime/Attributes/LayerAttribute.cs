using System;
using UnityEngine;

namespace MbsCore.Extensions
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class LayerAttribute : PropertyAttribute { }
}