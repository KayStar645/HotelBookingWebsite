using Core.Application.ViewModels.Tour;
using Core.Application.ViewModels.Common;

namespace Core.Application.ViewModels.Booking
{
	public class BookingVM : BaseVM
	{
		public string? InternalCode { get; set; }
		public int TourId { get; set; }
		public TourVM? Tour { get; set; }
		public string? CustomerName { get; set; }
		public string? CustomerEmail { get; set; }
		public DateTime? BookingDate { get; set; }
		public int? NumberOfPersons { get; set; }
		public double? TotalPrice { get; set; }
		public bool? IsDeleted { get; set; }

	}
}
