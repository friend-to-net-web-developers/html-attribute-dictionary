namespace FriendToNetWebDevelopers.HtmlAttributeDictionary.Test;

[TestFixture]
public class FactoryTests
{

    [Test]
    public void FactoryEmpty()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Empty);
    }

    [Test]
    public void FactoryWithValidId()
    {
        var dictionary = HtmlAttributeDictionaryFactory.WithId("foo");
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Not.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(dictionary.ContainsKey("id"), Is.True);
            Assert.That(dictionary["id"], Is.EqualTo("foo"));
        });
    }

    [Test]
    public void FactoryWithInvalidId()
    {
        var dictionary = HtmlAttributeDictionaryFactory.WithId("foo bar");
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Empty);
        Assert.That(dictionary.ContainsKey("id"), Is.False);
    }

    [Test]
    public void FactoryWithValidIdAndValidClassesString()
    {
        var dictionary = HtmlAttributeDictionaryFactory.WithId("foo", "bar baz");
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Not.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(dictionary.ContainsKey("id"), Is.True);
            Assert.That(dictionary.ContainsKey("class"), Is.True);
        });
        var classes = dictionary.GetClasses();
        Assert.That(classes, Is.Not.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(classes.Contains("bar"), Is.True);
            Assert.That(classes.Contains("baz"), Is.True);
        });
    }
    
    [Test]
    public void FactoryWithValidIdAndInvalidClassesString()
    {
        var dictionary = HtmlAttributeDictionaryFactory.WithId("foo", "b@r baz");
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Not.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(dictionary.ContainsKey("id"), Is.True);
            Assert.That(dictionary.ContainsKey("class"), Is.True);
        });
        var classes = dictionary.GetClasses();
        Assert.That(classes, Is.Not.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(classes.Contains("b@r"), Is.False);
            Assert.That(classes.Contains("baz"), Is.True);
        });
    }

    [Test]
    public void FactoryWithClassesValid()
    {
        var dictionary = HtmlAttributeDictionaryFactory.WithClasses("bar baz");
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Not.Empty);
        Assert.That(dictionary.ContainsKey("class"), Is.True);
        var classes = dictionary.GetClasses();
        Assert.Multiple(() =>
        {
            Assert.That(classes.Contains("bar"), Is.True);
            Assert.That(classes.Contains("baz"), Is.True);
        });
    }

    [Test]
    public void FactoryWithClassesInvalid()
    {
        var dictionary = HtmlAttributeDictionaryFactory.WithClasses("b@r baz");
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Not.Empty);
        Assert.That(dictionary.ContainsKey("class"), Is.True);
        var classes = dictionary.GetClasses();
        Assert.Multiple(() =>
        {
            Assert.That(classes.Contains("b@r"), Is.False);
            Assert.That(classes.Contains("baz"), Is.True);
        });
    }
    
    [Test]
    public void FactoryWithClassesAllInvalid()
    {
        var dictionary = HtmlAttributeDictionaryFactory.WithClasses("b@r b@z!");
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Empty);
        var classes = dictionary.GetClasses();
        Assert.Multiple(() =>
        {
            Assert.That(classes.Contains("b@r"), Is.False);
            Assert.That(classes.Contains("b@z!"), Is.False);
        });
    }
    
    [Test]
    public void FactoryGetDictionary()
    {
        var source = new Dictionary<string, string>
        {
            { "id", "bob_dole" },
            { "data-foo", "bar baz" }
        };

        var dictionary = HtmlAttributeDictionaryFactory.Get(source);
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Not.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(dictionary.ContainsKey("id"), Is.True);
            Assert.That(dictionary["id"], Is.EqualTo("bob_dole"));
            Assert.That(dictionary.ContainsKey("data-foo"), Is.True);
            Assert.That(dictionary["data-foo"], Is.EqualTo("bar baz"));
        });
    }

    [Test]
    public void FactoryGetCollection()
    {
        var source = new List<KeyValuePair<string, string>>
        {
            new("id", "bob_dole"),
            new("data-foo", "bar baz")
        };

        var dictionary = HtmlAttributeDictionaryFactory.Get(source);
        Assert.That(dictionary, Is.Not.Null);
        Assert.That(dictionary, Is.Not.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(dictionary.ContainsKey("id"), Is.True);
            Assert.That(dictionary["id"], Is.EqualTo("bob_dole"));
            Assert.That(dictionary.ContainsKey("data-foo"), Is.True);
            Assert.That(dictionary["data-foo"], Is.EqualTo("bar baz"));
        });
    }
    
}