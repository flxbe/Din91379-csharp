
namespace Din91379
{
    public class TypeC : Din91379String
    {
        private TypeC(string value) : base(value)
        {
        }

        public static TypeC FromString(string value)
        {
            AssertIsNormalized(value);

            string invalidGlyph = GetInvalidGlyph(value);
            if (invalidGlyph != null)
            {
                throw new InvalidGlyphException(value, invalidGlyph);
            }

            return new TypeC(value);
        }


        public static bool IsValid(string value)
        {
            try
            {
                TypeC.FromString(value);
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

                return glyph;
            }

            return null;
        }
    }
}
