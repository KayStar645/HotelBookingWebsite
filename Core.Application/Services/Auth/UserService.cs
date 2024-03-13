using Core.Application.Interfaces.Auth;
using Core.Application.Interfaces.Common;
using Core.Application.ViewModels.Auth;
using Core.Domain.Auth;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Services.Auth
{
    public class UserService : IUserService
    {
        private readonly IHotelBookingWebsiteDbContext _context;

        public UserService(IHotelBookingWebsiteDbContext pContext)
        {
            _context = pContext;
        }

        public async Task<List<string>> AssignPermissions(RevokeOrAssignPermissionRQ pRequest)
        {
            foreach (var permissionName in pRequest.Permissions)
            {
                var permission = await _context.Permissions.FirstOrDefaultAsync(x => x.Name == permissionName);

                var hasUP = await _context.UserPermissions
                                    .Include(x => x.Permission)
                                    .FirstOrDefaultAsync(x => x.UserId == pRequest.UserId &&
                                                   x.Permission.Name == permissionName);
                if (hasUP == null)
                {
                    await _context.UserPermissions.AddAsync(new UserPermission
                    {
                        UserId = pRequest.UserId,
                        PermissionId = permission.Id
                    });
                }
            }
            await _context.SaveChangesAsync(default(CancellationToken));

            return pRequest.Permissions;
        }

        public async Task RevokePermissions(RevokeOrAssignPermissionRQ pRequest)
        {
            foreach (var permissionName in pRequest.Permissions)
            {
                var hasUP = await _context.UserPermissions
                                    .Include(x => x.Permission)
                                    .FirstOrDefaultAsync(x => x.UserId == pRequest.UserId &&
                                                   x.Permission.Name == permissionName);
                if (hasUP != null)
                {
                    _context.UserPermissions.Remove(hasUP);
                }
            }
            await _context.SaveChangesAsync(default(CancellationToken));
        }
    }
}
