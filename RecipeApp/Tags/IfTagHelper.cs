using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Text.Encodings.Web;

namespace RecipeApp.Tags
{
    [HtmlTargetElement("if")]
    public class IfTagHelper : TagHelper
    {
        private readonly HtmlEncoder _encoder;
        public IfTagHelper(HtmlEncoder encoder) => _encoder = encoder;
        [HtmlAttributeName("if")]
        public bool RenderContent { get; set; } = true;
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!RenderContent)
            {
                output.TagName = null;
                output.SuppressOutput();
            }
        }
        public override int Order => int.MinValue;
    }
}
