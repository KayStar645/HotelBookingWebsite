using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.Interfaces.Common;
using Core.Application.Services.Common;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Customers;
using Core.Domain.Entities;

namespace Core.Application.Services
{
    public class CustomerService : BaseService<Customer, CustomerRQ, CustomerVM, BaseListRQ>, ICustomerService
    {
        public CustomerService(IHotelBookingWebsiteDbContext pContext, IMapper pMapper) : base(pContext, pMapper)
        { }

        protected override IQueryable<Customer> ApplySearch(IQueryable<Customer> query, string keyword)
        {
            query = query.Where(x =>
                x.Name.ToLower().Contains(keyword) ||
                x.Email.ToLower().Contains(keyword) ||
                x.Address.ToLower().Contains(keyword) ||
                x.Phone.ToLower().Contains(keyword));

            return query.AsQueryable();
        }
    }
}
