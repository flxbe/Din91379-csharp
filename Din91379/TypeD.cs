using System.Collections.Generic;

namespace Din91379
{
    public class TypeD : Din91379String
    {
        private static readonly HashSet<string> ValidGlyphs = Glyphs.CreateGlyphSet(new string[][] {
            Glyphs.LatinLetters,
            Glyphs.NonLettersN1,
            Glyphs.NonLettersN2,
            Glyphs.NonLettersN3,
            Glyphs.GreekLetters,
            Glyphs.NonLettersE1,
        });

        private TypeD(string value) : base(value)
        {
        }

        public static TypeD FromString(string value)
        {
            value = _ConvertAndCheck(value, ValidGlyphs);
            return new TypeD(value);
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
