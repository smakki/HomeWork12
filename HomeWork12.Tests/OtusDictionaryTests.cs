using HomeWork12;

public class OtusDictionaryTests
{
    [Fact]
    public void Add_WhenKeyDoesNotExist_ShouldAddValue()
    {
        var dictionary = new OtusDictionary();

        dictionary.Add(1, "One");

        Assert.Equal("One", dictionary.Get(1));
    }

    [Fact]
    public void Add_WhenKeyExists_ShouldThrowException()
    {
        var dictionary = new OtusDictionary();
        dictionary.Add(1, "One");

        var exception = Assert.Throws<ArgumentException>(() => dictionary.Add(1, "New One"));
        Assert.Contains("1", exception.Message);
    }

    [Fact]
    public void Add_WhenValueIsNull_ShouldThrowException()
    {
        var dictionary = new OtusDictionary();

        Assert.Throws<ArgumentNullException>(() => dictionary.Add(1, null));
    }

    [Fact]
    public void Get_WhenKeyExists_ShouldReturnValue()
    {
        var dictionary = new OtusDictionary();
        dictionary.Add(1, "One");

        var result = dictionary.Get(1);

        Assert.Equal("One", result);
    }

    [Fact]
    public void Get_WhenKeyDoesNotExist_ShouldReturnNull()
    {
        var dictionary = new OtusDictionary();

        var result = dictionary.Get(1);

        Assert.Null(result);
    }

    [Fact]
    public void Indexer_ShouldWorkSameAsAddAndGet()
    {
        var dictionary = new OtusDictionary();

        dictionary[1] = "One";

        Assert.Equal("One", dictionary[1]);
    }

    [Fact]
    public void Indexer_WhenSettingNull_ShouldThrowException()
    {
        
        var dictionary = new OtusDictionary();

        Assert.Throws<ArgumentNullException>(() => dictionary[1] = null);
    }

    [Fact]
    public void Add_WhenCollisionOccurs_ShouldResizeAndRelocateItems()
    {
        var dictionary = new OtusDictionary();

        dictionary.Add(0, "Zero");
        dictionary.Add(32, "Thirty Two"); // This will cause collision and resize

        Assert.Equal("Zero", dictionary.Get(0));
        Assert.Equal("Thirty Two", dictionary.Get(32));
    }

    [Fact]
    public void Add_MultipleDifferentKeysWithSameHash_ShouldWorkCorrectly()
    {
        var dictionary = new OtusDictionary();

        for (int i = 0; i < 5; i++)
        {
            dictionary.Add(i * 32, $"Value {i * 32}");
        }

        for (int i = 0; i < 5; i++)
        {
            Assert.Equal($"Value {i * 32}", dictionary.Get(i * 32));
        }
    }

    [Fact]
    public void Add_LargeNumberOfItems_ShouldHandleResizingCorrectly()
    {
        var dictionary = new OtusDictionary();
        const int itemCount = 100;

        for (int i = 0; i < itemCount; i++)
        {
            dictionary.Add(i, $"Value {i}");
        }

        for (int i = 0; i < itemCount; i++)
        {
            Assert.Equal($"Value {i}", dictionary.Get(i));
        }
    }

    [Fact]
    public void Add_NegativeKeys_ShouldWorkCorrectly()
    {
        var dictionary = new OtusDictionary();

        dictionary.Add(-1, "Minus One");
        dictionary.Add(-32, "Minus Thirty Two");
        dictionary.Add(-64, "Minus Sixty Four");

        Assert.Equal("Minus One", dictionary.Get(-1));
        Assert.Equal("Minus Thirty Two", dictionary.Get(-32));
        Assert.Equal("Minus Sixty Four", dictionary.Get(-64));
    }
}