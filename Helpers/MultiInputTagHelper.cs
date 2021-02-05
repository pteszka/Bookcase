using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookcase.Helpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("multi-input", ParentTag = "group")]
    public class MultiInputTagHelper : TagHelper
    {
        public ModelExpression AspFor { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "input";
            output.Attributes.Add("class", "form-control");
            string name = this.AspFor.Name;
            if (!String.IsNullOrEmpty(name))
            {
                output.Attributes.Add("name", name);
            }
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
/*                < input asp -for= "Name" class= "form-control" />
*/