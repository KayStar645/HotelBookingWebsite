using Core.Application.Responses;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Rooms;

namespace Core.Application.Interfaces
{
	public interface IRoomService
	{
		Task<PaginatedResult<RoomVM>> List(BaseListRQ pRequest);

		Task<RoomVM> Detail(int pId);

		Task<RoomVM> Create(RoomRQ pRequest);

		Task<RoomVM> Update(RoomRQ pRequest);

		Task<bool> Delete(int pId);
	}
}
