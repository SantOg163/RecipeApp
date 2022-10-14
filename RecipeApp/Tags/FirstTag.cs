using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Text.Encodings.Web;

namespace RecipeApp.Tags
{
    public class FirstTag : TagHelper
    {
        private readonly HtmlEncoder _encoder;
        public FirstTag(HtmlEncoder encoder) => _encoder = encoder;
        [HtmlAttributeName("add-name")]
        public bool IncludeName { get; set; } = true;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            var sb = new StringBuilder();
            if(IncludeName)
            {
                sb.Append("Name");
            }
            output.Content.AppendHtml(sb.ToString());
        }
    }
}
