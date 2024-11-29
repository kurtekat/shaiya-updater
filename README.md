# Shaiya Updater

## Environment

Windows 10

Visual Studio 2022

C# 10

Windows Presentation Foundation (WPF)

## Prerequisites

[.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

## Dependencies

[Microsoft.AspNet.WebApi.Client](https://www.nuget.org/packages/Microsoft.AspNet.WebApi.Client/)

[Parsec.Shaiya.Data](https://www.nuget.org/packages/Parsec.Shaiya.Data/)

## Build

Use **Publish** instead of **Build** to output a single executable. (recommended)

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

This project is designed to be like the original application. Users are expected to design the interface and develop the code to suit their needs. The source code is shared as-is, with little or no support from the author(s).

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

### Notes

For those who want to patch the original updater in their client:

Use a disassembler to find the updater version. Add 1 to the number, then assign it to the new updater and the server-side configuration file.

```
0040729A  68 90AC4500    PUSH 0045AC90  ;  ASCII ".\UpdateVersion-mjw-.ini"
0040729F  68 FF000000    PUSH 0FF
004072A4  8D5424 18      LEA EDX,DWORD PTR SS:[ESP+18]
004072A8  52             PUSH EDX
004072A9  50             PUSH EAX
004072AA  68 80AC4500    PUSH 0045AC80  ;  ASCII "UpdaterVersion"
004072AF  68 F0A14500    PUSH 0045A1F0  ;  ASCII "Version"
004072B4  FFD6           CALL ESI
004072B6  85C0           TEST EAX,EAX
004072B8  74 10          JE SHORT 004072CA
004072BA  8D4424 10      LEA EAX,DWORD PTR SS:[ESP+10]
004072BE  50             PUSH EAX
004072BF  E8 406E0300    CALL 0043E104
004072C4  83C4 04        ADD ESP,4
004072C7  8947 5C        MOV DWORD PTR DS:[EDI+5C],EAX
; compare versions
004072CA  837F 5C 1A     CMP DWORD PTR DS:[EDI+5C],1A  ;  26
004072CE  76 04          JBE SHORT 004072D4
```
