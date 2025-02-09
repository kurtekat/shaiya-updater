#include <fstream>
#include <string>
#include "include/SBinaryReader.h"
using namespace Updater::Data;

void SBinaryReader::close()
{
    if (!stream_)
        return;

    if (stream_->is_open())
        stream_->close();
}

void SBinaryReader::ignore(size_t count)
{
    stream_->ignore(count);
}

std::string SBinaryReader::readChars(size_t count)
{
    std::string buffer(count, 0);
    stream_->read(buffer.data(), buffer.size());
    return buffer;
}

std::string SBinaryReader::readString()
{
    uint32_t count{};
    stream_->read(reinterpret_cast<char*>(&count), 4);

    std::string buffer(count, 0);
    stream_->read(buffer.data(), buffer.size());
    return buffer;
}

int8_t SBinaryReader::readInt8()
{
    int8_t value{};
    stream_->read(reinterpret_cast<char*>(&value), 1);
    return value;
}

int16_t SBinaryReader::readInt16()
{
    int16_t value{};
    stream_->read(reinterpret_cast<char*>(&value), 2);
    return value;
}

int32_t SBinaryReader::readInt32()
{
    int32_t value{};
    stream_->read(reinterpret_cast<char*>(&value), 4);
    return value;
}

int64_t SBinaryReader::readInt64()
{
    int64_t value{};
    stream_->read(reinterpret_cast<char*>(&value), 8);
    return value;
}

uint8_t SBinaryReader::readUInt8()
{
    uint8_t value{};
    stream_->read(reinterpret_cast<char*>(&value), 1);
    return value;
}

uint16_t SBinaryReader::readUInt16()
{
    uint16_t value{};
    stream_->read(reinterpret_cast<char*>(&value), 2);
    return value;
}

uint32_t SBinaryReader::readUInt32()
{
    uint32_t value{};
    stream_->read(reinterpret_cast<char*>(&value), 4);
    return value;
}

uint64_t SBinaryReader::readUInt64()
{
    uint64_t value{};
    stream_->read(reinterpret_cast<char*>(&value), 8);
    return value;
}

float SBinaryReader::readSingle()
{
    float value{};
    stream_->read(reinterpret_cast<char*>(&value), 4);
    return value;
}

double SBinaryReader::readDouble()
{
    double value{};
    stream_->read(reinterpret_cast<char*>(&value), 8);
    return value;
}
