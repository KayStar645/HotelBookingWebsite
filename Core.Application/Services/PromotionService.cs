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

		protected override IQueryable<Promotion> ApplySearch(IQueryable<Promotion> query, string keyword)
		{
			query = query.Where(x =>
				x.InternalCode.ToLower().Contains(keyword) ||
				x.Name.ToLower().Contains(keyword));

			return query.AsQueryable();
		}
	}
}
