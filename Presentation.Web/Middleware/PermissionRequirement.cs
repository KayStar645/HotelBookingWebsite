using Core.Application.Common;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Web.Middleware
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }

    public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimCommon.Permission && c.Value == requirement.Permission))
            {
                context.Succeed(requirement);
            }

			return Task.CompletedTask;
        }
    }
}
