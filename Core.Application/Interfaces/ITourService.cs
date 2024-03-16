using Core.Application.Responses;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Tour;

namespace Core.Application.Interfaces
{
    public interface ITourService
    {
        Task<PaginatedResult<TourVM>> List(BaseListRQ pRequest);

        Task<TourVM> Detail(int pId);

        Task<TourVM> Create(TourRQ pRequest);

        Task<TourVM> Update(TourRQ pRequest);

        Task<bool> Delete(int pId);
    }
}
