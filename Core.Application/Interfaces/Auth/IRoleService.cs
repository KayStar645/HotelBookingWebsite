using Core.Application.ViewModels.Auth;

namespace Core.Application.Interfaces.Auth
{
	public interface IRoleService
    {
        Task<RoleVM> CreateAsync(RoleRQ pRequest);

        Task<RoleVM> UpdateAsync(RoleRQ pRequest);

        Task<List<string>> AssignRoles(AssignRoleRQ pRequest);
    }
}
