using System.Runtime.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Foits;
public class Color(byte blue,byte green,byte red,byte alpha) : ISerializable
{
    public byte Blue {get;set;} = blue;
    public byte Green {get;set;} = green;
    public byte Red {get;set;} = red;
    public byte Alpha {get;set;} = alpha;
    public static Color FromRGB(byte red,byte green,byte blue,byte alpha) => new(blue,green,red,alpha);
    public static Color FromRGB(byte red,byte green,byte blue) => FromRGB(red,green,blue,0xff);
    public Color(byte[] bytes): this(bytes[2],bytes[1],bytes[0],bytes[3])
    {

    }
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("alpha",Alpha);
        info.AddValue("red",Red);
        info.AddValue("green",Green);
        info.AddValue("blue",Blue);
    }
    public static implicit operator System.Drawing.Color(Color color)
    {
        return System.Drawing.Color.FromArgb(color.Alpha,color.Red,color.Green,color.Blue);
    }
    public byte[] ToBytes()
    {
        return [
            Alpha,
            Red,
            Green,
            Blue
        ];
    }
    public override string ToString()
    {
        return $"Color [B={Blue}, G={Green}, R={Red}, A={Alpha}]";
    }
}
public static class ColorExt
{
    public static Color ReadBGRA(this Reader reader)
    {
        return new Color(reader.ReadBytes(4));
    }
    public static List<Color> ReadBGRA(this Reader reader,ulong count)
    {
        List<Color> colors = [];
        for(ulong i = 0;i < count;i++)
            colors.Add(ReadBGRA(reader));
        return colors;
    }
    public static void Write(this Writer writer,Color color)
    {
        writer.Write(color.ToBytes());
    }
    public static void Write(this Writer writer,IEnumerable<Color> colors)
    {
        foreach(Color color in colors)
            Write(writer,color);
    }
}