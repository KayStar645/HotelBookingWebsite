using Core.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
	public class Room : AuditableEntity
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
	}
}
