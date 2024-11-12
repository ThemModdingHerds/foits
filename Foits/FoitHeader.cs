using System.Runtime.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Foits;
public class FoitHeader(uint identifier,byte unknown1,ushort unknown2,PaletteFiles? files,string name,string sptName,byte[] unknown3,ushort paletteCount) : ISerializable
{
    public const uint IDENTIFIER = 0x0000002EU;
    public uint Identifier {get;set;} = identifier;
    public byte Unknown1 {get;set;} = unknown1;
    public bool HasPals {get => Files != null;}
    public ushort Unknown2 {get;set;} = unknown2;
    public PaletteFiles? Files {get;set;} = files;
    public string Name {get;set;} = name; 
    public string SptName {get;set;} = sptName;
    public const int UNKNOWN3_SIZE = 11;
    public byte[] Unknown3 {get;set;} = unknown3;
    public ushort PaletteCount {get;set;} = paletteCount;
    public FoitHeader(uint identifier,byte unknown1,ushort unknown2,string name,string sptName,byte[] unknown3): this(identifier,unknown1,unknown2,null,name,sptName,unknown3,1)
    {

    }
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        
    }
}
public static class FoitHeaderExt
{
    public static FoitHeader ReadFoitHeader(this Reader reader)
    {
        PaletteFiles? files = null;
        uint id = reader.ReadUInt();
        byte byte1 = reader.ReadByte();
        bool hasPals = reader.ReadByte() != 0;
        ushort short1 = reader.ReadUShort();
        byte byte2_length = reader.ReadByte();
        if(hasPals)
        {
            files = reader.ReadPaletteFiles(byte2_length);
        }
        string name = IOUtils.ReadByteString(reader);
        string spt = IOUtils.ReadShortString(reader);
        byte[] byte7_arr = reader.ReadBytes(FoitHeader.UNKNOWN3_SIZE);
        ushort palCount = reader.ReadUShort();
        return new(id,byte1,short1,files,name,spt,byte7_arr,palCount);
    }
    public static void Write(this Writer writer,FoitHeader header)
    {
        writer.Write(header.Identifier);
        writer.Write(header.Unknown1);
        writer.Write((byte)(header.HasPals ? 1 : 0));
        writer.Write(header.Unknown2);
        writer.Write((byte)(header.Files?.Unknown2.Length ?? 0));
        if(header.Files != null)
            writer.Write(header.Files);
        IOUtils.WriteByteString(writer,header.Name);
        IOUtils.WriteShortString(writer,header.SptName);
        writer.Write([..header.Unknown3.Take(FoitHeader.UNKNOWN3_SIZE)]);
        writer.Write(header.PaletteCount);
    }
}