#pragma once
#include <filesystem>
#include <vector>

namespace Updater::Data
{
    class Saf final
    {
    public:

        std::filesystem::path path;

        /// <summary>
        /// Initializes a new instance of the Saf class.
        /// </summary>
        /// <param name="path"></param>
        Saf(const std::filesystem::path& path)
            : path(path)
        {
        }

        int eraseFile(int64_t offset, int32_t length) const;
        std::vector<char> readFile(int64_t offset, int32_t length) const;
        int64_t writeFile(const std::vector<char>& buffer) const;
        int writeFile(int64_t offset, const std::vector<char>& buffer) const;

    public:

        Saf(const Saf&) = delete;
        Saf& operator=(const Saf&) = delete;
    };
}
