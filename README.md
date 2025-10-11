# Extensions
[![Unity Version](https://img.shields.io/badge/unity-2022.3+-000.svg)](https://unity3d.com/get-unity/download/archive)

A comprehensive collection of useful extensions and utilities for Unity development, providing enhanced functionality for common tasks in both runtime and editor scripts.

## Table of Contents
- [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Manual Installation](#manual-installation)
    - [UPM Installation](#upm-installation)
- [Features](#features)
  - [Runtime Extensions](#runtime-extensions)
    - [Vector Extensions](#vector-extensions)
    - [Array Extensions](#array-extensions)
    - [GameObject Extensions](#gameobject-extensions)
    - [Type Extensions](#type-extensions)
  - [Editor Extensions](#editor-extensions)
    - [ScriptableObject Utility](#scriptableobject-utility)
  - [Examples](#examples)
    - [Using Vector Extensions](#using-vector-extensions)
    - [Using Array Extensions](#using-array-extensions)
- [License](#license)

## Getting Started

### Prerequisites
- [GIT](https://git-scm.com/downloads)
- [Unity](https://unity.com/releases/editor/archive) 2022.3+

### Manual Installation
1. Download the .unitypackage from the [releases](https://github.com/DanilChizhikov/Extensions/releases/) page.
2. Import Extensions.x.x.x.unitypackage into your project.

### UPM Installation
1. Open the manifest.json file in your project's Packages folder.
2. Add the following line to the dependencies section:
    ```json
    "com.dtech.extensions": "https://github.com/DanilChizhikov/Extensions.git",
    ```
3. Unity will automatically import the package.

If you want to set a target version, Extensions uses the `v*.*.*` release tag so you can specify a version like #v0.1.0.

For example `https://github.com/DanilChizhikov/Extensions.git#v0.1.0`.

## Features

### Runtime Extensions

#### Vector Extensions
- **Vector2/3 Extensions**: Extended functionality for Unity's Vector2 and Vector3 types
  - `To3D()`/`To2D()`: Convert between Vector2 and Vector3
  - `Distance2D()`: Calculate 2D distance ignoring Z-axis
  - `GetMin()`/`GetMax()`: Get minimum/maximum component values
  - `Abs()`: Get absolute values of all components

#### Array Extensions
- Extended functionality for arrays
  - `Add()`: Add item to array
  - `AddRange()`: Concatenate arrays
  - `Contains()`: Check if array contains item
  - `Copy()`: Create a shallow copy of the array

#### GameObject Extensions
- `SetLayer()`: Set layer for GameObject and optionally its children
- `GetChild()`: Get children with optional recursive search

#### Type Extensions
- `GetImplementations()`: Get non-abstract implementations of a type
- `GetAllImplementations()`: Get all implementations including abstract types
- `CustomGetType()`: Get type by name with additional search paths

### Editor Extensions

#### ScriptableObject Utility
- `GetImplementation<T>()`: Find and return a ScriptableObject implementation
- `GetImplementations<T>()`: Find all ScriptableObject implementations of a type

## Examples

### Using Vector Extensions
```csharp
// Get 2D distance ignoring Z-axis
float distance = transform.position.Distance2D(targetPosition);

// Convert Vector2 to Vector3
Vector3 position = new Vector2(1, 2).To3D();
```

### Using Array Extensions
```csharp
// Add item to array
var numbers = new int[] { 1, 2, 3 };
numbers = numbers.Add(4); // [1, 2, 3, 4]

// Check if array contains item
bool hasTwo = numbers.Contains(2); // true
```

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.