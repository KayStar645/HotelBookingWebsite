using Core.Application.ViewModels.Common;
using Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Rooms
{
	public class RoomRQ : BaseRQ
	{
		[Required(ErrorMessage = "Mã phòng là trường bắt buộc.")]
		public string? InternalCode { get; set; }

		[Required(ErrorMessage = "Loại phòng là trường bắt buộc.")]
		public int? KindRoomId { get; set; }

		[Required(ErrorMessage = "Tình trạng phòng là bắt buộc.")]
		[RegularExpression($"^({Room.STATUS_EMPTY}|{Room.STATUS_BOOKED}|{Room.STATUS_LIVE}|{Room.STATUS_CLEANING})$",
			ErrorMessage = "Giá trị không hợp lệ cho tình trạng phòng.")]
		public string? Status { get; set; }
	}
}
