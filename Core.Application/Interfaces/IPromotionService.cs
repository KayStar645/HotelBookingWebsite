using Core.Application.Responses;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Promotions;
using Core.Application.ViewModels.Rooms;

namespace Core.Application.Interfaces
{
	public interface IPromotionService
	{
		Task<PaginatedResult<PromotionVM>> List(BaseListRQ pRequest);

		Task<List<RoomVM>> ListRoomByPromotionId(int pId);

		Task<PromotionVM> Detail(int pId);

		Task<PromotionVM> Create(PromotionRQ pRequest);

		Task<PromotionVM> Update(PromotionRQ pRequest);

		Task<bool> Delete(int pId);

		Task<bool> Approve(int pId, string pStatus);
	}
}