using System.Collections.Generic;

namespace Din91379
{
    public sealed class TypeC : Din91379String
    {
        private static readonly HashSet<string> ValidGlyphs = Glyphs.CreateGlyphSet(new IEnumerable<string>[] {
            Glyphs.LatinLetters.Keys,
            Glyphs.NonLettersN1,
            Glyphs.NonLettersN2,
            Glyphs.NonLettersN3,
            Glyphs.NonLettersN4,
        });

        private TypeC(string value) : base(value)
        {
        }

        public string GetSearchForm()
        {
            return _GetSearchForm();
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

        public static TypeC operator +(TypeC left, TypeC right)
        {
            return new TypeC(left.value + right.value);
        }
    }
}
