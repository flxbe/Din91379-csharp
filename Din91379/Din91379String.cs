using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;


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

    public abstract class Din91379String : IComparable<Din91379String>, IComparable<string>,
        IEnumerable<char>, IEnumerable, IEquatable<string>, IEquatable<Din91379String>
    {
        readonly protected string value;

        protected Din91379String(string value)
        {
            this.value = value;
        }

        protected static string _ConvertAndCheck(string value, HashSet<string> validGlyphs)
        {
            value = value.Normalize();

            StringBuilder builder = new StringBuilder(value.Length);
            foreach (string glyph in Glyphs.GetGlyphEnumerator(value))
            {
                if (validGlyphs.Contains(glyph))
                {
                    builder.Append(glyph);
                }
                else if (Glyphs.DeprecatedLatinLetters.ContainsKey(glyph))
                {
                    // Translation of deprecated latin letters are valid for each data type,
                    // so this implicitly validates the translation.
                    // No extra test against the validGlyphs set is required.
                    builder.Append(Glyphs.DeprecatedLatinLetters[glyph]);
                }
                else
                {
                    throw new InvalidGlyphException(value, glyph);
                }
            }

            return builder.ToString();
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
            if (!value.IsNormalized())
            {
                return false;
            }

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

        public static bool operator ==(Din91379String? a, string? b)
        {
            if (a is null)
            {
                return b is null;
            }
            else
            {
                return a.value.Equals(b);
            }
        }

        public static bool operator !=(Din91379String? a, string? b) => !(a == b);

        public static bool operator ==(string? a, Din91379String? b) => b == a;
        public static bool operator !=(string? a, Din91379String? b) => b != a;

        public static bool operator ==(Din91379String? a, Din91379String? b)
        {
            if (a is null)
            {
                return b is null;
            }
            else
            {
                return a.Equals(b);
            }
        }

        public static bool operator !=(Din91379String? a, Din91379String? b) => !(a == b);

        public static string operator +(Din91379String left, string right)
        {
            return left.value + right;
        }

        public static string operator +(string left, Din91379String right)
        {
            return left + right.value;
        }

        public IEnumerator<char> GetEnumerator()
        {
            return this.value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }

        public static int GetHashCode(ReadOnlySpan<char> value, StringComparison comparisonType)
        {
            return String.GetHashCode(value, comparisonType);
        }

        public static int GetHashCode(ReadOnlySpan<char> value)
        {
            return String.GetHashCode(value);
        }

        public override bool Equals(object? other) => this.Equals(other as string);
        public bool Equals(string? other) => this.value == other;

        public bool Equals(Din91379String? other)
        {
            if (other is null)
            {
                return false;
            }

            return this.value.Equals(other.value);
        }

        public override string ToString() => this.value;
        public static implicit operator string(Din91379String d) => d.value;
    }
}