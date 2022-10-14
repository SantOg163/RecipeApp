using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.Encodings.Web;

namespace RecipeApp.Tags
{
    public class MarkdownTagHelper : TagHelper
    {
        private HtmlEncoder _encoder;
        public MarkdownTagHelper(HtmlEncoder encoder)=>_encoder = encoder;
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            TagHelperContent markDownRazorContent = await output.GetChildContentAsync(NullHtmlEncoder.Default);
            string markDown = markDownRazorContent.GetContent(NullHtmlEncoder.Default);
            string html=Markdig.Markdown.ToHtml(markDown);
            output.Content.SetHtmlContent(html);
            output.TagName = null;
        }
    }
}
