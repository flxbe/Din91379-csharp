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

        foreach (KeyValuePair<string, string> item in Glyphs.LatinLetters)
        {
            yield return new object[] { item.Key, item.Value };
        }

        foreach (string glyph in Glyphs.NonLettersN1)
        {
            yield return new object[] { glyph, glyph };
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

        foreach (string glyph in Glyphs.NonLettersN2)
        {
            // Respect special non-letter transliterations as described in
            // https://xoev.de/latinchars/1_1/supplement/identverfahren.pdf
            string searchForm = Glyphs.NonLetterTransliterations.GetValueOrDefault(glyph, glyph);
            yield return new object[] { glyph, searchForm };
        }

        // Sanity checks for non-letter transliteration.
        // Use prefix "Test " to distinguish from generated test cases above.
        yield return new object[] { "Test ª", "TEST A" }; // Special transliteration case
        yield return new object[] { "Test º", "TEST O" }; // Special transliteration case
        yield return new object[] { "Test !", "TEST !" }; // Default case: no transliteration
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

        foreach (string glyph in Glyphs.NonLettersN3)
        {
            yield return new object[] { glyph, glyph };
        }

        foreach (string glyph in Glyphs.NonLettersN4)
        {
            yield return new object[] { glyph, glyph };
        }
    }
}