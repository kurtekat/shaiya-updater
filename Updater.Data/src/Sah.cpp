#include <filesystem>
#include <fstream>
#include <functional>
#include <map>
#include <memory>
#include <string>
#include <vector>
#include "include/Convert.h"
#include "include/PathHelper.h"
#include "include/Sah.h"
#include "include/SBinaryReader.h"
#include "include/SBinaryWriter.h"
#include "include/SFile.h"
#include "include/SFolder.h"
using namespace Updater::Data;

std::shared_ptr<SFolder> Sah::ensureFolderExists(const std::filesystem::path& path)
{
    if (auto it = folders.find(path); it != folders.end())
        return it->second;

    std::shared_ptr<SFolder> currentFolder = rootFolder;

    auto parts = PathHelper::split(path);
    for (const auto& part : parts)
    {
        auto& subfolders = currentFolder->subfolders;
        if (auto it = subfolders.find(part); it != subfolders.end())
        {
            currentFolder = it->second;
        }
        else
        {
            auto subfolder = std::make_shared<SFolder>(part, currentFolder);
            subfolders.insert({ subfolder->name, subfolder });
            folders.insert({ subfolder->path, subfolder });
            currentFolder = subfolder;
        }
    }

    return currentFolder;
}

void Sah::read()
{
    std::ifstream stream(path, std::ios::binary);
    SBinaryReader binaryReader(stream);

    signature = binaryReader.readChars(3);
    unknown = binaryReader.readInt32();
    fileCount = binaryReader.readInt32();
    binaryReader.ignore(40);
    
    auto rootFolderName = binaryReader.readString();
    rootFolderName.pop_back();
    rootFolder = std::make_shared<SFolder>(rootFolderName);

    std::function<void(std::shared_ptr<SFolder>&)> readFolder;
    readFolder = [this, &binaryReader, &readFolder](auto& currentFolder)
        {
            folders.insert({ currentFolder->path, currentFolder });

            auto fileCount = binaryReader.readInt32();
            // Decrypt here (e.g., fileCount ^= 1234)
            for (int i = 0; i < fileCount; ++i)
            {
                auto fileName = binaryReader.readString();
                fileName.pop_back();

                auto file = std::make_shared<SFile>(fileName, currentFolder);
                file->offset = binaryReader.readInt64();
                file->length = binaryReader.readUInt32();
                file->timestamp = binaryReader.readInt32();

                currentFolder->files.insert({ file->name, file });
                files.insert({ file->path, file });
            }

            auto subfolderCount = binaryReader.readInt32();
            for (int i = 0; i < subfolderCount; ++i)
            {
                auto subfolderName = binaryReader.readString();
                subfolderName.pop_back();

                auto subfolder = std::make_shared<SFolder>(subfolderName, currentFolder);
                readFolder(subfolder);
                currentFolder->subfolders.insert({ subfolder->name, subfolder });
            }
        };

    readFolder(rootFolder);
    fileCount = Convert::toInt32(files.size());
    binaryReader.close();
}

void Sah::write()
{
    const std::string zeros(40, 0);
    std::ofstream stream(path, std::ios::binary);
    SBinaryWriter binaryWriter(stream);

    if (signature.size() != 3)
        signature.resize(3);

    binaryWriter.write(signature, 0, 3);
    binaryWriter.write(unknown);
    binaryWriter.write(fileCount);
    binaryWriter.write(zeros, 0, 40);
    binaryWriter.write(rootFolder->name.string());

    std::function<void(const std::shared_ptr<SFolder>&)> writeFolder;
    writeFolder = [this, &binaryWriter, &writeFolder](const auto& currentFolder)
        {
            auto fileCount = Convert::toInt32(currentFolder->files.size());
            // Encrypt here (e.g., fileCount ^= 1234)
            binaryWriter.write(fileCount);

            for (const auto& [name, file] : currentFolder->files)
            {
                binaryWriter.write(name.string());
                binaryWriter.write(file->offset);
                binaryWriter.write(file->length);
                binaryWriter.write(file->timestamp);
            }

            auto subfolderCount = Convert::toInt32(currentFolder->subfolders.size());
            binaryWriter.write(subfolderCount);

            for (const auto& [name, subfolder] : currentFolder->subfolders)
            {
                binaryWriter.write(name.string());
                writeFolder(subfolder);
            }
        };

    writeFolder(rootFolder);
    binaryWriter.write(zeros, 0, 8);
    binaryWriter.close();
}
