# Shaiya Updater

## Environment

Windows 10

Visual Studio 2022

C# 12

Windows Presentation Foundation (WPF)

## Prerequisites

[.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

## Dependencies

[Microsoft.AspNet.WebApi.Client](https://www.nuget.org/packages/Microsoft.AspNet.WebApi.Client/)

[Microsoft.Extensions.Configuration.Ini](https://www.nuget.org/packages/Microsoft.Extensions.Configuration.Ini/)

[Parsec.Shaiya.Data](https://www.nuget.org/packages/Parsec.Shaiya.Data/)

## Build

Use **Publish** instead of **Build** to output a single executable. (recommended)

## Screenshots

![Capture](https://github.com/kurtekat/shaiya-updater/assets/142125482/ee526f55-5a0f-45fa-a6ed-d231434b21f1)

# Documentation

This project is designed to be like the original application. Users are expected to design the interface and develop the code to suit their needs. The source code is shared as-is, with little or no support from the author(s).

## Client Configuration

```ini
; Version.ini
[Version]
CheckVersion=3
CurrentVersion=1
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
PatchFileVersion=10
UpdaterVersion=2
```
