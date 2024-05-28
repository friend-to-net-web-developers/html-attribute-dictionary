using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FriendToNetWebDevelopers.HtmlAttributeDictionary.Test
{
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
                { "html-attributes", tagHelper.HtmlAttributes },
            };

            var tagHelperContext = new TagHelperContext(attributes, new Dictionary<object, object>(), nameof(HtmlAttributeTagHelper));

            var output = new TagHelperOutput(
                "div",
                [],
                getChildContentAsync: (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(result: null))
            {
                TagMode = TagMode.StartTagAndEndTag,
            };

            tagHelper.Process(tagHelperContext, output);
            using var writer = new StringWriter();
            output.WriteTo(writer, HtmlEncoder.Default);

            Assert.That(writer.ToString(), Is.EqualTo("<div id=\"test-id\" class=\"test class\"></div>"));
        }
    }
}
