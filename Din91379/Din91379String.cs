using System;


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

        protected static void AssertIsNormalized(string value)
        {
            if (!System.StringNormalizationExtensions.IsNormalized(value))
            {
                throw new InvalidUnicodeNormalization(value);
            }
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