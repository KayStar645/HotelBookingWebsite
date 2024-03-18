using Core.Domain.Common;

namespace Core.Domain.Entities
{
	public class RoomPromotion : AuditableEntity
	{
		public int? RoomId { get; set; }

		public Room? Room { get; set; }

		public int? PromotionId { get; set; }

		public Promotion? Promotion { get; set; }
	}
}
