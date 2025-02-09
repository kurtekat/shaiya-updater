#pragma once
#include <filesystem>
#include <map>
#include <memory>
#include <string>
#include "PathHelper.h"

namespace Updater::Data
{
    class SFile;
    class SFolder;

    class Sah final
    {
    public:

        std::filesystem::path path;
        std::string signature;
        int32_t unknown; // to-do
        int32_t fileCount;
        std::shared_ptr<SFolder> rootFolder;

        std::map<std::filesystem::path, std::shared_ptr<SFile>, PathHelper::CompareIgnoreCaseLT> files;
        std::map<std::filesystem::path, std::shared_ptr<SFolder>, PathHelper::CompareIgnoreCaseLT> folders;

        /// <summary>
        /// Initializes a new instance of the Sah class.
        /// </summary>
        /// <param name="path"></param>
        Sah(const std::filesystem::path& path)
            : path(path), signature("SAH"), unknown(0), fileCount(0), rootFolder(nullptr)
        {
        }

        std::shared_ptr<SFolder> ensureFolderExists(const std::filesystem::path& path);
        void read();
        void write();

    public:

        Sah(const Sah&) = delete;
        Sah& operator=(const Sah&) = delete;
    };
}
