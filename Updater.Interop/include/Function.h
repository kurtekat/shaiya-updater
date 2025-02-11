#pragma once
using namespace System;
using namespace System::Runtime::InteropServices;

namespace Updater::Interop
{
    public ref class Function sealed
    {
    public:

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progressCallback"></param>
        static void DataPatcher(Action^ progressCallback);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progressCallback"></param>
        static void RemoveFiles(Action^ progressCallback);
    };
}
