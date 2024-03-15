using Core.Application.Responses;
using Core.Application.ViewModels.Auth;
using Core.Application.ViewModels.Common;

namespace Core.Application.Interfaces.Auth
{
    public interface IUserService
    {
        Task<PaginatedResult<UserVM>> List(BaseListRQ pRequest);

        Task<List<string>> AssignPermissions(RevokeOrAssignPermissionRQ pRequest);

        Task RevokePermissions(RevokeOrAssignPermissionRQ pRequest);
    }
}
