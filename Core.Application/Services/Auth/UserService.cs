using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Common;
using Core.Application.Interfaces.Auth;
using Core.Application.Interfaces.Common;
using Core.Application.Responses;
using Core.Application.Services.Extensions;
using Core.Application.ViewModels.Auth;
using Core.Application.ViewModels.Common;
using Core.Domain.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Core.Application.Services.Auth
{
	public class UserService : IUserService
    {
        private readonly IHotelBookingWebsiteDbContext _context;
        private readonly IMapper _mapper;
		private readonly IPasswordHasher<User> _passwordHasher;

		public UserService(IHotelBookingWebsiteDbContext pContext, IMapper pMapper,
            IPasswordHasher<User> pPasswordHasher)
        {
            _context = pContext;
            _mapper = pMapper;
            _passwordHasher = pPasswordHasher;
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

		public async Task<UserVM> Create(UserRQ pRequest)
		{
			try
			{
				var user = _mapper.Map<User>(pRequest);
				user.Password = _passwordHasher.HashPassword(user, pRequest.Password);
				user.Type = ClaimValue.TYPE_SUPER_ADMIN;
				var newUser = await _context.Users.AddAsync(user);
				await _context.SaveChangesAsync(default(CancellationToken));

                if(pRequest.RoleIds != null)
                {
					foreach (var roleId in pRequest.RoleIds)
					{
                        var userRole = new UserRole
                        {
                            UserId= user.Id,
                            RoleId = roleId
                        };
						var newUserRole = await _context.UserRoles.AddAsync(userRole);
						await _context.SaveChangesAsync(default(CancellationToken));
					}
				}
                return _mapper.Map<UserVM>(user);
            }
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<PaginatedResult<UserVM>> List(BaseListRQ pRequest)
        {
            var query = _context.Users.AsQueryable();

            if (pRequest.Search != null)
            {
                var keyword = ValidateExtensions.RemoveDiacritics(pRequest.Search.ToLower());
                query = query.Where(x =>
                            x.UserName.ToLower().Contains(keyword) ||
                            x.Email.ToLower().Contains(keyword) ||
                            x.PhoneNumber.ToLower().Contains(keyword));
            }

            if (pRequest.Filters != null)
            {
                query = QueryableExtensions.ApplyFilters(query, pRequest.Filters);
            }

            if (pRequest.Sorts != null)
            {
                query = QueryableExtensions.ApplySorting(query, pRequest.Sorts);
            }
            var totalItems = await query.CountAsync();

            query = query.Include(x => x.UserRoles).ThenInclude(x => x.Role)
                         .Include(x => x.UserPermissions).ThenInclude(x => x.Permission)
						 .Include(x => x.Staff);

            var entityResult = await query
				        .ProjectTo<UserVM>(_mapper.ConfigurationProvider)
						.Skip(((int)pRequest.Page - 1) * (int)pRequest.PageSize)
                        .Take((int)pRequest.PageSize)
                        .ToListAsync();
            var viewModels = _mapper.Map<List<UserVM>>(entityResult);

            return new PaginatedResult<UserVM>(viewModels, totalItems, pRequest.Page, pRequest.PageSize);
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

		public async Task<UserVM> Update(UserRQ pRequest)
		{
			try
			{
				var user = _mapper.Map<User>(pRequest);
                if(pRequest.Password.Length <= 50)
                {
				    user.Password = _passwordHasher.HashPassword(user, pRequest.Password);
                }
				var newUser = _context.Users.Update(user);
                user.Type = ClaimValue.TYPE_SUPER_ADMIN;
				await _context.SaveChangesAsync(default(CancellationToken));

				//var roleIdsOfUser = await _context.UserRoles.Select(x => x.RoleId).ToListAsync();
				//foreach (var roleId in roleIdsOfUser)
				//{
    //                var userRole = await _context.UserRoles
    //                    .FirstOrDefaultAsync(x => x.UserId == user.Id && x.RoleId == roleId);

				//	_context.UserRoles.Remove(userRole);
				//	await _context.SaveChangesAsync(default(CancellationToken));
				//}

				//if (pRequest.RoleIds != null)
				//{
				//	foreach (var roleId in pRequest.RoleIds)
				//	{
				//		var userRole = await _context.UserRoles
				//		.FirstOrDefaultAsync(x => x.UserId == user.Id && x.RoleId == roleId);

				//		var newUserRole = await _context.UserRoles.AddAsync(userRole);
				//		await _context.SaveChangesAsync(default(CancellationToken));
				//	}
				//}

				return _mapper.Map<UserVM>(user);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
	}
}
