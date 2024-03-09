using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.KindRooms;

namespace Core.Application.ViewModels.Rooms
{
	public class RoomVM : BaseVM
	{
		public string? InternalCode { get; set; }

		public string? Status { get; set; }

		public int? KindRoomId { get; set; }

		public KindRoomVM? KindRoomVM { get; set; }

		public bool? IsDeleted { get; set; }
	}
}
