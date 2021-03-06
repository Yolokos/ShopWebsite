#pragma checksum "C:\Users\guts\source\repos\ShopWebApp\BookStoreWebApi\Views\Account\Orders.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ad06ea448d6baea0b50dd6ea2088f06f2f15de17"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Orders), @"mvc.1.0.view", @"/Views/Account/Orders.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Account/Orders.cshtml", typeof(AspNetCore.Views_Account_Orders))]
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
#line 1 "C:\Users\guts\source\repos\ShopWebApp\BookStoreWebApi\Views\_ViewImports.cshtml"
using BookStoreWebApi;

#line default
#line hidden
#line 2 "C:\Users\guts\source\repos\ShopWebApp\BookStoreWebApi\Views\_ViewImports.cshtml"
using BookStoreWebApi.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ad06ea448d6baea0b50dd6ea2088f06f2f15de17", @"/Views/Account/Orders.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5949197833603d2e5bb35192a2faee749a9e3516", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_Orders : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<BookStoreWebApi.Models.Order>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(51, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\guts\source\repos\ShopWebApp\BookStoreWebApi\Views\Account\Orders.cshtml"
  
    ViewData["Title"] = "Orders";

#line default
#line hidden
            BeginContext(95, 314, true);
            WriteLiteral(@"
<h2>Ваши заказы</h2>

<table class=""table table-striped"">
    <tr>
        <td><a>Номер заказа</a></td>

        <td><a>Дата оформление заказа</a></td>

        <td><a>Дата доставки</a></td>

        <td><a>Цена</a></td>

        <td><a>Книжки</a></td>

        <td><a>Коль-во</a></td>
    </tr>
");
            EndContext();
#line 23 "C:\Users\guts\source\repos\ShopWebApp\BookStoreWebApi\Views\Account\Orders.cshtml"
     foreach (var order in Model)
    {

#line default
#line hidden
            BeginContext(451, 30, true);
            WriteLiteral("        <tr>\r\n            <td>");
            EndContext();
            BeginContext(482, 13, false);
#line 26 "C:\Users\guts\source\repos\ShopWebApp\BookStoreWebApi\Views\Account\Orders.cshtml"
           Write(order.OrderId);

#line default
#line hidden
            EndContext();
            BeginContext(495, 23, true);
            WriteLiteral("</td>\r\n            <td>");
            EndContext();
            BeginContext(519, 15, false);
#line 27 "C:\Users\guts\source\repos\ShopWebApp\BookStoreWebApi\Views\Account\Orders.cshtml"
           Write(order.DateOrder);

#line default
#line hidden
            EndContext();
            BeginContext(534, 23, true);
            WriteLiteral("</td>\r\n            <td>");
            EndContext();
            BeginContext(558, 17, false);
#line 28 "C:\Users\guts\source\repos\ShopWebApp\BookStoreWebApi\Views\Account\Orders.cshtml"
           Write(order.DateDeliver);

#line default
#line hidden
            EndContext();
            BeginContext(575, 23, true);
            WriteLiteral("</td>\r\n            <td>");
            EndContext();
            BeginContext(599, 18, false);
#line 29 "C:\Users\guts\source\repos\ShopWebApp\BookStoreWebApi\Views\Account\Orders.cshtml"
           Write(order.DeliverPrice);

#line default
#line hidden
            EndContext();
            BeginContext(617, 25, true);
            WriteLiteral("</td>\r\n            <td>\r\n");
            EndContext();
#line 31 "C:\Users\guts\source\repos\ShopWebApp\BookStoreWebApi\Views\Account\Orders.cshtml"
                 foreach (var book in order.OrderBooks.Select(p => p.Book).ToList())
                {
                    

#line default
#line hidden
            BeginContext(768, 15, false);
#line 33 "C:\Users\guts\source\repos\ShopWebApp\BookStoreWebApi\Views\Account\Orders.cshtml"
               Write(book.NameOfBook);

#line default
#line hidden
            EndContext();
#line 33 "C:\Users\guts\source\repos\ShopWebApp\BookStoreWebApi\Views\Account\Orders.cshtml"
                                    
                    
                }

#line default
#line hidden
            BeginContext(826, 37, true);
            WriteLiteral("            </td>\r\n            <td>\r\n");
            EndContext();
#line 38 "C:\Users\guts\source\repos\ShopWebApp\BookStoreWebApi\Views\Account\Orders.cshtml"
                 foreach(var count in order.OrderBooks.Select(p => p.CountCopy).ToList())
                {
                    

#line default
#line hidden
            BeginContext(994, 5, false);
#line 40 "C:\Users\guts\source\repos\ShopWebApp\BookStoreWebApi\Views\Account\Orders.cshtml"
               Write(count);

#line default
#line hidden
            EndContext();
#line 40 "C:\Users\guts\source\repos\ShopWebApp\BookStoreWebApi\Views\Account\Orders.cshtml"
                          
                }

#line default
#line hidden
            BeginContext(1020, 34, true);
            WriteLiteral("            </td>\r\n        </tr>\r\n");
            EndContext();
#line 44 "C:\Users\guts\source\repos\ShopWebApp\BookStoreWebApi\Views\Account\Orders.cshtml"
    }

#line default
#line hidden
            BeginContext(1061, 10, true);
            WriteLiteral("</table>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<BookStoreWebApi.Models.Order>> Html { get; private set; }
    }
}
#pragma warning restore 1591
