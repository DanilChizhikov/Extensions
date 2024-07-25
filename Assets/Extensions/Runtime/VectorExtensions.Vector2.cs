using UnityEngine;

namespace MbsCore.Extensions
{
    public static partial class VectorExtensions
    {
        public static Vector3 To3D(this Vector2 vector)
        {
            return new Vector3(vector.x, 0, vector.y);
        }
        
        public static float GetMin(this Vector2 vector)
        {
            return Mathf.Min(vector.x, vector.y);
        }
        
        public static float GetMax(this Vector2 vector)
        {
            return Mathf.Max(vector.x, vector.y);
        }

        public static float GetRandom(this Vector2 vector)
        {
            return Random.Range(vector.x, vector.y);
        }

        public static Vector2 Sort(this Vector2 vector)
        {
            if (vector.y < vector.x)
            {
                return new Vector2(vector.y, vector.x);
            }

            return vector;
        }
        
        public static Vector2 Abs(this Vector2 v2)
        {
            return new Vector2(Mathf.Abs(v2.x), Mathf.Abs(v2.y));
        }
        
        public static float Distance2D(this Vector2 v1, Vector3 v2)
        {
            return Vector2.Distance(v1, v2.To2D());
        }
        
        public static float SqrMagnitude2D(this Vector2 v1, Vector3 v2)
        {
            return (v1 - v2.To2D()).sqrMagnitude;
        }

        public static Vector2 Max(this Vector2 v1, Vector2 v2)
        {
            return new Vector2
                    {
                            x = Mathf.Max(v1.x, v2.x),
                            y = Mathf.Max(v1.y, v2.y),
                    };
        }
        
        public static Vector2 Min(this Vector2 v1, Vector2 v2)
        {
            return new Vector2
                    {
                            x = Mathf.Min(v1.x, v2.x),
                            y = Mathf.Min(v1.y, v2.y),
                    };
        }
    }
}