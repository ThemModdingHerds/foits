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
    u8 blue;
    u8 green;
    u8 red;
    u8 alpha;
};

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
    u32 int1;
    u8 byte1_length;
    if(int1 != 9)
    {
        u8 byte2;
        u8 byte1_arr[byte1_length];
        u8 byte3_arr[12];
        u16 palFilesCount;
        ByteString palFiles[palFilesCount];
    }
    ByteString name;
    ShortString spt;
    u16 short1;
    u32 int2;
    u32 int3;
    u8 byte4;
    u16 palCount;
};

Header header @ $;

Palette pals[header.palCount] @ $;

ColorGroups groups @ $;