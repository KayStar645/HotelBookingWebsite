using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.Interfaces.Common;
using Core.Application.Services.Common;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Promotions;
using Core.Domain.Entities;

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

		protected override IQueryable<Promotion> ApplySearch(IQueryable<Promotion> query, string keyword)
		{
			query = query.Where(x =>
				x.InternalCode.ToLower().Contains(keyword) ||
				x.Name.ToLower().Contains(keyword));

			return query.AsQueryable();
		}
	}
}
