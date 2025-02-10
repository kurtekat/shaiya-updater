#include "include/Function.h"
#include "include/Native.h"
using namespace System;
using namespace System::Runtime::InteropServices;
using namespace Updater::Interop;

void Function::DataPatcher(Action^ progressCallback)
{
    if (!progressCallback)
    {
        Native_DataPatcher();
    }
    else
    {
        auto gch = GCHandle::Alloc(progressCallback);
        auto ptr = Marshal::GetFunctionPointerForDelegate(progressCallback).ToPointer();
        auto callback = static_cast<void(*)()>(ptr);

        Native_DataPatcher(callback);
        gch.Free();
    }
}

void Function::RemoveFiles(Action^ progressCallback)
{
    if (!progressCallback)
    {
        Native_RemoveFiles();
    }
    else
    {
        auto gch = GCHandle::Alloc(progressCallback);
        auto ptr = Marshal::GetFunctionPointerForDelegate(progressCallback).ToPointer();
        auto callback = static_cast<void(*)()>(ptr);

        Native_RemoveFiles(callback);
        gch.Free();
    }
}
