using Core.Application.Responses;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Customers;

namespace Core.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<PaginatedResult<CustomerVM>> List(BaseListRQ pRequest);

        Task<CustomerVM> Detail(int pId);

        Task<CustomerVM> Create(CustomerRQ pRequest);

        Task<CustomerVM> Update(CustomerRQ pRequest);

        Task<bool> Delete(int pId);
    }
}
