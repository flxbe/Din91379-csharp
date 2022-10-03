
namespace Din91379
{
    public class TypeB : Din91379String
    {
        private TypeB(string value) : base(value)
        {
        }

        public static TypeB FromString(string value)
        {
            AssertIsNormalized(value);

            string invalidGlyph = GetInvalidGlyph(value);
            if (invalidGlyph != null)
            {
                throw new InvalidGlyphException(value, invalidGlyph);
            }

            return new TypeB(value);
        }

        public static bool IsValid(string value)
        {
            try
            {
                TypeB.FromString(value);
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

                return glyph;
            }

            return null;
        }
    }
}
