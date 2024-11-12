using System.Runtime.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Foits;
public class PaletteFiles(byte unknown1,byte[] unknown2,byte[] unknown3,IEnumerable<string> files) : ISerializable
{
    public byte Unknown1 {get;set;} = unknown1;
    public byte[] Unknown2 {get;set;} = unknown2;
    public const byte UNKNOWN3_SIZE = 12;
    public byte[] Unknown3 {get;set;} = unknown3;
    public List<string> Files {get;set;} = [..files];
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("unknown1",Unknown1);
        info.AddValue("unknown2",Unknown2);
        info.AddValue("unknown3",Unknown3);
    }
}
public static class PaletteFilesExt
{
    public static PaletteFiles ReadPaletteFiles(this Reader reader,byte unknown2_length)
    {
        byte unknown1 = reader.ReadByte();
        byte[] unknown2 = reader.ReadBytes(unknown2_length);
        byte[] unknown3 = reader.ReadBytes(PaletteFiles.UNKNOWN3_SIZE);
        ushort palFilesCount = reader.ReadUShort();
        List<string> palFiles = reader.ReadList(IOUtils.ReadByteString,palFilesCount);
        return new(unknown1,unknown2,unknown3,palFiles);
    }
    public static void Write(this Writer writer,PaletteFiles files)
    {
        writer.Write(files.Unknown1);
        writer.Write(files.Unknown2);
        writer.Write(files.Unknown3);
        writer.Write((ushort)files.Files.Count);
        IOUtils.WriteByteStrings(writer,files.Files);
    }
}