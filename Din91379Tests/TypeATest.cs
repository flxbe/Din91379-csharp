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

        foreach (string validGlyph in TestData.Groups.LatinLetters.Keys)
        {
            yield return new object[] { validGlyph };
        }

        foreach (string validGlyph in TestData.Groups.NonLettersN1.Keys)
        {
            yield return new object[] { validGlyph };
        }
    }

    [Theory]
    [MemberData(nameof(InvalidGlyphsTestData))]
    public void TestRejectsNotConvertibleGlyphs(string glyph)
    {
        Assert.Throws<InvalidGlyphException>(() => TypeA.FromString(glyph));

        Assert.False(TypeA.IsValid(glyph));
    }

    public static IEnumerable<object[]> InvalidGlyphsTestData()
    {
        foreach (string glyph in TestData.GloballyInvalidStrings)
        {
            yield return new object[] { glyph };
        }

        foreach (string glyph in TestData.Groups.NonLettersN2.Keys)
        {
            yield return new object[] { glyph };
        }

        foreach (string glyph in TestData.Groups.NonLettersN3.Keys)
        {
            yield return new object[] { glyph };
        }

        foreach (string glyph in TestData.Groups.NonLettersN4.Keys)
        {
            yield return new object[] { glyph };
        }

        foreach (string glyph in TestData.Groups.GreekLetters)
        {
            yield return new object[] { glyph };
        }

        foreach (string glyph in TestData.Groups.CyrillicLetters)
        {
            yield return new object[] { glyph };
        }

        foreach (string glyph in TestData.Groups.NonLettersE1)
        {
            yield return new object[] { glyph };
        }
    }
}