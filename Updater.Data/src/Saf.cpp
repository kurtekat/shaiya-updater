#include <fstream>
#include <vector>
#include "include/Saf.h"
using namespace Updater::Data;

int Saf::eraseFile(int64_t offset, int32_t length) const
{
    try
    {
        std::fstream saf;
        saf.exceptions(std::ios::badbit | std::ios::failbit);

        saf.open(path, std::ios::binary | std::ios::in | std::ios::out);
        saf.seekp(offset);

        std::vector<char> buffer(length);
        saf.write(buffer.data(), buffer.size());
        saf.close();
        return 0;
    }
    catch (std::exception)
    {
        return 1;
    }
}

int Saf::readFile(int64_t offset, std::vector<char>& output) const
{
    try
    {
        std::ifstream saf;
        saf.exceptions(std::ios::badbit | std::ios::eofbit | std::ios::failbit);

        saf.open(path, std::ios::binary);
        saf.seekg(offset);
        saf.read(output.data(), output.size());
        saf.close();
        return 0;
    }
    catch (std::exception)
    {
        return 1;
    }
}

int64_t Saf::writeFile(const std::vector<char>& buffer) const
{
    try
    {
        std::fstream saf;
        saf.exceptions(std::ios::badbit | std::ios::failbit);
        
        saf.open(path, std::ios::binary | std::ios::app | std::ios::ate);
        auto before = saf.tellp();
        saf.write(buffer.data(), buffer.size());
        saf.close();
        return before;
    }
    catch (std::exception)
    {
        return -1;
    }
}

int Saf::writeFile(int64_t offset, const std::vector<char>& buffer) const
{
    try
    {
        std::fstream saf;
        saf.exceptions(std::ios::badbit | std::ios::failbit);

        saf.open(path, std::ios::binary | std::ios::in | std::ios::out);
        saf.seekp(offset);
        saf.write(buffer.data(), buffer.size());
        saf.close();
        return 0;
    }
    catch (std::exception)
    {
        return 1;
    }
}
