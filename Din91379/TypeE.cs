using System.Collections.Generic;

namespace Din91379
{
    public sealed class TypeE : Din91379String
    {
        private static readonly HashSet<string> ValidGlyphs = Glyphs.CreateGlyphSet(new IEnumerable<string>[] {
            Glyphs.LatinLetters.Keys,
            Glyphs.NonLettersN1,
            Glyphs.NonLettersN2,
            Glyphs.NonLettersN3,
            Glyphs.NonLettersN4,
            Glyphs.GreekLetters,
            Glyphs.CyrillicLetters,
            Glyphs.NonLettersE1,
        });

        private TypeE(string value) : base(value)
        {
        }

        public static TypeE FromString(string value)
        {
            value = _ConvertAndCheck(value, ValidGlyphs);
            return new TypeE(value);
        }

        public static bool IsValid(string value)
        {
            return _IsValid(value, ValidGlyphs);
        }

        public static string? GetFirstInvalidGlyph(string value)
        {
            return _GetFirstInvalidGlyph(value, ValidGlyphs);
        }

        public static TypeE operator +(TypeE left, TypeE right)
        {
            return new TypeE(left.value + right.value);
        }
    }
}
