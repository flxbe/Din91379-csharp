namespace Din91379Tests;

using Din91379;


public class SearchFormTest
{
    [Theory]
    [MemberData(nameof(TypeASearchFormTestData))]
    public void TestTypeASearchForm(string original, string expected)
    {
        TypeA value = TypeA.FromString(original);

        Assert.Equal(expected, value.GetSearchForm());
    }

    public static IEnumerable<object[]> TypeASearchFormTestData()
    {
        // Test cases from
        // https://xoev.de/latinchars/1_1/supplement/identverfahren.pdf
        yield return new object[] { "René Müller", "RENE MUELLER" };
        yield return new object[] { "Rene Mueller", "RENE MUELLER" };
        yield return new object[] { "Nœl Schmidt-Strauß", "NOEL SCHMIDT-STRAUSS" };
        yield return new object[] { "Noël Schmidt-Strauß", "NOEL SCHMIDT-STRAUSS" };

        foreach (KeyValuePair<string, string> item in TestData.Groups.LatinLetters)
        {
            yield return new object[] { item.Key, item.Value };
        }

        foreach (KeyValuePair<string, string> item in TestData.Groups.NonLettersN1)
        {
            yield return new object[] { item.Key, item.Value };
        }
    }

    [Theory]
    [MemberData(nameof(TypeBSearchFormTestData))]
    public void TestTypeBSearchForm(string original, string expected)
    {
        TypeB value = TypeB.FromString(original);

        Assert.Equal(expected, value.GetSearchForm());
    }

    public static IEnumerable<object[]> TypeBSearchFormTestData()
    {
        // Test cases from
        // https://xoev.de/latinchars/1_1/supplement/identverfahren.pdf
        yield return new object[] { "Karl-Bröger Straße 17", "KARL-BROEGER STRASSE 17" };
        yield return new object[] { "Karl-Broeger Strasse 17", "KARL-BROEGER STRASSE 17" };

        foreach (object[] item in TypeASearchFormTestData())
        {
            yield return item;
        }

        foreach (KeyValuePair<string, string> item in TestData.Groups.NonLettersN2)
        {
            yield return new object[] { item.Key, item.Value };
        }
    }

    [Theory]
    [MemberData(nameof(TypeCSearchFormTestData))]
    public void TestTypeCSearchForm(string original, string expected)
    {
        TypeC value = TypeC.FromString(original);

        Assert.Equal(expected, value.GetSearchForm());
    }

    public static IEnumerable<object[]> TypeCSearchFormTestData()
    {
        foreach (object[] item in TypeBSearchFormTestData())
        {
            yield return item;
        }

        foreach (KeyValuePair<string, string> item in TestData.Groups.NonLettersN3)
        {
            yield return new object[] { item.Key, item.Value };
        }

        foreach (KeyValuePair<string, string> item in TestData.Groups.NonLettersN4)
        {
            yield return new object[] { item.Key, item.Value };
        }
    }
}