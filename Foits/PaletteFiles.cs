using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Foits;
public class PaletteFiles(byte unknown1,byte[] unknown2,byte[] unknown3,IEnumerable<string> files)
{
    [JsonPropertyName("unknown1")]
    public byte Unknown1 {get;set;} = unknown1;
    [JsonPropertyName("unknown2")]
    public byte[] Unknown2 {get;set;} = unknown2;
    public const byte UNKNOWN3_SIZE = 12;
    [JsonPropertyName("unknown3")]
    public byte[] Unknown3 {get;set;} = unknown3;
    [JsonPropertyName("files")]
    public List<string> Files {get;set;} = [..files];
    public PaletteFiles(byte unknown1,byte[] unknown2,byte[] unknown3): this(unknown1,unknown2,unknown3,[])
    {

    }
    public PaletteFiles(): this(0,[],new byte[UNKNOWN3_SIZE])
    {

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
        List<string> palFiles = reader.ReadPascal8Strings(palFilesCount);
        return new(unknown1,unknown2,unknown3,palFiles);
    }
    public static void Write(this Writer writer,PaletteFiles files)
    {
        writer.Write(files.Unknown1);
        writer.Write(files.Unknown2);
        writer.Write([..files.Unknown3.Take(PaletteFiles.UNKNOWN3_SIZE)]);
        writer.Write((ushort)files.Files.Count);
        writer.WritePascal8Strings(files.Files);
    }
}