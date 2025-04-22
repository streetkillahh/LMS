using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using LMS.Data;

namespace LMS.Filters
{
    public class AuthorizeRoleAttribute : ActionFilterAttribute
    {
        private readonly string[] _roles;

        public AuthorizeRoleAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.Request.Cookies["UserId"];
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            var dbContext = context.HttpContext.RequestServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
            var user = dbContext?.ApplicationUsers.Find(int.Parse(userId));
            if (user == null || !_roles.Contains(user.Role))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}