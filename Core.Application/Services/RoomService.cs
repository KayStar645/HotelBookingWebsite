using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.Interfaces.Common;
using Core.Application.Services.Common;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Rooms;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Services
{
    public class RoomService : BaseService<Room, RoomRQ, RoomVM, BaseListRQ>, IRoomService
	{
		public RoomService(IHotelBookingWebsiteDbContext pContext, IMapper pMapper) :
			base(pContext, pMapper)
		{ }

		protected override IQueryable<Room> ApplySearch(IQueryable<Room> query, string keyword)
		{
			query = query.Where(x =>
				x.InternalCode.ToLower().Contains(keyword) ||
				x.KindRoom.Name.ToLower().Contains(keyword));

			return query.AsQueryable();
		}

		protected override IQueryable<Room> Query(IQueryable<Room> query)
		{
			query = query.Include(x => x.KindRoom);

			return base.Query(query);
		}
	}

}
