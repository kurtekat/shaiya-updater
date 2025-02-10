#include "Native.h"
using namespace System;
using namespace System::Runtime::InteropServices;

namespace Updater::Interop
{
    public ref class Function
    {
    public:

        static void DataPatcher(Action^ progressCallback)
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

        static void RemoveFiles(Action^ progressCallback)
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
    };
}
