
namespace Din91379Tests;


class TestData
{
    public static readonly LatinChars Groups = LatinChars.Load();

    public static readonly string[] GloballyInvalidStrings = {
        "K͟",
        "K͟u",
        "k͟",
        "k͟u",
    };

    public static readonly string[] NewGlyphs = {
        "ē̍",
        "ō̍",
        "ḗ",
        char.ConvertFromUtf32(0x2032), // PRIME
        char.ConvertFromUtf32(0x2033), // DOUBLE PRIME
    };

    //public static readonly Dictionary<string, string> DeprecatedLatinLetters = new Dictionary<string, string> {
    //{"ē̍", "ī́"},
    //{"Ŀ", "L·"},
    //{"ŀ", "l·"},
    //{"ŉ", "'n"},
    //{"ō̍", "ṓ"},
    //{"ḗ", "ī́"},
    //};
}