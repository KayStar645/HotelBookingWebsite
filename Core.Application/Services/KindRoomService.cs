using AutoMapper;
using Core.Application.Interfaces.Common;
using Core.Application.Interfaces;
using Core.Application.Services.Common;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.KindRooms;
using Core.Domain.Entities;

namespace Core.Application.Services
{
	public class KindRoomService : BaseService<KindRoom, KindRoomRQ, KindRoomVM, BaseListRQ>, IKindRoomService
	{
		public KindRoomService(IHotelBookingWebsiteDbContext pContext, IMapper pMapper) :
			base(pContext, pMapper)
		{ }

		protected override IQueryable<KindRoom> ApplySearch(IQueryable<KindRoom> query, string keyword)
		{
			query = query.Where(x =>
				x.InternalCode.ToLower().Contains(keyword) ||
				x.Name.ToLower().Contains(keyword));

			return query.AsQueryable();
		}
	}

}
