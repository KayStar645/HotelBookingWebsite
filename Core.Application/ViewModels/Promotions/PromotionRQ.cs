using Core.Application.ViewModels.Common;

namespace Core.Application.ViewModels.Promotions
{
	public class PromotionRQ : BaseRQ
	{
		public string? InternalCode { get; set; }

		public string? Name { get; set; }

		public DateTime? StartTime { get; set; }

		public DateTime? EndTime { get; set; }

		public string? Type { get; set; }

		public string? Status { get; set; }

		//
		public double? Discount { get; set; }

		public double? PercentMax { get; set; }

		//
		public double? Percent { get; set; }

		public double? PriceMax { get; set; }
	}
}
