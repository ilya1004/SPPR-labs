using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WEB_253501_Rabets.UI.TagHelpers;

[HtmlTargetElement("Pager", TagStructure = TagStructure.WithoutEndTag)]
public class PagerTagHelper : TagHelper
{
    public string CssClass { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public string Category { get; set; }
    public bool Admin { get; set; }
    private readonly LinkGenerator _linkGenerator;
    public PagerTagHelper(LinkGenerator linkGenerator)
    {
        _linkGenerator = linkGenerator;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {

        var ul = new TagBuilder("ul");
        ul.AddCssClass("pagination");


        var prevPage = CurrentPage > 1 ? CurrentPage - 1 : 1;
        var prevLi = new TagBuilder("li");
        prevLi.AddCssClass($"page-item {(CurrentPage == 1 ? "disabled" : "")}");
        var prevLink = new TagBuilder("a");
        prevLink.AddCssClass("page-link");
        prevLink.Attributes.Add("data-page", (CurrentPage - 1).ToString());
        prevLink.Attributes.Add("data-category", Category);
        prevLink.Attributes.Add("aria-label", "Previous");

        
        prevLink.Attributes.Add("href", _linkGenerator.GetPathByAction(
            action: "Index", 
            controller: "Product",
            values: new { category = Category, page = prevPage }
        ));

        prevLink.InnerHtml.AppendHtml("<span aria-hidden=\"true\">&laquo;</span>");
        prevLi.InnerHtml.AppendHtml(prevLink);
        ul.InnerHtml.AppendHtml(prevLi);

        
        for (int i = 0; i < TotalPages; i++)
        {
            var pageLi = new TagBuilder("li");
            pageLi.AddCssClass($"page-item {(CurrentPage == i + 1 ? "active" : "")}");
            var pageLink = new TagBuilder("a");
            pageLink.AddCssClass("page-link");

            pageLink.Attributes.Add("data-page", $"{i + 1}");
            pageLink.Attributes.Add("data-category", Category);


            pageLink.Attributes.Add("href", _linkGenerator.GetPathByAction(
                action: "Index",
                controller: "Product",
                values: new { category = Category, page = i + 1 }
            ));

            pageLink.InnerHtml.Append((i + 1).ToString());
            pageLi.InnerHtml.AppendHtml(pageLink);
            ul.InnerHtml.AppendHtml(pageLi);
        }

        var nextPage = CurrentPage < TotalPages ? CurrentPage + 1 : TotalPages;
        var nextLi = new TagBuilder("li");
        nextLi.AddCssClass($"page-item {(CurrentPage == TotalPages ? "disabled" : "")}");
        var nextLink = new TagBuilder("a");
        nextLink.AddCssClass("page-link");

        nextLink.Attributes.Add("data-page", (CurrentPage + 1).ToString());
        nextLink.Attributes.Add("data-category", Category);
        nextLink.Attributes.Add("aria-label", "Next");


        nextLink.Attributes.Add("href", _linkGenerator.GetPathByAction(
            action: "Index", 
            controller: "Product",
            values: new { category = Category, page = nextPage }
        ));

        nextLink.InnerHtml.AppendHtml("<span aria-hidden=\"true\">&raquo;</span>");
        nextLi.InnerHtml.AppendHtml(nextLink);
        ul.InnerHtml.AppendHtml(nextLi);

        //tagBuilderUl.InnerHtml.AppendHtml(ul);

        output.TagName = "nav";
        output.Attributes.Add("aria-label", "Page navigation");
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Content.SetHtmlContent(ul);
        
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        await base.ProcessAsync(context, output);
    }
}
