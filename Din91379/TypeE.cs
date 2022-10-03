
namespace Din91379
{
    public class TypeE : Din91379String
    {
        private TypeE(string value) : base(value)
        {
        }

        public static TypeE FromString(string value)
        {
            AssertIsNormalized(value);

            string invalidGlyph = GetInvalidGlyph(value);
            if (invalidGlyph != null)
            {
                throw new InvalidGlyphException(value, invalidGlyph);
            }

            return new TypeE(value);
        }

        public static bool IsValid(string value)
        {
            try
            {
                TypeE.FromString(value);
                return true;
            }
            catch (InvalidGlyphException)
            {
                return false;
            }
        }

        private static string GetInvalidGlyph(string value)
        {
            foreach (string glyph in Glyphs.GetGlyphEnumerator(value))
            {
                if (Glyphs.LatinLetters.Contains(glyph))
                {
                    continue;
                }

                if (Glyphs.NonLettersN1.Contains(glyph))
                {
                    continue;
                }

                if (Glyphs.NonLettersN2.Contains(glyph))
                {
                    continue;
                }

                if (Glyphs.NonLettersN3.Contains(glyph))
                {
                    continue;
                }

                if (Glyphs.NonLettersN4.Contains(glyph))
                {
                    continue;
                }

                if (Glyphs.GreekLetters.Contains(glyph))
                {
                    continue;
                }

                if (Glyphs.CyrillicLetters.Contains(glyph))
                {
                    continue;
                }

                if (Glyphs.NonLettersE1.Contains(glyph))
                {
                    continue;
                }

                return glyph;
            }

            return null;
        }
    }
}
