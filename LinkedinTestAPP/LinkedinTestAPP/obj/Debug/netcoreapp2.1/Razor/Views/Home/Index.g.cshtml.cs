#pragma checksum "C:\Users\Andre\source\repos\LinkedinTestAPP\LinkedinTestAPP\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e0ae87f921b46a987d5ad900eb5b3f6b50402318"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\Andre\source\repos\LinkedinTestAPP\LinkedinTestAPP\Views\_ViewImports.cshtml"
using LinkedinTestAPP;

#line default
#line hidden
#line 2 "C:\Users\Andre\source\repos\LinkedinTestAPP\LinkedinTestAPP\Views\_ViewImports.cshtml"
using LinkedinTestAPP.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e0ae87f921b46a987d5ad900eb5b3f6b50402318", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"715794040331482fd31198d09d5231a580f28a03", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\Andre\source\repos\LinkedinTestAPP\LinkedinTestAPP\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
            BeginContext(45, 47, true);
            WriteLiteral("\r\n<br />\r\n<table class=\"table table-striped\">\r\n");
            EndContext();
#line 7 "C:\Users\Andre\source\repos\LinkedinTestAPP\LinkedinTestAPP\Views\Home\Index.cshtml"
     foreach (var item in User.Claims)
    {

#line default
#line hidden
            BeginContext(139, 30, true);
            WriteLiteral("        <tr>\r\n            <td>");
            EndContext();
            BeginContext(170, 9, false);
#line 10 "C:\Users\Andre\source\repos\LinkedinTestAPP\LinkedinTestAPP\Views\Home\Index.cshtml"
           Write(item.Type);

#line default
#line hidden
            EndContext();
            BeginContext(179, 23, true);
            WriteLiteral("</td>\r\n            <td>");
            EndContext();
            BeginContext(203, 10, false);
#line 11 "C:\Users\Andre\source\repos\LinkedinTestAPP\LinkedinTestAPP\Views\Home\Index.cshtml"
           Write(item.Value);

#line default
#line hidden
            EndContext();
            BeginContext(213, 22, true);
            WriteLiteral("</td>\r\n        </tr>\r\n");
            EndContext();
#line 13 "C:\Users\Andre\source\repos\LinkedinTestAPP\LinkedinTestAPP\Views\Home\Index.cshtml"

    }

#line default
#line hidden
            BeginContext(244, 8, true);
            WriteLiteral("</table>");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
