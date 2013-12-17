using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace IndiTownUI.Internal.Attributes
{
    public class AuthorizeUserAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            return httpContext.User.Identity.IsAuthenticated;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.HttpContext.User.Identity.IsAuthenticated && filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new ViewResult { ViewName = "SignIn" };
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/User/SignIn");
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}