using UnityEngine;

namespace MbsCore.Extensions.Runtime
{
    public static partial class VectorExtensions
    {
        public static Vector2 To2D(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.z);
        }

        public static Vector3 ToZeroY(this Vector3 vector)
        {
            return new Vector3(vector.x, 0, vector.z);
        }

        public static float GetMin(this Vector3 vector)
        {
            return Mathf.Min(Mathf.Min(vector.x, vector.y), Mathf.Min(vector.y, vector.z));
        }

        public static float GetMiddleValue(this Vector3 vector)
        {
            return (vector.x + vector.y + vector.z) / 3;
        }

        public static float Distance2D(this Vector3 v1, Vector3 v2)
        {
            return Vector2.Distance(v1.To2D(), v2.To2D());
        }

        public static float Distance2D(this Vector3 v1, Vector2 v2)
        {
            return Vector2.Distance(v1.To2D(), v2);
        }

        public static float SqrMagnitude2D(this Vector3 v1, Vector3 v2)
        {
            return (v1.To2D() - v2.To2D()).sqrMagnitude;
        }

        public static float SqrMagnitude2D(this Vector3 v1, Vector2 v2)
        {
            return (v1.To2D() - v2).sqrMagnitude;
        }
        
        public static Vector3 Max(this Vector3 v1, Vector3 v2)
        {
            return new Vector3
                    {
                            x = Mathf.Max(v1.x, v2.x),
                            y = Mathf.Max(v1.y, v2.y),
                            z = Mathf.Max(v1.z, v2.z),
                    };
        }
        
        public static Vector3 Min(this Vector3 v1, Vector3 v2)
        {
            return new Vector3
                    {
                            x = Mathf.Min(v1.x, v2.x),
                            y = Mathf.Min(v1.y, v2.y),
                            z = Mathf.Min(v1.z, v2.z),
                    };
        }
    }
}