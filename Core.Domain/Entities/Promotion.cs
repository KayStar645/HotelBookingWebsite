using Core.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
	public class Promotion : AuditableEntity
	{
		[NotMapped]
		public const string TYPE_DISCOUNT = "D";

		[NotMapped]
		public const string TYPE_PERCENT = "P";

		[NotMapped]
		public const string STATUS_DARF = "D";

		[NotMapped]
		public const string STATUS_APPROVE = "A";

		[NotMapped]
		public const string STATUS_CANCEL = "C";


		public string? InternalCode { get; set; }

		public string? Name { get; set; }

		public DateTime? StartTime { get; set; }

		public DateTime? EndTime { get; set; }

		public double? Discount { get; set; }

		public double? PercentMax { get; set; }

		public double? Percent { get; set; }

		public double? PriceMax { get; set; }

		public string? Type { get; set; }

		public string? Status { get; set; }
	}
}
