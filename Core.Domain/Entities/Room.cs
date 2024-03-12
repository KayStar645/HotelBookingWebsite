using Core.Domain.Common;
using Core.Domain.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
	public class Room : AuditableEntity, IInternalCode
	{
		[NotMapped]
		public const string STATUS_EMPTY = "E";
		[NotMapped]
		public const string STATUS_BOOKED = "B";
		[NotMapped]
		public const string STATUS_LIVE = "L";
		[NotMapped]
		public const string STATUS_CLEANING = "C";

		public string? InternalCode { get; set; }

		public string? Status { get; set; }


		// Khoá ngoại
		public int? KindRoomId { get; set; }

		[ForeignKey("KindRoomId")]
		public KindRoom? KindRoom { get; set; }

		public static string GetStatusName(string status)
		{
			switch (status)
			{
				case STATUS_EMPTY:
					return "Trống";
				case STATUS_BOOKED:
					return "Đã được đặt";
				case STATUS_LIVE:
					return "Khách đang ở";
				case STATUS_CLEANING:
					return "Đang dọn dẹp";
				default:
					return "Không xác định";
			}
		}
	}
}
