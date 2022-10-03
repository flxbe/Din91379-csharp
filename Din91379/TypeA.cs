
namespace Din91379
{
    public class TypeA : Din91379String
    {
        private TypeA(string value) : base(value)
        {
        }

        public static TypeA FromString(string value)
        {
            AssertIsNormalized(value);

            string? invalidGlyph = GetInvalidGlyph(value);
            if (invalidGlyph != null)
            {
                throw new InvalidGlyphException(value, invalidGlyph);
            }

            return new TypeA(value);
        }


        public static bool IsValid(string value)
        {
            try
            {
                TypeA.FromString(value);
                return true;
            }
            catch (InvalidGlyphException)
            {
                return false;
            }
        }

        private static string? GetInvalidGlyph(string value)
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

                return glyph;
            }

            return null;
        }

        public static TypeA operator +(TypeA left, TypeA right)
        {
            return new TypeA(left.value + right.value);
        }
    }
}
