using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Application.Interfaces.Auth;
using Core.Application.Interfaces.Common;
using Core.Application.Responses;
using Core.Application.Services.Extensions;
using Core.Application.ViewModels.Auth;
using Core.Application.ViewModels.Common;
using Core.Domain.Auth;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Core.Application.Services.Auth
{
    public class UserService : IUserService
    {
        private readonly IHotelBookingWebsiteDbContext _context;
        private readonly IMapper _mapper;

        public UserService(IHotelBookingWebsiteDbContext pContext, IMapper pMapper)
        {
            _context = pContext;
            _mapper = pMapper;
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
    }
}
