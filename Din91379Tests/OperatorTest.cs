namespace Din91379Tests;

using Din91379;


public class OperatorTest
{
    [Fact]
    public void TestStringComparison()
    {
        string original = "Hello";
        TypeA a = TypeA.FromString(original);

        Assert.True(original.Equals(a));
        Assert.True(a.Equals(original));

        Assert.True(original == a);
        Assert.True(a == original);

        Assert.Equal(original, a);
        Assert.Equal(a, original);
    }

    [Fact]
    public void TestDin91379Comparison()
    {
        TypeA a = TypeA.FromString("Hello");
        TypeB b = TypeB.FromString("Hello");

        Assert.True(a.Equals(b));
        Assert.True(b.Equals(a));

        Assert.True(a == b);
        Assert.True(b == a);

        Assert.Equal(a, b);
        Assert.Equal(a, a);
    }

    [Fact]
    public void TestHashValue()
    {
        string original = "Hello";
        TypeA a = TypeA.FromString(original);

        Assert.Equal(original.GetHashCode(), a.GetHashCode());
    }
}