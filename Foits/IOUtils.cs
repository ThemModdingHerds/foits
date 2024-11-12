using ThemModdingHerds.IO;

namespace ThemModdingHerds.Foits;
public static class IOUtils
{
    public static string ReadByteString(IReader reader)
    {
        byte length = reader.ReadByte();
        return reader.ReadASCII(length);
    }
    public static string ReadShortString(IReader reader)
    {
        ushort length = reader.ReadUShort();
        return reader.ReadASCII(length);
    }
    public static void WriteByteString(IWriter writer,string value)
    {
        writer.Write((byte)value.Length);
        writer.WriteASCII(value);
    }
    public static void WriteByteStrings(IWriter writer,IEnumerable<string> strings)
    {
        foreach(string s in strings)
            WriteByteString(writer,s);
    }
    public static void WriteShortString(IWriter writer,string value)
    {
        writer.Write((ushort)value.Length);
        writer.WriteASCII(value);
    }
    public static void WriteShortStrings(IWriter writer,IEnumerable<string> strings)
    {
        foreach(string s in strings)
            WriteShortString(writer,s);
    }
}