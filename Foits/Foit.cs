using System.Runtime.Serialization;
using ThemModdingHerds.IO.Binary;

namespace ThemModdingHerds.Foits;
public class Foit(FoitHeader header,IEnumerable<Palette> palettes,IEnumerable<ColorGroup> colorGroups) : ISerializable
{
    public FoitHeader Header {get;set;} = header;
    public List<Palette> Palettes {get;set;} = [..palettes];
    public Palette OriginalPalette {get => Palettes[0];}
    public List<ColorGroup> ColorGroups {get;set;} = [..colorGroups];
    public Foit(FoitHeader header): this(header,[],[])
    {
        
    }
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        throw new NotImplementedException();
    }
}
public static class FoitExt
{
    public static Foit ReadFoit(this Reader reader)
    {
        FoitHeader header = reader.ReadFoitHeader();
        List<Palette> palettes = reader.ReadPalette(header.PaletteCount);
        List<ColorGroup> colorGroups = reader.ReadColorGroups();
        return new(header,palettes,colorGroups);
    }
}