using Microsoft.AspNetCore.Authorization;

namespace Presentation.Web.Middleware
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PermissionAttribute : AuthorizeAttribute
    {
        public PermissionAttribute(string permission) : base("PermissionPolicy")
        {
            Policy = $"{permission}";
        }
    }

}
