using AutoMapper;
using Core.Application.Interfaces.Auth;
using Core.Application.Interfaces.Common;
using Core.Application.ViewModels.Auth;
using Core.Domain.Auth;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Core.Application.Services.Auth
{
    public class RoleService : IRoleService
	{
        private readonly IHotelBookingWebsiteDbContext _context;

        private readonly IMapper _mapper;

        public RoleService(IHotelBookingWebsiteDbContext pContext, IMapper pMapper)
        {
            _context = pContext;
            _mapper = pMapper;
        }

        public async Task<List<string>> AssignRoles(AssignRoleRQ pRequest)
        {
            var currentRoles = await _context.UserRoles
                                                .Where(x => x.UserId == pRequest.UserId)
                                                .Select(x => x.RoleId)
                                                .ToListAsync();

            var deleteRole = currentRoles.Cast<int>().Except(pRequest.RolesId).ToList();
            var addRole = pRequest.RolesId.Except(currentRoles.Cast<int>()).ToList();

            foreach (var role in deleteRole)
            {
                var result = await _context.UserRoles
                            .FirstOrDefaultAsync(x => x.UserId == pRequest.UserId && x.RoleId == role);
                _context.UserRoles.Remove(result);
            }

            foreach (var role in addRole)
            {
                await _context.UserRoles.AddAsync(new UserRole
                {
                    UserId = pRequest.UserId,
                    RoleId = role
                });
            }
            await _context.SaveChangesAsync(default(CancellationToken));

            List<string> permission = new List<string>();
            foreach (var role in pRequest.RolesId)
            {
                var per = await _context.RolePermissions
                                           .Include(x => x.Permission)
                                           .Where(x => x.RoleId == role)
                                           .Select(x => x.Permission.Name)
                                           .ToListAsync();
                permission = permission.Union(per).ToList();
            }

            return permission;
        }

        public async Task<RoleVM> CreateAsync(RoleRQ pRequest)
        {
            try
            {
                var role = _mapper.Map<Role>(pRequest);

                var newRole = await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync(default(CancellationToken));
                List<string> permissions = new List<string>();

                if (pRequest.PermissionsName != null)
                {
                    foreach (string permissionName in pRequest.PermissionsName)
                    {
                        var permission = await _context.Permissions
                                    .FirstOrDefaultAsync(x => x.Name == permissionName);

                        if (permission != null)
                        {
                            var rolePermission = new RolePermission
                            {
                                RoleId = role.Id,
                                PermissionId = permission.Id
                            };
                            await _context.RolePermissions.AddAsync(rolePermission);
                            permissions.Add(permission.Name);
                        }
                    }
                    await _context.SaveChangesAsync(default(CancellationToken));
                }

                var roleResponses = _mapper.Map<RoleVM>(newRole.Entity);
                roleResponses.PermissionsName = permissions;
                return roleResponses;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RoleVM> UpdateAsync(RoleRQ pRequest)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var role = _mapper.Map<Role>(pRequest);
                    var updateRole = _context.Roles.Update(role);
                    await _context.SaveChangesAsync(default(CancellationToken));
                    List<string> permissions = new List<string>();

                    // Get Permission hiện của của Role này
                    var currentPermissionsName = await _context.RolePermissions
                                                        .Include(x => x.Permission)
                                                        .Where(x => x.RoleId == pRequest.Id)
                                                        .Select(x => x.Permission.Name)
                                                        .ToListAsync();

                    if (pRequest.PermissionsName != null)
                    {
                        var deletePermission = currentPermissionsName.Except(pRequest.PermissionsName).ToList();
                        var addPermission = pRequest.PermissionsName.Except(currentPermissionsName).ToList();
                        var sharePermission = pRequest.PermissionsName.Intersect(currentPermissionsName).ToList();

                        foreach (string permissionName in deletePermission)
                        {
                            var delete = await _context.RolePermissions
                                        .Include(x => x.Permission)
                                        .Where(x => x.RoleId == role.Id && x.Permission.Name == permissionName)
                                        .FirstOrDefaultAsync();
                            if (delete != null)
                            {
                                _context.RolePermissions.Remove(delete);
                            }
                        }

                        foreach (string permissionName in addPermission)
                        {
                            var permission = await _context.Permissions
                                                 .FirstOrDefaultAsync(x => x.Name == permissionName);

                            if (permission != null)
                            {

                                var per = new RolePermission
                                {
                                    RoleId = role.Id,
                                    PermissionId = permission.Id
                                };
                                await _context.RolePermissions.AddAsync(per);
                                permissions.Add(permission.Name);
                            }
                        }
                        await _context.SaveChangesAsync(default(CancellationToken));
                        permissions = permissions.Union(sharePermission).ToList();
                    }

                    transaction.Complete();

                    var roleResponses = _mapper.Map<RoleVM>(updateRole.Entity);
                    roleResponses.PermissionsName = permissions;
                    return roleResponses;
                }
                catch (Exception ex)
                {
                    transaction.Dispose();
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
