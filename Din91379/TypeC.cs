using System.Collections.Generic;

namespace Din91379
{
    public class TypeC : Din91379String
    {
        private static readonly HashSet<string> ValidGlyphs = Glyphs.CreateGlyphSet(new string[][] {
            Glyphs.LatinLetters,
            Glyphs.NonLettersN1,
            Glyphs.NonLettersN2,
            Glyphs.NonLettersN3,
            Glyphs.NonLettersN4,
        });

        private TypeC(string value) : base(value)
        {
        }

        public static TypeC FromString(string value)
        {
            value = _ConvertAndCheck(value, ValidGlyphs);
            return new TypeC(value);
        }

        public static bool IsValid(string value)
        {
            return _IsValid(value, ValidGlyphs);
        }

        public static string? GetFirstInvalidGlyph(string value)
        {
            return _GetFirstInvalidGlyph(value, ValidGlyphs);
        }
    }
}
