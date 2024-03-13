using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.Interfaces.Common;
using Core.Application.Services.Common;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Services;
using Core.Domain.Entities;

namespace Core.Application.Services
{
	public class ServiceService : BaseService<Service, ServiceRQ, ServiceVM, BaseListRQ>, IServiceService
	{
		public ServiceService(IHotelBookingWebsiteDbContext pContext, IMapper pMapper) :
			base(pContext, pMapper)
		{ }

		protected override IQueryable<Service> ApplySearch(IQueryable<Service> query, string keyword)
		{
			query = query.Where(x =>
				x.InternalCode.ToLower().Contains(keyword) ||
				x.Name.ToLower().Contains(keyword));

			return query.AsQueryable();
		}
	}

}
