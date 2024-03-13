using Core.Application.ViewModels.Auth;

namespace Core.Application.Interfaces.Auth
{
	public interface IPermissionService
	{
        Task<List<PermissionVM>> Create(List<PermissionRQ> pPermissions);

        Task<List<PermissionVM>> Get();
    }
}
