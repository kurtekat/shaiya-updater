#include <fstream>
#include <string>
#include "include/SBinaryWriter.h"
using namespace Updater::Data;

void SBinaryWriter::close()
{
    if (!stream_)
        return;

    if (stream_->is_open())
        stream_->close();
}

void SBinaryWriter::write(const std::string& str)
{
    auto count = static_cast<uint32_t>(str.size() + 1);
    stream_->write(reinterpret_cast<char*>(&count), 4);
    stream_->write(str.c_str(), count);
}

void SBinaryWriter::write(const std::string& buffer, size_t index, size_t count)
{
    if ((buffer.size() - index) < count)
        throw std::invalid_argument::exception();

    stream_->write(&buffer[index], count);
}

void SBinaryWriter::write(int8_t value)
{
    stream_->write(reinterpret_cast<char*>(&value), 1);
}

void SBinaryWriter::write(int16_t value)
{
    stream_->write(reinterpret_cast<char*>(&value), 2);
}

void SBinaryWriter::write(int32_t value)
{
    stream_->write(reinterpret_cast<char*>(&value), 4);
}

void SBinaryWriter::write(int64_t value)
{
    stream_->write(reinterpret_cast<char*>(&value), 8);
}

void SBinaryWriter::write(uint8_t value)
{
    stream_->write(reinterpret_cast<char*>(&value), 1);
}

void SBinaryWriter::write(uint16_t value)
{
    stream_->write(reinterpret_cast<char*>(&value), 2);
}

void SBinaryWriter::write(uint32_t value)
{
    stream_->write(reinterpret_cast<char*>(&value), 4);
}

void SBinaryWriter::write(uint64_t value)
{
    stream_->write(reinterpret_cast<char*>(&value), 8);
}

void SBinaryWriter::write(float value)
{
    stream_->write(reinterpret_cast<char*>(&value), 4);
}

void SBinaryWriter::write(double value)
{
    stream_->write(reinterpret_cast<char*>(&value), 8);
}
