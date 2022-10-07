using System.Xml;
using System.Text;

namespace Din91379Tests;


class LatinChars
{
    public readonly Dictionary<string, string> LatinLetters;
    public readonly Dictionary<string, string> NonLettersN1;
    public readonly Dictionary<string, string> NonLettersN2;
    public readonly Dictionary<string, string> NonLettersN3;
    public readonly Dictionary<string, string> NonLettersN4;
    public readonly List<string> GreekLetters;
    public readonly List<string> CyrillicLetters;
    public readonly List<string> NonLettersE1;

    private LatinChars(XmlDocument doc)
    {
        this.LatinLetters = new Dictionary<string, string>();
        this.NonLettersN1 = new Dictionary<string, string>();
        this.NonLettersN2 = new Dictionary<string, string>();
        this.NonLettersN3 = new Dictionary<string, string>();
        this.NonLettersN4 = new Dictionary<string, string>();
        this.GreekLetters = new List<string>();
        this.CyrillicLetters = new List<string>();
        this.NonLettersE1 = new List<string>();

        foreach (XmlNode node in doc.GetElementsByTagName("char"))
        {
            ImportNode(node);
        }

        foreach (XmlNode node in doc.GetElementsByTagName("sequence"))
        {
            ImportNode(node);
        }

        // Check, that all glyphs have been loaded
        _CheckGlyphCount(this.LatinLetters.Count, 646);
        _CheckGlyphCount(this.NonLettersN1.Count, 18);
        _CheckGlyphCount(this.NonLettersN2.Count, 60);
        _CheckGlyphCount(this.NonLettersN3.Count, 6);
        _CheckGlyphCount(this.NonLettersN4.Count, 4);
        _CheckGlyphCount(this.GreekLetters.Count, 69);
        _CheckGlyphCount(this.CyrillicLetters.Count, 62);
        _CheckGlyphCount(this.NonLettersE1.Count, 40);

        // Add new letters introduced between DIN SPEC 91379 and DIN 91379
        this.LatinLetters.Add("ē̍", "E");
        this.LatinLetters.Add("ḗ", "E");
        this.LatinLetters.Add("ō̍", "O");
        this.NonLettersE1.Add("′");
        this.NonLettersE1.Add("″");

    }

    public static LatinChars Load()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load("../../../latinchars.xml");

        return new LatinChars(doc);
    }

    private void ImportNode(XmlNode node)
    {
        CharacterInfo info = CharacterInfo.Parse(node);

        switch (info.Group)
        {
            case "bll":
                this.LatinLetters.Add(info.Glyph, info.SearchForm);
                break;
            case "bnlreq":
                this.NonLettersN1.Add(info.Glyph, info.SearchForm);
                break;
            case "bnl":
                this.NonLettersN2.Add(info.Glyph, info.SearchForm);
                break;
            case "bnlopt":
                this.NonLettersN3.Add(info.Glyph, info.SearchForm);
                break;
            case "bnlnot":
                this.NonLettersN4.Add(info.Glyph, info.SearchForm);
                break;
            case "dc":
                return;
            case "gl":
                this.GreekLetters.Add(info.Glyph);
                break;
            case "cl":
                this.CyrillicLetters.Add(info.Glyph);
                break;
            case "enl":
                this.NonLettersE1.Add(info.Glyph);
                break;
            default:
                throw new Exception("Invalid group");
        }
    }

    private static void _CheckGlyphCount(int actual, int expected)
    {
        if (actual != expected)
        {
            throw new Exception("Wrong glyph count");
        }
    }
}

class CharacterInfo
{
    public readonly string Group;
    public readonly string Glyph;
    public readonly string SearchForm;

    private CharacterInfo(string group, string glyph, string searchForm)
    {
        this.Group = group;
        this.Glyph = glyph;
        this.SearchForm = searchForm;
    }

    public static CharacterInfo Parse(XmlNode node)
    {
        string group = GetAttribute(node, "group");

        XmlNode? cpNode = node.ChildNodes[0];
        if (cpNode is null || cpNode.Name != "cp")
        {
            throw new Exception("character cpNode is null");
        }
        string glyph = ParseCodePoints(cpNode.InnerText);

        // Not all glyphs have mapping info.
        // Example: 00A0 (NO-BREAK SPACE)
        // Glyphs without a mapping are mapped to the empty string
        XmlNode? mappingNode = node.ChildNodes[2];
        if (mappingNode is null || mappingNode.Name != "mapping")
        {
            return new CharacterInfo(group, glyph, "");
        }

        string type = GetAttribute(mappingNode, "type");
        if (type == "identity")
        {
            return new CharacterInfo(group, glyph, glyph);
        }
        else if (type == "mapped")
        {
            StringBuilder builder = new StringBuilder();
            foreach (XmlNode destinationNode in mappingNode)
            {
                builder.Append(ParseCodePoints(destinationNode.InnerText));
            }

            string searchForm = builder.ToString();

            return new CharacterInfo(group, glyph, searchForm);
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