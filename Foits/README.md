# Them's Fightin' Herds Foits reader/writer

A library for reading/writing `.foit` files form Them's Fightin' Herds

## Usage

Reading

```c#
using ThemModdingHerds.Foits;
using ThemModdingHerds.IO.Binary;

Reader reader = new(pathToFoitFile);
Foit foit = reader.ReadFoit();
```

## Credits

- petko021tv and [TFH-Palette-Editor](https://codeberg.org/TFH_Modding/TFH-Palette-Editor)
