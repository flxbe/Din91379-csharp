namespace Din91379Tests;

using Din91379;


public class DinSpec91379CompatibilityTest
{
    [Theory]
    [MemberData(nameof(NewCharacters))]
    public void TestShouldMarkNewCharactersAsNotCompatible(string glyph)
    {
        TypeE value = TypeE.FromString(glyph);

        Assert.False(value.IsDinSpec91379Compatible());
    }

    public static IEnumerable<object[]> NewCharacters()
    {
        foreach (string glyph in TestData.NewGlyphs)
        {
            yield return new object[] { glyph };
        }
    }

    [Theory]
    [MemberData(nameof(OldCharacters))]
    public void TestShouldMarkOldCharactersAsCompatible(string glyph)
    {
        TypeE value = TypeE.FromString(glyph);

        Assert.True(value.IsDinSpec91379Compatible());
    }

    public static IEnumerable<object[]> OldCharacters()
    {
        foreach (string glyph in TestData.Groups.GetGlyphEnumerator())
        {
            if (!TestData.NewGlyphs.Contains(glyph))
            {
                yield return new object[] { glyph };
            }
        }
    }
}