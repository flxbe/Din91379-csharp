using System;
using System.Collections.Generic;


namespace Din91379
{
    public abstract class Din91379Exception : System.Exception
    {
        public Din91379Exception(string message) : base(message) { }

    }

    public class InvalidGlyphException : Din91379Exception
    {
        readonly string value;
        readonly string invalidGlyph;

        public InvalidGlyphException(string value, string invalidGlyph) : base(String.Format("Found invalid glyph '{0}' in '{1}'", invalidGlyph, value))
        {
            this.value = value;
            this.invalidGlyph = invalidGlyph;
        }

    }

    public class InvalidUnicodeNormalization : Din91379Exception
    {
        readonly string value;

        public InvalidUnicodeNormalization(string value) : base(String.Format("The string is not Unicode NFC normalized: '{0}'", value))
        {
            this.value = value;
        }

    }

    public abstract class Din91379String : IComparable<Din91379String>, IComparable<string>
    {
        readonly protected string value;

        protected Din91379String(string value)
        {
            this.value = value;
        }

        protected static string _ConvertAndCheck(string value, HashSet<string> validGlyphs)
        {
            if (!System.StringNormalizationExtensions.IsNormalized(value))
            {
                throw new InvalidUnicodeNormalization(value);
            }

            string? invalidGlyph = _GetFirstInvalidGlyph(value, validGlyphs);
            if (invalidGlyph != null)
            {
                throw new InvalidGlyphException(value, invalidGlyph);
            }

            // TODO: translate deprecated glyphs

            return value;
        }

        protected static string? _GetFirstInvalidGlyph(string value, HashSet<string> validGlyphs)
        {
            foreach (string glyph in Glyphs.GetGlyphEnumerator(value))
            {
                if (!validGlyphs.Contains(glyph))
                {
                    return glyph;
                }
            }

            return null;
        }

        protected static bool _IsValid(string value, HashSet<string> validGlyphs)
        {
            return _GetFirstInvalidGlyph(value, validGlyphs) == null;
        }


        public int CompareTo(Din91379String? other)
        {
            if (other is null)
            {
                return this.value.CompareTo(null);
            }
            else
            {
                return this.value.CompareTo(other.value);
            }
        }
        public int CompareTo(string? other)
        {
            return this.value.CompareTo(other);
        }

        public static string operator +(Din91379String left, string right)
        {
            return left.value + right;
        }

        public static string operator +(string left, Din91379String right)
        {
            return left + right.value;
        }
    }
}