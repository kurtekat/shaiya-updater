#include <filesystem>
#include <fstream>
#include <iostream>
#include <map>
#include <memory>
#include <string>
#include <vector>
#include <Updater.Data/include/Convert.h>
#include <Updater.Data/include/Data.h>
#include <Updater.Data/include/Saf.h>
#include <Updater.Data/include/Sah.h>
#include <Updater.Data/include/SFile.h>
#include <Updater.Data/include/SFolder.h>
#include "Native.h"
using namespace Updater::Data;

void Native_DataPatcher(void(*progressCallback)())
{
    auto target = std::make_unique<Data>("data.sah", "data.saf");
    target->sah->read();

    auto update = std::make_unique<Data>("update.sah", "update.saf");
    update->sah->read();

    for (const auto& [path, patch] : update->sah->files)
    {
        if (auto it = target->sah->files.find(path); it != target->sah->files.end())
        {
            auto& file = it->second;
            if (patch->length > file->length)
            {
                auto buffer = update->saf->readFile(patch->offset, patch->length);
                if (buffer.empty())
                    throw std::runtime_error::exception();

                auto offset = target->saf->writeFile(buffer);
                if (offset == -1)
                    throw std::runtime_error::exception();

                target->saf->eraseFile(file->offset, file->length);
                file->offset = offset;
                file->length = patch->length;
            }
            else
            {
                auto buffer = update->saf->readFile(patch->offset, patch->length);
                if (buffer.empty())
                    throw std::runtime_error::exception();

                target->saf->eraseFile(file->offset, file->length);
                if (target->saf->writeFile(file->offset, buffer))
                    throw std::runtime_error::exception();

                file->length = patch->length;
            }
        }
        else
        {
            auto buffer = update->saf->readFile(patch->offset, patch->length);
            if (buffer.empty())
                throw std::runtime_error::exception();

            auto offset = target->saf->writeFile(buffer);
            if (offset == -1)
                throw std::runtime_error::exception();

            patch->offset = offset;
            auto parentFolder = target->sah->ensureFolderExists(patch->parentFolder->path);
            parentFolder->files.insert({ patch->name, patch });

            target->sah->files.insert({ patch->path, patch });
            target->sah->fileCount = Convert::toInt32(target->sah->files.size());
        }

        if (progressCallback)
            progressCallback();
    }

    target->sah->write();
    std::remove("update.sah");
    std::remove("update.saf");
}

void Native_RemoveFiles(void(*progressCallback)())
{
    auto data = std::make_unique<Data>("data.sah", "data.saf");
    data->sah->read();

    std::ifstream list("delete.lst");
    if (!list)
        return;

    std::vector<std::filesystem::path> paths;
    std::string line;

    while (std::getline(list, line))
    {
        std::filesystem::path path(line);
        paths.push_back(path.make_preferred());
    }

    list.close();

    for (const auto& path : paths)
    {
        if (auto it = data->sah->files.find(path); it != data->sah->files.end())
        {
            auto& file = it->second;
            file->parentFolder->files.erase(file->name);

            data->saf->eraseFile(file->offset, file->length);
            data->sah->files.erase(it);
            data->sah->fileCount = Convert::toInt32(data->sah->files.size());
        }

        if (progressCallback)
            progressCallback();
    }

    data->sah->write();
    std::remove("delete.lst");
}
