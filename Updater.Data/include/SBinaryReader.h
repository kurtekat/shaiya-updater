#pragma once
#include <fstream>
#include <string>

namespace Updater::Data
{
    class SBinaryReader final
    {
    public:

        /// <summary>
        /// Initializes a new instance of the SBinaryReader class.
        /// </summary>
        /// <param name="stream"></param>
        SBinaryReader(std::ifstream& stream)
            : stream_(nullptr)
        {
            if (!stream)
                throw std::invalid_argument::exception();

            stream_ = &stream;
        }

        /// <summary>
        /// Closes the underlying stream.
        /// </summary>
        void close();

        /// <summary>
        /// Reads the specified number of characters from the current stream and discards them.
        /// </summary>
        /// <param name="count"></param>
        void ignore(size_t count);

        /// <summary>
        /// Reads the specified number of characters from the current stream.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        std::string readChars(size_t count);

        /// <summary>
        /// Reads a length-prefixed string from the current stream. 
        /// This method first reads the length of the string as a 4-byte unsigned integer, 
        /// and then reads that many characters from the stream.
        /// </summary>
        /// <returns></returns>
        std::string readString();

        /// <summary>
        /// Reads a 1-byte signed integer from the current stream.
        /// </summary>
        /// <returns></returns>
        int8_t readInt8();

        /// <summary>
        /// Reads a 2-byte signed integer from the current stream.
        /// </summary>
        /// <returns></returns>
        int16_t readInt16();

        /// <summary>
        /// Reads a 4-byte signed integer from the current stream.
        /// </summary>
        /// <returns></returns>
        int32_t readInt32();

        /// <summary>
        /// Reads an 8-byte signed integer from the current stream.
        /// </summary>
        /// <returns></returns>
        int64_t readInt64();

        /// <summary>
        /// Reads a 1-byte unsigned integer from the current stream.
        /// </summary>
        /// <returns></returns>
        uint8_t readUInt8();

        /// <summary>
        /// Reads a 2-byte unsigned integer from the current stream.
        /// </summary>
        /// <returns></returns>
        uint16_t readUInt16();

        /// <summary>
        /// Reads a 4-byte unsigned integer from the current stream.
        /// </summary>
        /// <returns></returns>
        uint32_t readUInt32();

        /// <summary>
        /// Reads an 8-byte unsigned integer from the current stream.
        /// </summary>
        /// <returns></returns>
        uint64_t readUInt64();

        /// <summary>
        /// Reads a 4-byte floating point value from the current stream.
        /// </summary>
        /// <returns></returns>
        float readSingle();

        /// <summary>
        /// Reads an 8-byte floating point value from the current stream.
        /// </summary>
        /// <returns></returns>
        double readDouble();

    private:

        std::ifstream* stream_;

    public:

        SBinaryReader(const SBinaryReader&) = delete;
        SBinaryReader& operator=(const SBinaryReader&) = delete;
    };
}
