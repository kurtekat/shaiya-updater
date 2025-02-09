#pragma once
#include <filesystem>
#include <memory>
#include "PathHelper.h"
#include "SFolder.h"

namespace Updater::Data
{
    class SFile final
    {
    public:

        std::filesystem::path path;
        std::filesystem::path name;
        int64_t offset;
        int32_t length;
        int32_t timestamp; // i.e., __time32_t
        std::shared_ptr<SFolder> parentFolder;

        /// <summary>
        /// Initializes a new instance of the SFile class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parentFolder"></param>
        SFile(const std::filesystem::path& name, const std::shared_ptr<SFolder>& parentFolder = nullptr)
            : name(name), parentFolder(parentFolder), offset(0), length(0), timestamp(0)
        {
            if (!parentFolder)
                path = name;
            else
                path = PathHelper::combine(parentFolder->path, name);
        }
    };
}
