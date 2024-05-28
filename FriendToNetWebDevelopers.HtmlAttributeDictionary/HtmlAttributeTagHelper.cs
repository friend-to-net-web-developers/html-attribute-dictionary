using Microsoft.AspNetCore.Razor.TagHelpers;

namespace FriendToNetWebDevelopers.HtmlAttributeDictionary
{
    /// <summary>
    /// Allow the html-attributes attribute to be used on any html tag
    /// </summary>
    [HtmlTargetElement(Attributes = "html-attributes", ParentTag = null, TagStructure = TagStructure.NormalOrSelfClosing)]
    public class HtmlAttributeTagHelper : TagHelper
    {
        [HtmlAttributeName("html-attributes")]
        public Dictionary<string, string> HtmlAttributes { get; set; } = [];

        public override void Process(TagHelperContext context,
            TagHelperOutput output)
        {
            foreach (var (key, value) in HtmlAttributes)
            {
                output.Attributes.SetAttribute(key, value);
            }
        }
    }
}
