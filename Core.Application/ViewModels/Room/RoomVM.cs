using Core.Application.ViewModels.Common;

namespace Core.Application.ViewModels.Room
{
	public class RoomVM : BaseVM
	{
		public string? InternalCode { get; set; }

		public string? Status { get; set; }

		public int? KindRoomId { get; set; }

		public bool? IsDeleted { get; set; }
	}
}
