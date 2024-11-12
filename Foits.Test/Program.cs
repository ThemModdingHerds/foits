using System.Drawing;
using System.Drawing.Imaging;
using ThemModdingHerds.Foits;
using ThemModdingHerds.IO.Binary;

string path = "baihe.foit";
Reader reader = new(path);
Foit foit = reader.ReadFoit();
Console.WriteLine();