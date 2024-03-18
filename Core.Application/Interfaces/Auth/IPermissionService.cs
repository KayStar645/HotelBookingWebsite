using Core.Application.ViewModels.Auth;

namespace Core.Application.Interfaces.Auth
{
	public interface IPermissionService
	{
		Task<List<PermissionDto>> PermissionByRole(int? pRoleId = null);

		Task<List<PermissionVM>> Create(List<string> pPermissions);

		Task<List<PermissionVM>> Get();
    }
}
