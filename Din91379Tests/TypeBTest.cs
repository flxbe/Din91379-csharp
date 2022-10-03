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
    }
}