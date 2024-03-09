using Core.Application.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Room
{
	public class RoomRQ : BaseRQ
	{
		[Required(ErrorMessage = "Mã phòng là trường bắt buộc.")]
		public string? InternalCode { get; set; }

		[Required(ErrorMessage = "Loại phòng là trường bắt buộc.")]
		public int? KindRoomId { get; set; }

		public string? Status { get; set; }
	}
}
