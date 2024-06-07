namespace FriendToNetWebDevelopers.HtmlAttributeDictionary.Test;

[TestFixture]
public class DataAttributeConvenienceTests
{
    [Test]
    public void Data()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.SetData("foo", "bar");
        Assert.Multiple(() =>
        {
            Assert.That(okay);
            Assert.That(dictionary["data-foo"], Is.EqualTo("bar"));
        });
        okay = dictionary.RemoveData("foo");
        Assert.Multiple(() =>
        {
            Assert.That(okay);
            Assert.That(dictionary, Is.Empty);
        });
    }

    [Test]
    public void Aria()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.SetAria("foo", "bar");
        Assert.Multiple(() =>
        {
            Assert.That(okay);
            Assert.That(dictionary["aria-foo"], Is.EqualTo("bar"));
        });
        okay = dictionary.RemoveAria("foo");
        Assert.Multiple(() =>
        {
            Assert.That(okay);
            Assert.That(dictionary, Is.Empty);
        });
    }
}