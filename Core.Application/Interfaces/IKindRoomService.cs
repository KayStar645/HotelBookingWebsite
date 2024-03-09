using Core.Application.Responses;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.KindRooms;

namespace Core.Application.Interfaces
{
	public interface IKindRoomService
	{
		Task<PaginatedResult<KindRoomVM>> List(BaseListRQ pRequest);

		Task<KindRoomVM> Detail(int pId);

		Task<KindRoomVM> Create(KindRoomRQ pRequest);

		Task<KindRoomVM> Update(KindRoomRQ pRequest);

		Task<bool> Delete(int pId);
	}
}
