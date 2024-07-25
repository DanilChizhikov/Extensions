using UnityEngine;

namespace MbsCore.Extensions
{
    public static class Vector3Math
    {
        public const float MinSqrMagnitude = 0.001f;
        
        /// <summary>
        /// Calculates the first-order intercept point
        /// </summary>
        /// <param name="source">Initial position</param>
        /// <param name="sourceVelocity">Velocity of the initial position</param>
        /// <param name="speed">Intercept speed</param>
        /// <param name="targetPosition">Target position</param>
        /// <param name="targetVelocity">Target velocity</param>
        /// <returns>Returns the intercept point</returns>
        public static Vector3 FirstOrderIntercept(Vector3 source, Vector3 sourceVelocity, float speed,
                                                  Vector3 targetPosition, Vector3 targetVelocity)
        {
            Vector3 targetRelativePosition = targetPosition - source;
            Vector3 targetRelativeVelocity = targetVelocity - sourceVelocity;
            float t = FirstOrderInterceptTime(speed, targetRelativePosition, targetRelativeVelocity);
            return targetPosition + t * (targetRelativeVelocity);
        }
        
        /// <summary>
        /// Calculates first-order intercept using relative target position
        /// </summary>
        /// <param name="speed">Intercept speed</param>
        /// <param name="targetRelativePosition">Relative target position</param>
        /// <param name="targetRelativeVelocity">Relative target velocity</param>
        /// <returns>Returns the time of intercept</returns>
        public static float FirstOrderInterceptTime(float speed, Vector3 targetRelativePosition, Vector3 targetRelativeVelocity)
        {
            float vSqr = targetRelativeVelocity.sqrMagnitude;
            float a = vSqr - speed * speed;
            if (vSqr < MinSqrMagnitude)
            {
                return 0f;
            }

            if (Mathf.Approximately(a, 0))
            {
                return Mathf.Max(-Vector3.Dot(targetRelativePosition, targetRelativeVelocity) / vSqr, 0f);
            }

            float b = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
            float c = targetRelativePosition.sqrMagnitude;
            float det = b * b - 4f * a * c;
            float result = 0f;
            if (det > 0f)
            {
                float sqrtDet = Mathf.Sqrt(det);
                float t1 = (-b + sqrtDet) / (2f * a);
                float t2 = (-b - sqrtDet) / (2f * a);
                if (t1 > 0f)
                {
                    result = t2 > 0f ? Mathf.Min(t1, t2) : t1;
                }
                else
                {
                    result = Mathf.Max(t2, 0f);
                }
            }
            else if (Mathf.Approximately(det, 0f))
            {
                result = Mathf.Max(-b / (2f * a), 0f);
            }

            return result;
        }

        public static Vector3 GetInterceptPoint(Vector3 position, float moveSpeed, Vector3 targetPosition,
                                                Vector3 targetVelocity, out bool interseptionExist)
        {
            Vector3 interceptPoint = FirstOrderIntercept(position, Vector3.zero, moveSpeed, targetPosition, targetVelocity);
            interseptionExist = Vector3.Distance(interceptPoint, targetPosition) > 0.1f;
            return interceptPoint;
        }

        public static Vector3 GetInterceptPoint(Vector3 position, float moveSpeed, Vector3 targetPosition, Vector3 targetVelocity)
        {
            Vector3 interceptPoint = FirstOrderIntercept(position, Vector3.zero, moveSpeed, targetPosition, targetVelocity);
            return interceptPoint;
        }
    }
}