using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWebApi.FIlters
{
    public class AfterAuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool auth = context.HttpContext.User.Identity.IsAuthenticated;

            if (auth)
            {
                context.Result = new NotFoundResult();
                    //new RedirectToRouteResult(new RedirectResult ("/Home/Index"));
            }
        }
    }
}
