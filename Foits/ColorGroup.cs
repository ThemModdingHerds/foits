using System.Drawing;
using System.Runtime.Serialization;
using System.Text;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Foits;
public class ColorGroup(byte start,byte end,string name,byte extra1,byte extra2) : ISerializable
{
    public byte Start {get;set;} = start;
    public byte End {get;set;} = end;
    public byte Length {get => (byte)(End - Start); }
    public byte Extra1 {get;set;} = extra1;
    public byte Extra2 {get;set;} = extra2;
    public string Name {get;set;} = name;

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("start",Start);
        info.AddValue("end",End);
        info.AddValue("extra1",Extra1);
        info.AddValue("extra2",Extra2);
        info.AddValue("nameLength",(byte)Name.Length);
        info.AddValue("name",Encoding.ASCII.GetBytes(Name));
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
        string name = IOUtils.ReadByteString(reader);
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
        IOUtils.WriteByteString(writer,group.Name);
    }
    public static void Write(this Writer writer,IEnumerable<ColorGroup> groups)
    {
        writer.Write((byte)groups.Count());
        foreach(ColorGroup group in groups)
            Write(writer,group);
    }
}