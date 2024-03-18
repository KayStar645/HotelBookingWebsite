using Core.Application.Responses;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Staffs;

namespace Core.Application.Interfaces
{
    public interface IStaffService
    {
        Task<PaginatedResult<StaffVM>> List(BaseListRQ pRequest);

        Task<StaffVM> Detail(int pId);

		Task<StaffVM> DetailByInternalCode(string? pInternalCode);

		Task<StaffVM> Create(StaffRQ pRequest);

        Task<StaffVM> Update(StaffRQ pRequest);

        Task<bool> Delete(int pId);
    }
}
