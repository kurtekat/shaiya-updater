#pragma once
using namespace System;

namespace Updater::Interop
{
    public ref class Function sealed
    {
    public:

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progressCallback"></param>
        static void DataBuilder(Action^ progressCallback);

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
