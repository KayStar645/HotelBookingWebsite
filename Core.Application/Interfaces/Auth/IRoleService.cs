using Core.Application.Responses;
using Core.Application.ViewModels.Auth;
using Core.Application.ViewModels.Common;

namespace Core.Application.Interfaces.Auth
{
	public interface IRoleService
	{
		Task<PaginatedResult<RoleVM>> List(BaseListRQ pRequest);

		Task<List<RoleVM>> RoleByUserId(int pUserId);

		Task<RoleVM> CreateAsync(RoleRQ pRequest);

        Task<RoleVM> UpdateAsync(RoleRQ pRequest);

        Task<List<string>> AssignRoles(AssignRoleRQ pRequest);
    }
}
