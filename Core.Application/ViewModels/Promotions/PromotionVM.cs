using Core.Application.ViewModels.Common;

namespace Core.Application.ViewModels.Promotions
{
	public class PromotionVM : BaseVM
	{
		public string? InternalCode { get; set; }

		public string? Name { get; set; }

		public DateTime? StartTime { get; set; }

		public DateTime? EndTime { get; set; }

		public string? Type { get; set; }

		public string? Status { get; set; }

		// Giảm tiền
		public double? Discount { get; set; }

		public double? PercentMax { get; set; }

		// Giảm %
		public double? Percent { get; set; }

		public double? PriceMax { get; set; }
	}
}
