# Shaiya Updater

## Environment

Windows 10

Visual Studio 2022

C# 10

Windows Presentation Foundation (WPF)

## Prerequisites

[Microsoft Visual C++ Redistributable](https://aka.ms/vs/17/release/vc_redist.x86.exe)

[.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

[.NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)

## Dependencies

[Microsoft.AspNet.WebApi.Client](https://www.nuget.org/packages/Microsoft.AspNet.WebApi.Client/)

## Build

Use **Publish** instead of **Build** to output a single .NET executable. The C++ library will be copied to the publish directory.

## System Requirements

OS            | Version           |
--------------|-------------------|
Windows 7     | SP1 *\**          |
Windows 8     | 8.1               |
Windows 10    | Version 1607+     |
Windows 11    | Version 22000+    |

*\** Windows 7 SP1 is supported with Extended Security Updates installed.

## Screenshots

![Capture](https://github.com/kurtekat/shaiya-updater/assets/142125482/ee526f55-5a0f-45fa-a6ed-d231434b21f1)

# Documentation

This project is designed to be like the original application. Users are expected to design the interface and develop the code to suit their needs. The source code is shared as-is, with little or no support from the author.

## Client Configuration

```ini
[Version]
CheckVersion=3
CurrentVersion=1
StartUpdate=UPDATE_END
```

## Server Configuration

https://github.com/kurtekat/kurtekat.github.io

### Web

```csharp
// Updater/Common/Constants.cs
public const string Source = "https://kurtekat.github.io";
public const string WebBrowserSource = "https://google.com";
```

## Patching

### Data

https://www.elitepvpers.com/forum/shaiya-private-server/1953495-tool-shaiya-make-exe-client-updater-patcher.html

https://www.elitepvpers.com/forum/shaiya-pserver-guides-releases/4937732-guide-how-delele-files-client-via-updater.html

### Updater

Assign `UpdaterVersion` and build the application.

```csharp
// Updater/Common/Constants.cs
public const uint UpdaterVersion = 2;
```

Rename the executable to `new_updater` and upload it to the expected location.

```
https://website.com/shaiya/new_updater.exe
```

Assign `UpdaterVersion` in the configuration file.

```ini
; https://website.com/shaiya/UpdateVersion.ini
[Version]
CheckVersion=3
UpdaterVersion=2
PatchFileVersion=10
```
