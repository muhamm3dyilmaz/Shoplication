#pragma checksum "D:\Web Developing\Asp.Net MVC\ShopApp\ShopApp.webui\Views\Shop\List.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cd083aa8e55fac1f0353ea36b58e13040656c853"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shop_List), @"mvc.1.0.view", @"/Views/Shop/List.cshtml")]
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
#nullable restore
#line 2 "D:\Web Developing\Asp.Net MVC\ShopApp\ShopApp.webui\Views\_ViewImports.cshtml"
using ShopApp.Entity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Web Developing\Asp.Net MVC\ShopApp\ShopApp.webui\Views\_ViewImports.cshtml"
using ShopApp.webui.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cd083aa8e55fac1f0353ea36b58e13040656c853", @"/Views/Shop/List.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7574018388da4a8046c0de56dc18f17569caede8", @"/Views/_ViewImports.cshtml")]
    public class Views_Shop_List : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ProductListViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"row\">\r\n\r\n    <div class=\"col-md-3 justify-content-start\">\r\n\r\n        ");
#nullable restore
#line 7 "D:\Web Developing\Asp.Net MVC\ShopApp\ShopApp.webui\Views\Shop\List.cshtml"
   Write(await Component.InvokeAsync("Categories"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n    </div>\r\n\r\n    <div class=\"col-md-9\">\r\n\r\n        <div class=\"row\">\r\n\r\n");
#nullable restore
#line 15 "D:\Web Developing\Asp.Net MVC\ShopApp\ShopApp.webui\Views\Shop\List.cshtml"
             foreach (var product in Model.Products)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"col-sm-12 col-md-6 col-lg-4\">\r\n\r\n                    ");
#nullable restore
#line 19 "D:\Web Developing\Asp.Net MVC\ShopApp\ShopApp.webui\Views\Shop\List.cshtml"
               Write(await Html.PartialAsync("_product",product));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                </div>\r\n");
#nullable restore
#line 22 "D:\Web Developing\Asp.Net MVC\ShopApp\ShopApp.webui\Views\Shop\List.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n\r\n        <div class=\"row\">\r\n\r\n            <div class=\"col\">\r\n\r\n                <nav aria-label=\"Page navigation example\">\r\n\r\n                    <ul class=\"pagination\">\r\n\r\n");
#nullable restore
#line 34 "D:\Web Developing\Asp.Net MVC\ShopApp\ShopApp.webui\Views\Shop\List.cshtml"
                         for (int i = 0; i < Model.PageInfo.TotalPages(); i++)
                        {
                            if (String.IsNullOrEmpty(Model.PageInfo.CurrentCategory))
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <li");
            BeginWriteAttribute("class", " class=\"", 919, "\"", 996, 3);
            WriteAttributeValue("", 927, "active-btn", 927, 10, true);
            WriteAttributeValue(" ", 937, "page-item", 938, 10, true);
#nullable restore
#line 38 "D:\Web Developing\Asp.Net MVC\ShopApp\ShopApp.webui\Views\Shop\List.cshtml"
WriteAttributeValue(" ", 947, Model.PageInfo.CurrentPage == i+1?"active":"", 948, 48, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    <a class=\"page-link btn-green\"");
            BeginWriteAttribute("href", " href=\"", 1066, "\"", 1094, 2);
            WriteAttributeValue("", 1073, "/products?page=", 1073, 15, true);
#nullable restore
#line 39 "D:\Web Developing\Asp.Net MVC\ShopApp\ShopApp.webui\Views\Shop\List.cshtml"
WriteAttributeValue("", 1088, i+1, 1088, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 39 "D:\Web Developing\Asp.Net MVC\ShopApp\ShopApp.webui\Views\Shop\List.cshtml"
                                                                                            Write(i+1);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                                </li>\r\n");
#nullable restore
#line 41 "D:\Web Developing\Asp.Net MVC\ShopApp\ShopApp.webui\Views\Shop\List.cshtml"
                            }

                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <li");
            BeginWriteAttribute("class", " class=\"", 1280, "\"", 1357, 3);
            WriteAttributeValue("", 1288, "active-btn", 1288, 10, true);
            WriteAttributeValue(" ", 1298, "page-item", 1299, 10, true);
#nullable restore
#line 45 "D:\Web Developing\Asp.Net MVC\ShopApp\ShopApp.webui\Views\Shop\List.cshtml"
WriteAttributeValue(" ", 1308, Model.PageInfo.CurrentPage == i+1?"active":"", 1309, 48, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    <a class=\"page-link btn-green\"");
            BeginWriteAttribute("href", " href=\"", 1427, "\"", 1487, 4);
            WriteAttributeValue("", 1434, "/products/", 1434, 10, true);
#nullable restore
#line 46 "D:\Web Developing\Asp.Net MVC\ShopApp\ShopApp.webui\Views\Shop\List.cshtml"
WriteAttributeValue("", 1444, Model.PageInfo.CurrentCategory, 1444, 31, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1475, "?page=", 1475, 6, true);
#nullable restore
#line 46 "D:\Web Developing\Asp.Net MVC\ShopApp\ShopApp.webui\Views\Shop\List.cshtml"
WriteAttributeValue("", 1481, i+1, 1481, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 46 "D:\Web Developing\Asp.Net MVC\ShopApp\ShopApp.webui\Views\Shop\List.cshtml"
                                                                                                                            Write(i+1);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\r\n                                </li>\r\n");
#nullable restore
#line 48 "D:\Web Developing\Asp.Net MVC\ShopApp\ShopApp.webui\Views\Shop\List.cshtml"
                            }
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </ul>\r\n\r\n                </nav>\r\n\r\n            </div>\r\n\r\n        </div>\r\n\r\n    </div>\r\n\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script src=""https://code.jquery.com/jquery-3.4.1.slim.min.js"" integrity=""sha384-J6qa4849blE2+poT4WnyKhv5vZF5SrPo0iEjwBvKU7imGFAV0wwj1yYfoRSJoZ+n"" crossorigin=""anonymous""></script>
    <script src=""https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"" integrity=""sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo"" crossorigin=""anonymous""></script>
    <script src=""https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"" integrity=""sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6"" crossorigin=""anonymous""></script>
");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ProductListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
