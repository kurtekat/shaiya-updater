#pragma once
#include <filesystem>
#include <memory>

namespace Updater::Data
{
    class Saf;
    class Sah;

    class Data final
    {
    public:

        std::unique_ptr<Sah> sah;
        std::unique_ptr<Saf> saf;

        /// <summary>
        /// Initializes a new instance of the Data class.
        /// </summary>
        /// <param name="sahPath"></param>
        /// <param name="safPath"></param>
        Data(const std::filesystem::path& sahPath, const std::filesystem::path& safPath)
            : sah(nullptr), saf(nullptr)
        {
            sah = std::make_unique<Sah>(sahPath);
            saf = std::make_unique<Saf>(safPath);
        }

    public:

        Data(const Data&) = delete;
        Data& operator=(const Data&) = delete;
    };
}
