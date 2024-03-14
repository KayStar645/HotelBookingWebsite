using Core.Application.Responses;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Services;

namespace Core.Application.Interfaces
{
    public interface IServiceService
    {
        Task<PaginatedResult<ServiceVM>> List(BaseListRQ pRequest);

        Task<ServiceVM> Detail(int pId);

        Task<ServiceVM> Create(ServiceRQ pRequest);

        Task<ServiceVM> Update(ServiceRQ pRequest);

        Task<bool> Delete(int pId);
    }
}
