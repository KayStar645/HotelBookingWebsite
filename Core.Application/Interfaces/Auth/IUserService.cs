using Core.Application.ViewModels.Auth;

namespace Core.Application.Interfaces.Auth
{
	public interface IUserService
    {
        Task<List<string>> AssignPermissions(RevokeOrAssignPermissionRQ pRequest);

        Task RevokePermissions(RevokeOrAssignPermissionRQ pRequest);
    }
}
