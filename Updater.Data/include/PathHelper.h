#pragma once
#include <algorithm>
#include <cwctype>
#include <filesystem>
#include <string>
#include <vector>

namespace Updater::Data
{
    class PathHelper final
    {
    public:

        using Path = std::filesystem::path;

        /// <summary>
        /// A case-insensitive less than comparator.
        /// </summary>
        struct CompareIgnoreCaseLT
        {
            bool operator()(const Path& lhs, const Path& rhs) const
            {
                return icompare(lhs, rhs) < 0;
            }
        };

        /// <summary>
        /// Combines two paths into a path.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns>See std::filesystem::operator/ docs.</returns>
        static Path combine(const Path& lhs, const Path& rhs)
        {
            return lhs / rhs;
        }

        /// <summary>
        /// Compares two paths, ignoring case.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns>See std::string::compare docs.</returns>
        static int icompare(const Path& lhs, const Path& rhs)
        {
            auto a = lhs.wstring();
            std::transform(a.begin(), a.end(), a.begin(), [](wint_t c) {
                return std::towupper(c);
                });

            auto b = rhs.wstring();
            std::transform(b.begin(), b.end(), b.begin(), [](wint_t c) {
                return std::towupper(c);
                });

            return a.compare(b);
        }

        /// <summary>
        /// Splits a path into components based on directory separators.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>A vector containing the components delimited by directory separators.</returns>
        static std::vector<Path> split(const Path& path)
        {
            std::vector<Path> parts;
            for (const auto& part : path)
                parts.push_back(part);

            return parts;
        }
    };
}
