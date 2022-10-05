using System.Collections.Generic;

namespace Din91379
{
    public sealed class TypeB : Din91379String
    {
        private static readonly HashSet<string> ValidGlyphs = Glyphs.CreateGlyphSet(new IEnumerable<string>[] {
            Glyphs.LatinLetters.Keys,
            Glyphs.NonLettersN1,
            Glyphs.NonLettersN2,
        });

        private TypeB(string value) : base(value)
        {
        }

        public string GetSearchForm()
        {
            return _GetSearchForm();
        }

        public static TypeB FromString(string value)
        {
            value = _ConvertAndCheck(value, ValidGlyphs);
            return new TypeB(value);
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
