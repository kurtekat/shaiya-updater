#pragma once
#include <fstream>
#include <string>

namespace Updater::Data
{
    class SBinaryWriter final
    {
    public:

        /// <summary>
        /// Initializes a new instance of the SBinaryWriter class.
        /// </summary>
        /// <param name="stream"></param>
        SBinaryWriter(std::ofstream& stream)
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
        /// Writes a length-prefixed null-terminated string to the current stream. 
        /// This method first writes the length of the string as a 4-byte unsigned integer, 
        /// and then writes that many characters to the stream.
        /// </summary>
        /// <param name="str"></param>
        void write(const std::string& str);

        /// <summary>
        /// Writes a section of a character array to the current stream.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        void write(const std::string& buffer, size_t index, size_t count);

        /// <summary>
        /// Writes a 1-byte signed integer to the current stream.
        /// </summary>
        /// <param name="value"></param>
        void write(int8_t value);

        /// <summary>
        /// Writes a 2-byte signed integer to the current stream.
        /// </summary>
        /// <param name="value"></param>
        void write(int16_t value);

        /// <summary>
        /// Writes a 4-byte signed integer to the current stream.
        /// </summary>
        /// <param name="value"></param>
        void write(int32_t value);

        /// <summary>
        /// Writes an 8-byte signed integer to the current stream.
        /// </summary>
        /// <param name="value"></param>
        void write(int64_t value);

        /// <summary>
        /// Writes a 1-byte unsigned integer to the current stream.
        /// </summary>
        /// <param name="value"></param>
        void write(uint8_t value);

        /// <summary>
        /// Writes a 2-byte unsigned integer to the current stream.
        /// </summary>
        /// <param name="value"></param>
        void write(uint16_t value);

        /// <summary>
        /// Writes a 4-byte unsigned integer to the current stream.
        /// </summary>
        /// <param name="value"></param>
        void write(uint32_t value);

        /// <summary>
        /// Writes an 8-byte unsigned integer to the current stream.
        /// </summary>
        /// <param name="value"></param>
        void write(uint64_t value);

        /// <summary>
        /// Writes a 4-byte floating-point value to the current stream.
        /// </summary>
        /// <param name="value"></param>
        void write(float value);

        /// <summary>
        /// Writes an 8-byte floating-point value to the current stream.
        /// </summary>
        /// <param name="value"></param>
        void write(double value);

    private:

        std::ofstream* stream_;

    public:

        SBinaryWriter(const SBinaryWriter&) = delete;
        SBinaryWriter& operator=(const SBinaryWriter&) = delete;
    };
}
