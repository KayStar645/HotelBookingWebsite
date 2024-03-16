using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.Interfaces.Common;
using Core.Application.Services.Common;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Tour;
using Core.Domain.Entities;

namespace Core.Application.Services
{
    public class TourService : BaseService<Tour, TourRQ, TourVM, BaseListRQ>, ITourService
    {
        public TourService(IHotelBookingWebsiteDbContext pContext, IMapper pMapper) :
            base(pContext, pMapper)
        { }

        protected override IQueryable<Tour> ApplySearch(IQueryable<Tour> query, string keyword)
        {
            query = query.Where(x =>
                x.InternalCode.ToLower().Contains(keyword) ||
                x.Name.ToLower().Contains(keyword));

            return query.AsQueryable();
        }
    }

}
