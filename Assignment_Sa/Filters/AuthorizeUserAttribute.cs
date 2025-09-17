using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SalesApp.Filters;

namespace SalesApp.Filters
{
    public class AuthorizeUserAttribute : ActionFilterAttribute
    {
        private readonly string _role;

        public AuthorizeUserAttribute(string role = "")
        {
            _role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;
            var userId = httpContext.Session.GetInt32("UserId");
            var role = httpContext.Session.GetString("Role");

            if (userId == null)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            if (!string.IsNullOrEmpty(_role) && role != _role)
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}

//[AuthorizeUser("Admin")]
//public IActionResult AdminOnlyPage()
//{
//    return View();
//}
