namespace Din91379Tests;

using Din91379;


public class TypeBTest
{
    [Theory]
    [MemberData(nameof(ValidGlyphsTestData))]
    public void TestAcceptsAllValidGlyphs(string glyph)
    {
        Assert.True(TypeB.IsValid(glyph));
    }

    public static IEnumerable<object[]> ValidGlyphsTestData()
    {
        foreach (string validGlyph in Glyphs.LatinLetters)
        {
            yield return new object[] { validGlyph };
        }

        foreach (string validGlyph in Glyphs.NonLettersN1)
        {
            yield return new object[] { validGlyph };
        }

        foreach (string validGlyph in Glyphs.NonLettersN2)
        {
            yield return new object[] { validGlyph };
        }
    }

    [Theory]
    [MemberData(nameof(DeprecatedGlyphsTestData))]
    public void TestTranslatesDeprecatedGlyphs(string glyph, string replacement)
    {
        TypeB value = TypeB.FromString(glyph);
        Assert.Equal(value, replacement);
    }

    public static IEnumerable<object[]> DeprecatedGlyphsTestData()
    {
        foreach (KeyValuePair<string, string> item in Glyphs.DeprecatedLatinLetters)
        {
            yield return new object[] { item.Key, item.Value };
        }
    }

    [Theory]
    [MemberData(nameof(InvalidGlyphsTestData))]
    public void TestRejectsInvalidGlyphs(string glyph)
    {
        Assert.False(TypeB.IsValid(glyph));
    }

    public static IEnumerable<object[]> InvalidGlyphsTestData()
    {
        foreach (string glyph in Data.GloballyInvalidStrings)
        {
            yield return new object[] { glyph };
        }

        foreach (string glyph in Glyphs.DeprecatedLatinLetters.Keys)
        {
            yield return new object[] { glyph };
        }

        foreach (string glyph in Glyphs.NonLettersN3)
        {
            yield return new object[] { glyph };
        }

        foreach (string glyph in Glyphs.NonLettersN4)
        {
            yield return new object[] { glyph };
        }

        foreach (string glyph in Glyphs.GreekLetters)
        {
            yield return new object[] { glyph };
        }

        foreach (string glyph in Glyphs.CyrillicLetters)
        {
            yield return new object[] { glyph };
        }

        foreach (string glyph in Glyphs.NonLettersE1)
        {
            yield return new object[] { glyph };
        }
    }
}