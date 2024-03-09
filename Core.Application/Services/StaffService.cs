using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.Services.Common;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Staffs;
using Core.Domain.Entities;

namespace Core.Application.Services
{
	public class StaffService : BaseService<Staff, StaffRQ, StaffVM, BaseListRQ>, IStaffService
	{
		public StaffService(IHotelBookingWebsiteDbContext pContext, IMapper pMapper) :
			base(pContext, pMapper) { }

		protected override IQueryable<Staff> ApplySearch(IQueryable<Staff> query, string keyword)
		{
			query = query.Where(x =>
				x.InternalCode.ToLower().Contains(keyword) ||
				x.Name.ToLower().Contains(keyword) ||
				x.Address.ToLower().Contains(keyword) ||
				x.Phone.ToLower().Contains(keyword));

			return query.AsQueryable();
		}
	}

}
