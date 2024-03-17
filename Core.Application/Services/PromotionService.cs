using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.Interfaces.Common;
using Core.Application.Services.Common;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Promotions;
using Core.Application.ViewModels.Rooms;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Services
{
	public class PromotionService :
		BaseService<Promotion, PromotionRQ, PromotionVM, BaseListRQ>,
		IPromotionService
	{
		public PromotionService(IHotelBookingWebsiteDbContext pContext, IMapper pMapper) :
			base(pContext, pMapper)
		{ }

		public async Task<bool> Approve(int pId, string pStatus)
		{
			bool flag = true;
			var promotion = await _context.Promotions.FindAsync(pId);
			if(promotion.Status == Promotion.STATUS_DARF)
			{
				promotion.Status = Promotion.STATUS_APPROVE;
			}
			else if (promotion.Status == Promotion.STATUS_APPROVE)
			{
				if(!(pStatus == Promotion.STATUS_CANCEL || pStatus == Promotion.STATUS_DARF))
				{
					flag = false;
				}
				promotion.Status = pStatus;
			}
			else if (promotion.Status == Promotion.STATUS_CANCEL)
			{
				flag = false;
			}
			_context.Promotions.Update(promotion);
			await _context.SaveChangesAsync(default(CancellationToken));

			return flag;
		}

		public async Task<List<RoomVM>> ListRoomByPromotionId(int pId)
		{
			var room = await _context.RoomPromotions
				.Where(x => x.PromotionId == pId)
				.Include(x => x.Room)
				.ThenInclude(x => x.KindRoom)
				.Select(x => x.Room)
				.ToListAsync();

			return _mapper.Map<List<RoomVM>>(room);
		}

		protected override IQueryable<Promotion> ApplySearch(IQueryable<Promotion> query, string keyword)
		{
			query = query.Where(x =>
				x.InternalCode.ToLower().Contains(keyword) ||
				x.Name.ToLower().Contains(keyword));

			return query.AsQueryable();
		}
	}
}
