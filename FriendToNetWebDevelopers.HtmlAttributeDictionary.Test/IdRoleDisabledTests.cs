namespace FriendToNetWebDevelopers.HtmlAttributeDictionary.Test;

[TestFixture]
public class IdRoleDisabledTests
{
    [Test]
    public void IdSetValid()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.SetId("foo_bar");
        Assert.Multiple(() =>
        {
            Assert.That(okay);
            Assert.That(dictionary, Is.Not.Empty);
        });
        Assert.Multiple(() =>
        {
            Assert.That(dictionary.ContainsKey("id"), Is.True);
            Assert.That(dictionary["id"], Is.EqualTo("foo_bar"));
        });
    }

    [Test]
    public void IdSetInvalid()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.SetId("foo bar");
        Assert.Multiple(() =>
        {
            Assert.That(okay, Is.False);
            Assert.That(dictionary, Is.Empty);
        });
    }
    
    [Test]
    public void IdRemove()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.SetId("foo_bar");
        Assert.Multiple(() =>
        {
            Assert.That(okay);
            Assert.That(dictionary, Is.Not.Empty);
        });
        Assert.That(dictionary.ContainsKey("id"), Is.True);
        dictionary.RemoveId();
        Assert.That(dictionary, Is.Empty);
    }
    
    [Test]
    public void RoleSetPresent()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.SetRole("panel");
        Assert.That(okay);
        Assert.Multiple(() =>
        {
            Assert.That(dictionary.ContainsKey("role"));
            Assert.That(dictionary["role"], Is.EqualTo("panel"));
        });
    }

    [Test]
    public void RoleSetEmpty()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.SetRole(string.Empty);
        Assert.That(okay, Is.False);
        Assert.That(dictionary, Is.Empty);
    }
    
    [Test]
    public void RoleSetNull()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.SetRole(null);
        Assert.That(okay, Is.False);
        Assert.That(dictionary, Is.Empty);
    }
    
    [Test]
    public void RoleRemove()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.SetRole("panel");
        Assert.That(okay);
        Assert.Multiple(() =>
        {
            Assert.That(dictionary.ContainsKey("role"));
            Assert.That(dictionary["role"], Is.EqualTo("panel"));
        });
        dictionary.RemoveRole();
        Assert.That(dictionary, Is.Empty);
    }

    [Test]
    public void DisabledSet()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        dictionary.SetDisabled();
        Assert.Multiple(() =>
        {
            Assert.That(dictionary.ContainsKey("disabled"));
        });
    }
    
    [Test]
    public void DisabledRemove()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        dictionary.SetDisabled();
        Assert.Multiple(() =>
        {
            Assert.That(dictionary.ContainsKey("disabled"));
        });
        dictionary.RemoveDisabled();
        Assert.That(dictionary, Is.Empty);
    }
}