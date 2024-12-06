using System.Drawing;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Foits;
public class ColorGroup(byte start,byte end,string name,byte extra1 = 0,byte extra2 = 0)
{
    [JsonPropertyName("start")]
    public byte Start {get;set;} = start;
    [JsonPropertyName("end")]
    public byte End {get;set;} = end;
    [JsonIgnore]
    public byte Length {get => (byte)(End - Start); }
    [JsonPropertyName("extra1")]
    public byte Extra1 {get;set;} = extra1;
    [JsonPropertyName("extra2")]
    public byte Extra2 {get;set;} = extra2;
    [JsonPropertyName("name")]
    public string Name {get;set;} = name;
    public ColorGroup(): this(0,0,string.Empty)
    {

    }
    public override string ToString()
    {
        return $"ColorGroup '{Name}' [{Start}:{End}] {{{Extra1}, {Extra2}}}";
    }
}
public static class ColorGroupExt
{
    public static ColorGroup ReadColorGroup(this Reader reader)
    {
        byte start = reader.ReadByte();
        byte end = reader.ReadByte();
        byte extra1 = reader.ReadByte();
        byte extra2 = reader.ReadByte();
        string name = reader.ReadPascal8String();
        return new(start,end,name,extra1,extra2);
    }
    public static List<ColorGroup> ReadColorGroup(this Reader reader,ulong count)
    {
        List<ColorGroup> groups = [];
        for(ulong i = 0;i < count;i++)
            groups.Add(ReadColorGroup(reader));
        return groups;
    }
    public static List<ColorGroup> ReadColorGroups(this Reader reader)
    {
        byte count = reader.ReadByte();
        return ReadColorGroup(reader,count);
    }
    public static void Write(this Writer writer,ColorGroup group)
    {
        writer.Write(group.Start);
        writer.Write(group.End);
        writer.Write(group.Extra1);
        writer.Write(group.Extra2);
        writer.WritePascal8String(group.Name);
    }
    public static void Write(this Writer writer,IEnumerable<ColorGroup> groups)
    {
        writer.Write((byte)groups.Count());
        foreach(ColorGroup group in groups)
            Write(writer,group);
    }
}