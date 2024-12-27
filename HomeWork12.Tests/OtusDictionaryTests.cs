using HomeWork12;

public class OtusDictionaryTests
{
    [Fact]
    public void Add_WhenKeyDoesNotExist_ShouldAddValue()
    {
        // Arrange
        var dictionary = new OtusDictionary();

        // Act
        dictionary.Add(1, "One");

        // Assert
        Assert.Equal("One", dictionary.Get(1));
    }

    [Fact]
    public void Add_WhenKeyExists_ShouldThrowException()
    {
        // Arrange
        var dictionary = new OtusDictionary();
        dictionary.Add(1, "One");

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => dictionary.Add(1, "New One"));
        Assert.Contains("1", exception.Message);
    }

    [Fact]
    public void Add_WhenValueIsNull_ShouldThrowException()
    {
        // Arrange
        var dictionary = new OtusDictionary();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => dictionary.Add(1, null));
    }

    [Fact]
    public void Get_WhenKeyExists_ShouldReturnValue()
    {
        // Arrange
        var dictionary = new OtusDictionary();
        dictionary.Add(1, "One");

        // Act
        var result = dictionary.Get(1);

        // Assert
        Assert.Equal("One", result);
    }

    [Fact]
    public void Get_WhenKeyDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var dictionary = new OtusDictionary();

        // Act
        var result = dictionary.Get(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Indexer_ShouldWorkSameAsAddAndGet()
    {
        // Arrange
        var dictionary = new OtusDictionary();

        // Act
        dictionary[1] = "One";

        // Assert
        Assert.Equal("One", dictionary[1]);
    }

    [Fact]
    public void Indexer_WhenSettingNull_ShouldThrowException()
    {
        // Arrange
        var dictionary = new OtusDictionary();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => dictionary[1] = null);
    }

    [Fact]
    public void Add_WhenCollisionOccurs_ShouldResizeAndRelocateItems()
    {
        // Arrange
        var dictionary = new OtusDictionary();

        // Act
        // Adding keys that will have the same initial hash (32 is initial size)
        dictionary.Add(0, "Zero");
        dictionary.Add(32, "Thirty Two"); // This will cause collision and resize

        // Assert
        Assert.Equal("Zero", dictionary.Get(0));
        Assert.Equal("Thirty Two", dictionary.Get(32));
    }

    [Fact]
    public void Add_MultipleDifferentKeysWithSameHash_ShouldWorkCorrectly()
    {
        // Arrange
        var dictionary = new OtusDictionary();

        // Act
        // Adding multiple keys that would initially hash to the same index
        for (int i = 0; i < 5; i++)
        {
            dictionary.Add(i * 32, $"Value {i * 32}");
        }

        // Assert
        for (int i = 0; i < 5; i++)
        {
            Assert.Equal($"Value {i * 32}", dictionary.Get(i * 32));
        }
    }

    [Fact]
    public void Add_LargeNumberOfItems_ShouldHandleResizingCorrectly()
    {
        // Arrange
        var dictionary = new OtusDictionary();
        const int itemCount = 100;

        // Act
        for (int i = 0; i < itemCount; i++)
        {
            dictionary.Add(i, $"Value {i}");
        }

        // Assert
        for (int i = 0; i < itemCount; i++)
        {
            Assert.Equal($"Value {i}", dictionary.Get(i));
        }
    }

    [Fact]
    public void Add_NegativeKeys_ShouldWorkCorrectly()
    {
        // Arrange
        var dictionary = new OtusDictionary();

        // Act
        dictionary.Add(-1, "Minus One");
        dictionary.Add(-32, "Minus Thirty Two");
        dictionary.Add(-64, "Minus Sixty Four");

        // Assert
        Assert.Equal("Minus One", dictionary.Get(-1));
        Assert.Equal("Minus Thirty Two", dictionary.Get(-32));
        Assert.Equal("Minus Sixty Four", dictionary.Get(-64));
    }
}