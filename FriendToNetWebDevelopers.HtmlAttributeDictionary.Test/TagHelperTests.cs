using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FriendToNetWebDevelopers.HtmlAttributeDictionary.Test;

[TestFixture]
public class TagHelperTests
{
    [Test]
    public void TagHelperProcessCheck()
    {
        var dictionary = HtmlAttributeDictionaryFactory.Get();
        dictionary.SetId("test-id");
        dictionary.AddClasses("test class");

        var tagHelper = new HtmlAttributeTagHelper
        {
            HtmlAttributes = dictionary
        };

        var attributes = new TagHelperAttributeList
        {
            { "html-attributes", tagHelper.HtmlAttributes }
        };

        var tagHelperContext = new TagHelperContext(attributes, new Dictionary<object, object>(),
            nameof(HtmlAttributeTagHelper));

        var output = new TagHelperOutput(
            "div",
            [],
            getChildContentAsync: (_, _) => Task.FromResult<TagHelperContent>(result: new DefaultTagHelperContent()))
        {
            TagMode = TagMode.StartTagAndEndTag
        };

        tagHelper.Process(tagHelperContext, output);
        using var writer = new StringWriter();
        output.WriteTo(writer, HtmlEncoder.Default);

        Assert.That(writer.ToString(), Is.EqualTo("<div id=\"test-id\" class=\"test class\"></div>"));
    }
}