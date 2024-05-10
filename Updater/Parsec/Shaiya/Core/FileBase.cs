#pragma warning disable 8600, 8602
using System.Text;
using Parsec.Common;
using Parsec.Extensions;
using System.IO;
using Parsec.Helpers;
using Parsec.Serialization;
using Parsec.Shaiya.Data;

namespace Parsec.Shaiya.Core;

public abstract class FileBase
{
    /// <summary>
    /// Full path to the file
    /// </summary>
    public string Path { get; set; } = "";

    public abstract string Extension { get; }

    public Episode Episode { get; set; } = Episode.Unknown;

    public Encoding Encoding { get; set; } = Encoding.ASCII;

    /// <summary>
    /// Plain file name
    /// </summary>
    public string FileName => System.IO.Path.GetFileName(Path);

    /// <summary>
    /// File name without the extension (.xx)
    /// </summary>
    public string FileNameWithoutExtension => System.IO.Path.GetFileNameWithoutExtension(Path);

    protected abstract void Read(SBinaryReader binaryReader);

    protected abstract void Write(SBinaryWriter binaryWriter);

    public void Write(string path)
    {
        var serializationOptions = new BinarySerializationOptions(Episode, Encoding);
        using var binaryWriter = new SBinaryWriter(path, serializationOptions);
        Write(binaryWriter);
    }

    public void Write(string path, Episode episode, Encoding? encoding = null)
    {
        encoding ??= Encoding.ASCII;

        var serializationOptions = new BinarySerializationOptions(episode, encoding);
        using var binaryWriter = new SBinaryWriter(path, serializationOptions);
        Write(binaryWriter);
    }

    /// <summary>
    /// Reads the shaiya file format from a file
    /// </summary>
    /// <param name="path">File path</param>
    /// <param name="serializationOptions">Serialization options</param>
    /// <typeparam name="T">Shaiya File Format Type</typeparam>
    /// <returns>T instance</returns>
    internal static T ReadFromFile<T>(string path, BinarySerializationOptions serializationOptions) where T : FileBase, new()
    {
        var instance = new T { Path = path, Episode = serializationOptions.Episode, Encoding = serializationOptions.Encoding };
        using var binaryReader = new SBinaryReader(path, serializationOptions);

        if (instance is IEncryptable encryptableInstance)
        {
            encryptableInstance.DecryptBuffer(binaryReader);
        }

        instance.Read(binaryReader);
        return instance;
    }

    /// <summary>
    /// Reads the shaiya file format from a file
    /// </summary>
    /// <param name="path">File path</param>
    /// <param name="type">FileBase child type to be read</param>
    /// <param name="serializationOptions">Serialization options</param>
    /// <returns>FileBase instance</returns>
    internal static FileBase ReadFromFile(string path, Type type, BinarySerializationOptions serializationOptions)
    {
        if (!type.GetBaseClassesAndInterfaces().Contains(typeof(FileBase)))
            throw new ArgumentException("Type must be a child of FileBase");

        var instance = (FileBase)Activator.CreateInstance(type);
        instance.Path = path;
        instance.Episode = serializationOptions.Episode;
        instance.Encoding = serializationOptions.Encoding;

        using var binaryReader = new SBinaryReader(path, serializationOptions);

        if (instance is IEncryptable encryptableInstance)
        {
            encryptableInstance.DecryptBuffer(binaryReader);
        }

        instance.Read(binaryReader);
        return instance;
    }

    /// <summary>
    /// Reads the shaiya file format from a buffer (byte array)
    /// </summary>
    /// <param name="name">File name</param>
    /// <param name="buffer">File buffer</param>
    /// <param name="serializationOptions">Serialization options</param>
    /// <typeparam name="T">Shaiya File Format Type</typeparam>
    /// <returns>T instance</returns>
    internal static T ReadFromBuffer<T>(string name, byte[] buffer, BinarySerializationOptions serializationOptions) where T : FileBase, new()
    {
        var instance = new T { Path = name, Episode = serializationOptions.Episode, Encoding = serializationOptions.Encoding };
        using var binaryReader = new SBinaryReader(buffer, serializationOptions);

        if (instance is IEncryptable encryptableInstance)
        {
            encryptableInstance.DecryptBuffer(binaryReader);
        }

        instance.Read(binaryReader);
        return instance;
    }

    /// <summary>
    /// Reads the shaiya file format from a buffer (byte array)
    /// </summary>
    /// <param name="name">File name</param>
    /// <param name="buffer">File buffer</param>
    /// <param name="type">FileBase child type to be read</param>
    /// <param name="serializationOptions">Serialization options</param>
    /// <returns>FileBase instance</returns>
    internal static FileBase ReadFromBuffer(string name, byte[] buffer, Type type, BinarySerializationOptions serializationOptions)
    {
        if (!type.GetBaseClassesAndInterfaces().Contains(typeof(FileBase)))
            throw new ArgumentException("Type must be a child of FileBase");

        var instance = (FileBase)Activator.CreateInstance(type);
        instance.Path = name;
        instance.Episode = serializationOptions.Episode;
        instance.Encoding = serializationOptions.Encoding;

        using var binaryReader = new SBinaryReader(buffer, serializationOptions);

        if (instance is IEncryptable encryptableInstance)
        {
            encryptableInstance.DecryptBuffer(binaryReader);
        }

        instance.Read(binaryReader);
        return instance;
    }

    /// <summary>
    /// Reads the shaiya file format from a buffer (byte array) within a <see cref="Data"/> instance
    /// </summary>
    /// <param name="data"><see cref="Data"/> instance</param>
    /// <param name="file"><see cref="SFile"/> instance</param>
    /// <param name="serializationOptions">Serialization options</param>
    /// <returns>FileBase instance</returns>
    internal static T ReadFromData<T>(Data.Data data, SFile file, BinarySerializationOptions serializationOptions) where T : FileBase, new()
    {
        return ReadFromBuffer<T>(file.Name, data.GetFileBuffer(file), serializationOptions);
    }

    /// <summary>
    /// Reads the shaiya file format from a buffer (byte array) within a <see cref="Data"/> instance
    /// </summary>
    /// <param name="data"><see cref="Data"/> instance</param>
    /// <param name="file"><see cref="SFile"/> instance</param>
    /// <param name="type">FileBase child type to be read</param>
    /// <param name="serializationOptions"></param>
    /// <returns>FileBase instance</returns>
    internal static FileBase ReadFromData(Data.Data data, SFile file, Type type, BinarySerializationOptions serializationOptions)
    {
        if (!data.FileIndex.ContainsValue(file))
            throw new FileNotFoundException("The provided SFile instance is not part of the Data");

        return ReadFromBuffer(file.Name, data.GetFileBuffer(file), type, serializationOptions);
    }

    public IEnumerable<byte> GetBytes()
    {
        var serializationOptions = BinarySerializationOptions.Default;
        return GetBytes(serializationOptions);
    }

    public IEnumerable<byte> GetBytes(Episode episode, Encoding? encoding = null)
    {
        var serializationOptions = new BinarySerializationOptions(episode, encoding);
        return GetBytes(serializationOptions);
    }

    public IEnumerable<byte> GetBytes(BinarySerializationOptions serializationOptions)
    {
        using var memoryStream = new MemoryStream();
        using var binaryWriter = new SBinaryWriter(memoryStream, serializationOptions);
        Write(binaryWriter);
        return memoryStream.ToArray();
    }
}
