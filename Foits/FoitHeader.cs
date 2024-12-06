using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Foits;
public class FoitHeader(uint unknown1,PaletteFiles? files,string name,string sptName,ushort unknown2,uint unknown3,uint unknown4,byte unknown5,ushort paletteCount)
{
    public const uint IDENTIFIER = 0x0000002EU;
    [JsonPropertyName("unknown1")]
    public uint Unknown1 {get;set;} = unknown1;
    [JsonIgnore]
    public bool HasPals {get => Files != null;}
    [JsonPropertyName("files")]
    public PaletteFiles? Files {get;set;} = files;
    [JsonPropertyName("name")]
    public string Name {get;set;} = name;
    [JsonPropertyName("sptname")]
    public string SptName {get;set;} = sptName;
    [JsonPropertyName("unknown2")]
    public ushort Unknown2 {get;set;} = unknown2;
    [JsonPropertyName("unknown3")]
    public uint Unknown3 {get;set;} = unknown3;
    [JsonPropertyName("unknown4")]
    public uint Unknown4 {get;set;} = unknown4;
    [JsonPropertyName("unknown5")]
    public byte Unknown5 {get;set;} = unknown5;
    [JsonPropertyName("paletteCount")]
    public ushort PaletteCount {get;set;} = paletteCount;
    public FoitHeader(uint unknown1,string name,string sptName,ushort unknown2,uint unknown3,uint unknown4,byte unknown5): this(unknown1,null,name,sptName,unknown2,unknown3,unknown4,unknown5,1)
    {

    }
    public FoitHeader(): this(0,string.Empty,string.Empty,0,0,0,0)
    {

    }
}
public static class FoitHeaderExt
{
    public static FoitHeader ReadFoitHeader(this Reader reader)
    {
        PaletteFiles? files = null;
        uint id = reader.ReadUInt();
        if(id != FoitHeader.IDENTIFIER)
            throw new Exception("not a foit header");
        uint int1 = reader.ReadUInt();
        byte byte1 = reader.ReadByte();
        if(int1 != 9)
        {
            files = reader.ReadPaletteFiles(byte1);
        }
        string name = reader.ReadPascal8String();
        string spt = reader.ReadPascal16String();
        ushort short1 = reader.ReadUShort();
        uint int2 = reader.ReadUInt();
        uint int3 = reader.ReadUInt();
        byte byte3 = reader.ReadByte();
        ushort palCount = reader.ReadUShort();
        return new(int1,files,name,spt,short1,int2,int3,byte3,palCount);
    }
    public static void Write(this Writer writer,FoitHeader header)
    {
        writer.Write(FoitHeader.IDENTIFIER);
        writer.Write(header.Unknown1);
        writer.Write((byte)(header.Files?.Unknown2.Length ?? 0));
        if(header.Files != null)
            writer.Write(header.Files);
        writer.WritePascal8String(header.Name);
        writer.WritePascal16String(header.SptName);
        writer.Write(header.Unknown2);
        writer.Write(header.Unknown3);
        writer.Write(header.Unknown4);
        writer.Write(header.Unknown5);
        writer.Write(header.PaletteCount);
    }
}