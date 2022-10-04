namespace Din91379Tests;

using Din91379;


public class TypeATest
{
    [Theory]
    [MemberData(nameof(ValidGlyphsTestData))]
    public void TestAcceptsAllValidGlyphs(string glyph)
    {
        Assert.True(TypeA.IsValid(glyph));

        Assert.Equal(glyph, TypeA.FromString(glyph));
    }

    public static IEnumerable<object[]> ValidGlyphsTestData()
    {
        yield return new object[] { "" };

        foreach (string validGlyph in Glyphs.LatinLetters)
        {
            yield return new object[] { validGlyph };
        }

        foreach (string validGlyph in Glyphs.NonLettersN1)
        {
            yield return new object[] { validGlyph };
        }
    }

    [Theory]
    [MemberData(nameof(NotConvertibleTestData))]
    public void TestRejectsNotConvertibleGlyphs(string glyph)
    {
        Assert.Throws<InvalidGlyphException>(() => TypeA.FromString(glyph));
    }

    public static IEnumerable<object[]> NotConvertibleTestData()
    {
        foreach (string glyph in Data.GloballyInvalidStrings)
        {
            yield return new object[] { glyph };
        }

        foreach (string glyph in Glyphs.NonLettersN2)
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

    [Theory]
    [MemberData(nameof(InvalidGlyphsTestData))]
    public void TestIdentifiesInvalidGlyphs(string glyph)
    {
        Assert.False(TypeA.IsValid(glyph));
    }

    public static IEnumerable<object[]> InvalidGlyphsTestData()
    {
        foreach (object[] data in NotConvertibleTestData())
        {
            yield return data;
        }

        foreach (string glyph in Glyphs.DeprecatedLatinLetters.Keys)
        {
            yield return new object[] { glyph };
        }
    }
}