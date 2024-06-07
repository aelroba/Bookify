using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Bookify.web.Helpers
{
    [HtmlTargetElement("a", Attributes = "active")]
    public class ActiveHeaderTag : TagHelper
    {
        public string? Active { get; set; }
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext? ViewContextData { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            if(string.IsNullOrEmpty(Active)) return;
            var currentController = ViewContextData?.RouteData.Values["controller"]?.ToString();
            if(currentController!.Equals(Active)) {
                if(output.Attributes.ContainsName("class")) {
                    output.Attributes.SetAttribute("class", output.Attributes["class"].Value + " here" );
                } else {
                    output.Attributes.SetAttribute("class", "here");
                }
            }
        }
    }
}