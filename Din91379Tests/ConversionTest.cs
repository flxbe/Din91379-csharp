namespace Din91379Tests;

using Din91379;


public class ConversionTest
{
    [Theory]
    [MemberData(nameof(NonNfcTestData))]
    public void TestTypeANormalization(string nonNormalized, string normalized)
    {
        Assert.False(TypeA.IsValid(nonNormalized));

        TypeA value = TypeA.FromString(nonNormalized);
        Assert.Equal(value, normalized);
    }

    [Theory]
    [MemberData(nameof(NonNfcTestData))]
    public void TestTypeBNormalization(string nonNormalized, string normalized)
    {
        Assert.False(TypeB.IsValid(nonNormalized));

        TypeB value = TypeB.FromString(nonNormalized);
        Assert.Equal(value, normalized);
    }

    [Theory]
    [MemberData(nameof(NonNfcTestData))]
    public void TestTypeCNormalization(string nonNormalized, string normalized)
    {
        Assert.False(TypeC.IsValid(nonNormalized));

        TypeC value = TypeC.FromString(nonNormalized);
        Assert.Equal(value, normalized);
    }

    [Theory]
    [MemberData(nameof(NonNfcTestData))]
    public void TestTypeDNormalization(string nonNormalized, string normalized)
    {
        Assert.False(TypeD.IsValid(nonNormalized));

        TypeD value = TypeD.FromString(nonNormalized);
        Assert.Equal(value, normalized);
    }

    [Theory]
    [MemberData(nameof(NonNfcTestData))]
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