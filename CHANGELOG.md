# Changelog

## [0.1.1] - 2025-10-11

### Added
- **ScriptableObjects**
  - Added ScriptableObjectCreateWindow for creating ScriptableObjects

## [0.1.0] - 2025-10-11

### Added
- **Vector Extensions**
  - Added comprehensive set of extension methods for Vector2 and Vector3
  - Added Vector3Math utility for advanced vector calculations
  - Added FirstOrderIntercept for predictive targeting

- **Array Extensions**
  - Added Add, AddRange, Contains, and other useful array operations
  - Added Copy and Reverse methods for array manipulation

- **GameObject Extensions**
  - Added SetLayer with optional child inclusion
  - Added GetChild methods with recursive search option

- **Type System**
  - Added TypeExtensions for runtime type inspection
  - Added support for finding type implementations
  - Added custom type resolution by name

- **Editor Tools**
  - Added ScriptableObjectUtility for finding ScriptableObject instances
  - Implemented ScriptableList drawer with custom editor UI
  - Added support for custom property drawers

### Changed
- Reorganized project structure for better maintainability
- Improved documentation and code comments
- Optimized performance of extension methods

### Fixed
- Fixed null reference issues in array extensions
- Resolved type resolution edge cases
- Fixed layer application in GameObject extensions