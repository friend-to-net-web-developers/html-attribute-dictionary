namespace FriendToNetWebDevelopers.HtmlAttributeDictionary.Test;

[TestFixture]
public class StandardTests
{
    [Test]
    public void SetAttributeDiscardNull_WithValue()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.SetAttributeDiscardNull("rows", "5");
        Assert.Multiple(() =>
        {
            Assert.That(okay);
            Assert.That(dictionary, Is.Not.Empty);
        });
        Assert.Multiple(() =>
        {
            Assert.That(dictionary.ContainsKey("rows"));
            Assert.That(dictionary["rows"], Is.EqualTo("5"));
        });
    }

    [Test]
    public void SetAttributeDiscardNull_WithNull()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.SetAttributeDiscardNull("rows", null);
        Assert.Multiple(() =>
        {
            Assert.That(okay, Is.False);
            Assert.That(dictionary, Is.Empty);
        });
    }

    [Test]
    public void SetAttributeDiscardNullOrEmpty_WithValue()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.SetAttributeDiscardNullOrEmpty("rows", "5");
        Assert.Multiple(() =>
        {
            Assert.That(okay);
            Assert.That(dictionary, Is.Not.Empty);
        });
        Assert.Multiple(() =>
        {
            Assert.That(dictionary.ContainsKey("rows"));
            Assert.That(dictionary["rows"], Is.EqualTo("5"));
        });
    }

    [Test]
    public void SetAttributeDiscardNullOrEmpty_WithEmpty()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.SetAttributeDiscardNullOrEmpty("rows", string.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(okay, Is.False);
            Assert.That(dictionary, Is.Empty);
        });
    }

    [Test]
    public void SetAttributeOnly_ValidAttribute()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.SetAttributeOnly("rows");
        Assert.Multiple(() =>
        {
            Assert.That(okay, Is.True);
            Assert.That(dictionary, Is.Not.Empty);
        });
        Assert.That(dictionary.ContainsKey("rows"));
    }

    [Test]
    public void SetAttributeOnly_InvalidAttribute()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.SetAttributeOnly("bob");
        Assert.Multiple(() =>
        {
            Assert.That(okay, Is.False);
            Assert.That(dictionary, Is.Empty);
        });
        Assert.That(dictionary.ContainsKey("bob"), Is.False);
    }

    [Test]
    public void Add_1()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Empty);
        dictionary.Add("data-foo", "bar");
        Assert.That(dictionary, Is.Not.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(dictionary.ContainsKey("data-foo"));
            Assert.That(dictionary["data-foo"], Is.EqualTo("bar"));
        });
    }

    [Test]
    public void Add_2()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Empty);
        dictionary.Add("data-foo", "bar");
        Assert.That(dictionary, Is.Not.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(dictionary.ContainsKey("data-foo"));
            Assert.That(dictionary["data-foo"], Is.EqualTo("bar"));
        });
        Assert.Throws<ArgumentException>(() =>
        {
            dictionary.Add("data-foo", "bar");
        });
    }

    [Test]
    public void TryAdd_1()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.TryAdd("data-foo", "bar");
        Assert.Multiple(() =>
        {
            Assert.That(okay);
            Assert.That(dictionary, Is.Not.Empty);
        });
        Assert.Multiple(() =>
        {
            Assert.That(dictionary.ContainsKey("data-foo"));
            Assert.That(dictionary["data-foo"], Is.EqualTo("bar"));
        });
    }

    [Test]
    public void TryAdd_2()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.TryAdd("data-foo", "bar");
        Assert.Multiple(() =>
        {
            Assert.That(okay);
            Assert.That(dictionary, Is.Not.Empty);
        });
        Assert.Multiple(() =>
        {
            Assert.That(dictionary.ContainsKey("data-foo"));
            Assert.That(dictionary["data-foo"], Is.EqualTo("bar"));
        });
        okay = dictionary.TryAdd("data-foo", "bar");
        Assert.That(okay, Is.False);
    }

    [Test]
    public void Set_Valid()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Empty);
        dictionary["data-foo"] = "bar";
        Assert.That(dictionary, Is.Not.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(dictionary.ContainsKey("data-foo"));
            Assert.That(dictionary["data-foo"], Is.EqualTo("bar"));
        });
    }

    [Test]
    public void Set_Invalid()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Empty);
        dictionary["foo"] = "bar";
        Assert.That(dictionary, Is.Empty);
    }
    
    
}