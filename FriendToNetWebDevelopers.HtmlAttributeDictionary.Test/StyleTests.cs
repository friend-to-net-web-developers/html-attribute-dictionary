using FriendToNetWebDevelopers.HtmlAttributeDictionary.Models;

namespace FriendToNetWebDevelopers.HtmlAttributeDictionary.Test;

[TestFixture]
public class StyleTests
{
    [Test]
    public void SetStyles_1Valid()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.SetStyleAttribute("display:none;");
        Assert.Multiple(() =>
        {
            Assert.That(okay, Is.EqualTo(new Tuple<bool, int>(true, 1)));
            Assert.That(dictionary, Is.Not.Empty);
        });
        var declarations = dictionary.GetStyleAttribute();
        Assert.That(declarations, Is.Not.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(declarations.ContainsKey("display"));
            Assert.That(declarations.ToString(), Is.EqualTo("display:none;"));
            Assert.That(declarations["display"], Is.EqualTo("none"));
        });
    }

    [Test]
    public void SetStyle_Individual()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var styleAttribute = dictionary.GetStyleAttribute();
        Assert.That(styleAttribute, Is.Empty);
        Assert.That(styleAttribute.SetDeclaration("display:none"));
        dictionary.SetStyleAttribute(styleAttribute);
        Assert.That(dictionary["style"], Is.EqualTo("display:none;"));
    }
    
    [Test]
    public void SetStyle_FromDictionary()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var styleAttributes = new Dictionary<string, string>
        {
            ["display"] = "none"
        };
        var styleAttribute = new StyleAttributeDictionary(styleAttributes);
        dictionary.SetStyleAttribute(styleAttribute);
        Assert.That(dictionary["style"], Is.EqualTo("display:none;"));
        styleAttribute["display"] = "flex";
        dictionary.SetStyleAttribute(styleAttribute);
        Assert.That(dictionary["style"], Is.EqualTo("display:flex;"));
    }
    
    [Test]
    public void SetStyles_2Valid()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var okay = dictionary.SetStyleAttribute("display:none;color:#111111;");
        Assert.Multiple(() =>
        {
            Assert.That(okay, Is.EqualTo(new Tuple<bool, int>(true, 2)));
            Assert.That(dictionary, Is.Not.Empty);
        });
        var declarations = dictionary.GetStyleAttribute();
        Assert.That(declarations, Is.Not.Empty);
        Assert.Multiple(() =>
        {
            Assert.That(declarations.ContainsKey("display"));
            Assert.That(declarations.ContainsKey("color"));
            Assert.That(declarations.ToString(), Is.EqualTo("display:none;color:#111111;"));
        });
        Assert.That(dictionary.ToString(), Is.EqualTo("style=\"display:none;color:#111111;\""));
    }

    [Test]
    public void SetStyles_2Valid_SimpleMethod()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        dictionary["style"] = "display:none;color:#111111;";
        Assert.That(dictionary.ToString(), Is.EqualTo("style=\"display:none;color:#111111;\""));
    }
    
    [Test]
    public void SetStyles_1Valid_3Invalid_SimpleMethod()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        dictionary["style"] = "display:none;#color:#111111;color:@foo;background-image:url(\"http://foo-bar.com/icon.png\");";
        Assert.That(dictionary.ToString(), Is.EqualTo("style=\"display:none;\""));
    }

    [Test]
    public void SetDisplayNone()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var result = dictionary.AddDisplayNoneToStyle();
        Assert.Multiple(() =>
        {
            Assert.That(result.Item1);
            Assert.That(result.Item2, Is.EqualTo(1));
            Assert.That(result.Item3, Is.EqualTo(1));
        });
        Assert.That(dictionary.ToString(), Is.EqualTo("style=\"display:none;\""));
    }
    
    [Test]
    public void AddStyles_DisplayNone()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        dictionary.SetStyleAttribute("color:white;");
        Assert.That(dictionary.ToString(), Is.EqualTo("style=\"color:white;\""));
        dictionary.AddDisplayNoneToStyle();
        Assert.That(dictionary.ToString(), Is.EqualTo("style=\"color:white;display:none;\""));
    }

    [Test]
    public void RemoveStyles_Attribute()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var setResult = dictionary.SetStyleAttribute("color:white;");
        Assert.Multiple(() =>
        {
            Assert.That(setResult.Item1);
            Assert.That(setResult.Item2, Is.EqualTo(1));
        });
        Assert.That(dictionary.ToString(), Is.EqualTo("style=\"color:white;\""));
        var result = dictionary.RemoveStyleAttribute();
        Assert.Multiple(() =>
        {
            Assert.That(result);
            Assert.That(dictionary.ToString(), Is.EqualTo(string.Empty));
        });
    }

    [Test]
    public void RemoveStyles_Add3_Remove_1Valid_1Missing()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        Assert.That(dictionary, Is.Empty);
        var setResult = dictionary.SetStyleAttribute("color:white;background-color:white;display:block;");
        Assert.Multiple(() =>
        {
            Assert.That(setResult.Item1);
            Assert.That(setResult.Item2, Is.EqualTo(3));
        });
        Assert.That(dictionary.ToString(), Is.EqualTo("style=\"color:white;background-color:white;display:block;\""));
        var result = dictionary.RemoveDeclarationsFromStyleAttribute(new HashSet<string>
        {
            "background-color",
            "foo-bar"
        });
        Assert.Multiple(() =>
        {
            Assert.That(result.Item1);
            Assert.That(result.Item2, Is.EqualTo(1));
            Assert.That(result.Item3, Is.EqualTo(2));
        });
        Assert.That(dictionary.ToString(), Is.EqualTo("style=\"color:white;display:block;\""));
    }
}