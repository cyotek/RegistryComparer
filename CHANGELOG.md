Change Log
==========

2.0.1
-----

### Fixed
* Fixed a crash that could occur when trying to obtain values from empty `RegistryKeySnapshotCollection` or `RegistryValueSnapshotCollection` instances

2.0.0
-----

### Changed
* Target framework changed from .NET 3.5 to 4.6
* UI tweaks and spelling corrections

### Fixed
* Fixed a crash that occurred if a value did not have a type (`REG_NONE`)
* Fixed a crash if an `IOException` was thrown calling `RegistryKey.GetValueNames`

1.0.0
-----

* Initial Release
