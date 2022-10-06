namespace Din91379Tests;

using Din91379;


public class ConversionTest
{
    [Theory]
    [MemberData(nameof(DeprecatedGlyphsTestData))]
    public void TestTypeAConversion(string glyph, string replacement)
    {
        TypeA value = TypeA.FromString(glyph);
        Assert.Equal(value, replacement);
    }

    [Theory]
    [MemberData(nameof(DeprecatedGlyphsTestData))]
    public void TestTypeBConversion(string glyph, string replacement)
    {
        TypeB value = TypeB.FromString(glyph);
        Assert.Equal(value, replacement);
    }

    [Theory]
    [MemberData(nameof(DeprecatedGlyphsTestData))]
    public void TestTypeCConversion(string glyph, string replacement)
    {
        TypeC value = TypeC.FromString(glyph);
        Assert.Equal(value, replacement);
    }

    [Theory]
    [MemberData(nameof(DeprecatedGlyphsTestData))]
    public void TestTypeDConversion(string glyph, string replacement)
    {
        TypeD value = TypeD.FromString(glyph);
        Assert.Equal(value, replacement);
    }

    [Theory]
    [MemberData(nameof(DeprecatedGlyphsTestData))]
    public void TestTypeEConversion(string glyph, string replacement)
    {
        TypeE value = TypeE.FromString(glyph);
        Assert.Equal(value, replacement);
    }

    public static IEnumerable<object[]> DeprecatedGlyphsTestData()
    {
        foreach (KeyValuePair<string, string> item in TestData.DeprecatedLatinLetters)
        {
            yield return new object[] { item.Key, item.Value };
        }
    }

    [Theory]
    [MemberData(nameof(DeprecatedGlyphsTestData))]
    public void TestTypeANormalization(string nonNormalized, string normalized)
    {
        Assert.False(TypeA.IsValid(nonNormalized));

        TypeA value = TypeA.FromString(nonNormalized);
        Assert.Equal(value, normalized);
    }

    [Theory]
    [MemberData(nameof(DeprecatedGlyphsTestData))]
    public void TestTypeBNormalization(string nonNormalized, string normalized)
    {
        Assert.False(TypeB.IsValid(nonNormalized));

        TypeB value = TypeB.FromString(nonNormalized);
        Assert.Equal(value, normalized);
    }

    [Theory]
    [MemberData(nameof(DeprecatedGlyphsTestData))]
    public void TestTypeCNormalization(string nonNormalized, string normalized)
    {
        Assert.False(TypeC.IsValid(nonNormalized));

        TypeC value = TypeC.FromString(nonNormalized);
        Assert.Equal(value, normalized);
    }

    [Theory]
    [MemberData(nameof(DeprecatedGlyphsTestData))]
    public void TestTypeDNormalization(string nonNormalized, string normalized)
    {
        Assert.False(TypeD.IsValid(nonNormalized));

        TypeD value = TypeD.FromString(nonNormalized);
        Assert.Equal(value, normalized);
    }

    [Theory]
    [MemberData(nameof(DeprecatedGlyphsTestData))]
    public void TestTypeENormalization(string nonNormalized, string normalized)
    {
        Assert.False(TypeE.IsValid(nonNormalized));

        TypeE value = TypeE.FromString(nonNormalized);
        Assert.Equal(value, normalized);
    }

    public static IEnumerable<object[]> NonNfcTestData()
    {
        yield return new object[] {
            char.ConvertFromUtf32(0x0041) + char.ConvertFromUtf32(0x0308),
            "Ä",
        };
    }
}