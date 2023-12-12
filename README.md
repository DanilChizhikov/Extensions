# Extensions
![](https://img.shields.io/badge/unity-2022.3+-000.svg)

## Description
This package contains many possible extensions to both the codebase and the editor for Unity.

## Table of Contents
- [Getting Started](#Getting-Started)
    - [Install manually (using .unitypackage)](#Install-manually-(using-.unitypackage))
    - [Install via UPM (using Git URL)](#Install-via-UPM-(using-Git-URL))
- [Extensions](#Extensions)
    - [Editor](#Editor)
    - [Runtime](#Runtime)
    - [Inspector](#Inspector)
- [License](#License)

## Getting Started
Prerequisites:
- [GIT](https://git-scm.com/downloads)
- [Unity](https://unity.com/releases/editor/archive) 2022.3+

### Install manually (using .unitypackage)
1. Download the .unitypackage from [releases](https://github.com/DanilChizhikov/Extensions/releases/) page.
2. Open Extensions.x.x.x.unitypackage

### Install via UPM (using Git URL)
1. Navigate to your project's Packages folder and open the manifest.json file.
2. Add this line below the "dependencies": { line
    - ```json title="Packages/manifest.json"
      "com.danilchizhikov.extensions": "https://github.com/DanilChizhikov/Extensions.git?path=Assets/Extensions#0.0.1",
      ```
UPM should now install the package.

## Extensions

### Editor
1. ScriptableObjectUtility
```csharp
public static class ScriptableObjectUtility
{
    private const string FilterTemplate = "t: {0}";
    
    /// <summary>
    /// Returns founded ScriptableObjects implementations of type. Work only in Editor!
    /// </summary>
    public static IEnumerable<T> GetImplementations<T>() where T : ScriptableObject;
    
    /// <summary>
    /// Returns founded ScriptableObject implementation of type. Work only in Editor!
    /// </summary>
    public static T GetImplementation<T>() where T : ScriptableObject;
}
```
### Runtime

1. Vectors
   - Vector3Math
```csharp
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
                                              Vector3 targetPosition, Vector3 targetVelocity);
    
    /// <summary>
    /// Calculates first-order intercept using relative target position
    /// </summary>
    /// <param name="speed">Intercept speed</param>
    /// <param name="targetRelativePosition">Relative target position</param>
    /// <param name="targetRelativeVelocity">Relative target velocity</param>
    /// <returns>Returns the time of intercept</returns>
    public static float FirstOrderInterceptTime(float speed, Vector3 targetRelativePosition, Vector3 targetRelativeVelocity);

    public static Vector3 GetInterceptPoint(Vector3 position, float moveSpeed, Vector3 targetPosition,
                                            Vector3 targetVelocity, out bool interseptionExist);

    public static Vector3 GetInterceptPoint(Vector3 position, float moveSpeed, Vector3 targetPosition, Vector3 targetVelocity);
}
```

 - VectorExtensions
```csharp
public static partial class VectorExtensions
{
    public static Vector3 To3D(this Vector2 vector);
    public static float GetMin(this Vector2 vector);
    public static float GetMax(this Vector2 vector);
    public static float GetRandom(this Vector2 vector);
    public static Vector2 Sort(this Vector2 vector);
    public static Vector2 Abs(this Vector2 v2);
    public static float Distance2D(this Vector2 v1, Vector3 v2);
    public static float SqrMagnitude2D(this Vector2 v1, Vector3 v2);
    public static Vector2 To2D(this Vector3 vector);
    public static Vector3 ToZeroY(this Vector3 vector);
    public static float GetMin(this Vector3 vector);
    public static float GetMiddleValue(this Vector3 vector);
    public static float Distance2D(this Vector3 v1, Vector3 v2);
    public static float Distance2D(this Vector3 v1, Vector2 v2);
    public static float SqrMagnitude2D(this Vector3 v1, Vector3 v2);
    public static float SqrMagnitude2D(this Vector3 v1, Vector2 v2);
}
```
### Inspector

1. Layers
```csharp
[AttributeUsage(AttributeTargets.Field)]
public sealed class LayerAttribute : PropertyAttribute { }
```
## License

MIT