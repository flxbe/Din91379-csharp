using System.Xml;
using System.Text;

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

    [Theory]
    [MemberData(nameof(LatinCharsSearchFormTestSet))]
    public void TestLatinCharsTransliteration(string original, string expected)
    {
        TypeC value = TypeC.FromString(original);

        Assert.Equal(expected, value.GetSearchForm());
    }

    public static IEnumerable<object[]> LatinCharsSearchFormTestSet()
    {
        string[] excludedSamples = { "Ŀ", "ŉ", "ŀ" };

        XmlDocument doc = new XmlDocument();
        doc.Load("../../../latinchars.xml");

        XmlNodeList characterNodes = doc.GetElementsByTagName("char");
        foreach (XmlNode node in characterNodes)
        {
            Tuple<string, string>? info = GetTransliterationInfo(node);


            if (!(info is null))
            {
                // Skip deprecated samples for no, until clearing up
                // if the breaking change in search form transliteration is intentional.
                if (excludedSamples.Contains(info.Item1))
                {
                    continue;
                }

                yield return new object[] { info.Item1, info.Item2 };
            }
        }

        XmlNodeList sequenceNodes = doc.GetElementsByTagName("sequence");
        foreach (XmlNode node in sequenceNodes)
        {
            Tuple<string, string>? info = GetTransliterationInfo(node);

            if (!(info is null))
            {
                yield return new object[] { info.Item1, info.Item2 };
            }
        }
    }

    private static Tuple<string, string>? GetTransliterationInfo(XmlNode node)
    {
        XmlNode? cpNode = node.ChildNodes[0];
        if (cpNode is null || cpNode.Name != "cp")
        {
            throw new Exception("character cpNode is null");
        }
        string glyph = ParseCodePoints(cpNode.InnerText);

        // Not all glyphs have mapping info.
        // Example: 00A0 (NO-BREAK SPACE)
        XmlNode? mappingNode = node.ChildNodes[2];
        if (mappingNode is null || mappingNode.Name != "mapping")
        {
            return null;
        }

        string type = GetAttribute(mappingNode, "type");
        if (type == "identity")
        {
            return new Tuple<string, string>(glyph, glyph);
        }
        else if (type == "mapped")
        {
            StringBuilder builder = new StringBuilder();
            foreach (XmlNode destinationNode in mappingNode)
            {
                builder.Append(ParseCodePoints(destinationNode.InnerText));
            }

            string destination = builder.ToString();

            return new Tuple<string, string>(glyph, destination);
        }
        else
        {
            throw new Exception("Unknown mapping type");
        }

    }

    private static string ParseCodePoints(string value)
    {
        StringBuilder builder = new StringBuilder();
        foreach (string codePoint in value.Split(" "))
        {
            string character = char.ConvertFromUtf32(int.Parse(codePoint, System.Globalization.NumberStyles.HexNumber));
            builder.Append(character);
        }

        return builder.ToString();
    }

    private static string GetAttribute(XmlNode node, string key)
    {
        XmlAttributeCollection? attributes = node.Attributes;
        if (attributes is null)
        {
            throw new Exception("attributes is null");
        }

        XmlAttribute? attribute = attributes[key];
        if (attribute is null)
        {
            throw new Exception("Cannot find attribute");
        }

        return attribute.Value;
    }
}