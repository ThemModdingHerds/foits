using System.Drawing;
using System.Runtime.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Foits;
public class Palette(uint header,IEnumerable<Color> colors,byte[] footer)
{
    public uint Header {get;set;} = header;
    public List<Color> Colors {get;set;} = [..colors];
    public const int FOOTER_SIZE = 10;
    public byte[] Footer {get;set;} = footer;
    public Palette(uint header,byte[] footer): this(header,[],footer)
    {

    }
    public Palette(): this(0,new byte[10])
    {

    }
    public override string ToString()
    {
        return $"Palette [Colors={Colors.Count}]";
    }
    public List<Color> GetColors(ColorGroup group)
    {
        return Colors.Slice(group.Start,group.Length);
    }
    public void SetColors(ColorGroup group,IEnumerable<Color> colors)
    {
        List<Color> values = [..colors];
        if(group.Length != values.Count)
            throw new Exception($"group requires {group.Length} elements, got {values.Count} instead");
        for(int i = group.Start;i <= group.End;i++)
        {
            Colors[i] = values[i];
        }
    }
}
public static class PaletteExt
{
    public static Palette ReadPalette(this Reader reader)
    {
        uint header = reader.ReadUInt();
        ushort count = reader.ReadUShort();
        List<Color> colors = reader.ReadList((r) => r.ReadBGRA(),count);
        byte[] footer = reader.ReadBytes(Palette.FOOTER_SIZE);
        return new(header,colors,footer);
    }
    public static List<Palette> ReadPalette(this Reader reader,ulong count)
    {
        List<Palette> palettes = [];
        for(ulong i = 0;i < count;i++)
            palettes.Add(ReadPalette(reader));
        return palettes;
    }
    public static void Write(this Writer writer,Palette palette)
    {
        writer.Write(palette.Header);
        writer.Write((ushort)palette.Colors.Count);
        writer.Write(palette.Colors,(w,c) => w.WriteBGRA(c));
        writer.Write([..palette.Footer.Take(Palette.FOOTER_SIZE)]);
    }
    public static void Write(this Writer writer,IEnumerable<Palette> palettes)
    {
        foreach(Palette palette in palettes)
            Write(writer,palette);
    }
}