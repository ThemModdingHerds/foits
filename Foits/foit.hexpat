#include <std/io>

struct ByteString
{
    u8 length;
    char value[length];
};

struct ShortString
{
    u16 length;
    char value[length];
};

struct BGRA
{
    u8 blue  [[color("0000FF")]];
    u8 green [[color("00FF00")]];
    u8 red [[color("FF0000")]];
    u8 alpha;
} [[static, color(std::format("{:02X}{:02X}{:02X}", red, green, blue))]];

struct Palette
{
    u32 header;
    u16 count;
    BGRA colors[count];
    u8 footer[10];
};

struct ColorGroup
{
    u8 start;
    u8 end;
    u8 extra1;
    u8 extra2;
    ByteString name;
};

struct ColorGroups
{
    u8 count;
    ColorGroup groups[count];
};

struct Header
{
    u32 id;
    u8 byte1;
    bool hasPals;
    u16 short1;
    u8 byte2_length;
    if(hasPals)
    {
        u8 byte3;
        u8 byte2_arr[byte2_length];
        u8 byte4_arr[12];
        u16 palFilesCount;
        ByteString palFiles[palFilesCount];
    }
    ByteString name;
    ShortString spt;
    u8 byte7_arr[11];
    u16 palCount;
};

Header header @ $;

Palette pals[header.palCount] @ $;

ColorGroups groups @ $;