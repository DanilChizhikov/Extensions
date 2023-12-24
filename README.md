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
      "com.danilchizhikov.extensions": "https://github.com/DanilChizhikov/Extensions.git?path=Assets/Extensions#0.0.6",
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
    public static Vector2 Max(this Vector2 v1, Vector2 v2);
    public static Vector2 Min(this Vector2 v1, Vector2 v2);
    public static Vector3 Max(this Vector3 v1, Vector3 v2);
    public static Vector3 Min(this Vector3 v1, Vector3 v2);
}
```

 - TypeExtensions
```csharp
public static class TypeExtensions
{
    /// <summary>Returns non-abstract and non-generic implementations of type.</summary>
    public static Type[] GetImplementations(this Type baseType);

    /// <summary>Returns implementations of type including non-abstract and non-generic.</summary>
    public static Type[] GetAllImplementations(this Type baseType);

    /// <summary>Returns types that belong to the base type.</summary>
    public static Type[] GetAssignableTypes(this Type baseType);

    /// <summary>Returns the type by its name.</summary>
    public static Type CustomGetType(this string typeName);

    /// <summary>Returns the weight between the original type and its children type.</summary>
    public static int Comparison(this Type type, Type childrenType);
}
```

 - ArrayExtensions
```csharp
public static class ArrayExtensions
{
    /// <summary>
    /// <para>Adds an item to the end of the array.</para>
    /// </summary>
    /// <param name="item">The item to add to the end of the array. (<paramref name="item" /> can be <see langword="null" /> if T is a reference type.)</param>
    public static T[] Add<T>(this T[] source, T item);

    /// <summary>
    /// <para>Adds the elements of the specified collection to the end of the array.</para>
    /// </summary>
    /// <param name="collection">The collection whose elements are added to the end of the array.</param>
    public static T[] AddRange<T>(this T[] source, T[] items);

    /// <summary>
    /// <para>Determines whether the array contains a specific value.</para>
    /// </summary>
    /// <param name="item">The object to locate in the current collection. (<paramref name="item" /> can be <see langword="null" /> if T is a reference type.)</param>
    public static bool Contains<T>(this T[] source, T item);

    /// <summary>
    /// <para>Searches for the specified object and returns the zero-based index of the first occurrence within the entire array.</para>
    /// </summary>
    /// <param name="item">To be added.</param>
    public static int IndexOf<T>(this T[] source, T item);

    /// <summary>
    /// <para>Determines whether every element in the List matches the conditions defined by the specified predicate.</para>
    /// </summary>
    /// <param name="match">The predicate delegate that specifies the check against the elements.</param>
    public static bool TrueForAll<T>(this T[] source, Predicate<T> match);

    /// <summary>
    /// Return a new copy from source array.
    /// </summary>
    /// <returns></returns>
    public static T[] Copy<T>(this T[] source);

    /// <summary>
    /// Return a new reverse copy from source array.
    /// </summary>
    /// <returns></returns>
    public static T[] Reverse<T>(this T[] source);
}
```

2. GameObject
```csharp
public static class GameObjectExtensions
{
    public static GameObject SetLayer(this GameObject source, int layer, bool includeChild = false);
}
```

### Inspector

1. Layers
```csharp
[AttributeUsage(AttributeTargets.Field)]
public sealed class LayerAttribute : PropertyAttribute { }
```

2. ScriptableList
```csharp
[Serializable]
public class ScriptableList<T> : List<T>, ISerializationCallbackReceiver where T : ScriptableObject
{
    [SerializeField] private T[] _scriptableObjects = Array.Empty<T>();
    
    public void OnBeforeSerialize();
    public void OnAfterDeserialize();
}
```

## License

MIT