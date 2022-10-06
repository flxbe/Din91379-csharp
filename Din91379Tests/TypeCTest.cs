namespace Din91379Tests;

using Din91379;


public class TypeCTest
{
    private static readonly LatinChars LatinCharsData = LatinChars.Load();

    [Theory]
    [MemberData(nameof(ValidGlyphsTestData))]
    public void TestAcceptsAllValidGlyphs(string glyph)
    {
        Assert.True(TypeC.IsValid(glyph));

        Assert.Equal(glyph, TypeC.FromString(glyph));
    }

    public static IEnumerable<object[]> ValidGlyphsTestData()
    {
        yield return new object[] { "" };

        foreach (string validGlyph in LatinCharsData.LatinLetters.Keys)
        {
            yield return new object[] { validGlyph };
        }

        foreach (string validGlyph in LatinCharsData.NonLettersN1.Keys)
        {
            yield return new object[] { validGlyph };
        }

        foreach (string validGlyph in LatinCharsData.NonLettersN2.Keys)
        {
            yield return new object[] { validGlyph };
        }

        foreach (string glyph in LatinCharsData.NonLettersN3.Keys)
        {
            yield return new object[] { glyph };
        }

        foreach (string glyph in LatinCharsData.NonLettersN4.Keys)
        {
            yield return new object[] { glyph };
        }
    }

    [Theory]
    [MemberData(nameof(NotConvertibleTestData))]
    public void TestRejectsNotConvertibleGlyphs(string glyph)
    {
        Assert.Throws<InvalidGlyphException>(() => TypeC.FromString(glyph));
    }

    public static IEnumerable<object[]> NotConvertibleTestData()
    {
        foreach (string glyph in TestData.GloballyInvalidStrings)
        {
            yield return new object[] { glyph };
        }

        foreach (string glyph in LatinCharsData.GreekLetters)
        {
            yield return new object[] { glyph };
        }

        foreach (string glyph in LatinCharsData.CyrillicLetters)
        {
            yield return new object[] { glyph };
        }

        foreach (string glyph in LatinCharsData.NonLettersE1)
        {
            yield return new object[] { glyph };
        }
    }

    [Theory]
    [MemberData(nameof(InvalidGlyphsTestData))]
    public void TestRejectsInvalidGlyphs(string glyph)
    {
        Assert.False(TypeC.IsValid(glyph));
    }

    public static IEnumerable<object[]> InvalidGlyphsTestData()
    {
        foreach (object[] data in NotConvertibleTestData())
        {
            yield return data;
        }

        foreach (string glyph in TestData.DeprecatedLatinLetters.Keys)
        {
            yield return new object[] { glyph };
        }
    }
}