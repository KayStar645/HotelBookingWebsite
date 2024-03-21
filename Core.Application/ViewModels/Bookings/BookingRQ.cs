using Core.Application.ViewModels.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Booking
{
	public class BookingRQ : BaseRQ
	{
		[Required(ErrorMessage = "Mã Đặt tour là trường bắt buộc.")]
		public string InternalCode { get; set; }

		[Required(ErrorMessage = "Mã tour là trường bắt buộc.")]
		public int TourId { get; set; }

		[Required(ErrorMessage = "Tên người đặt tour là trường bắt buộc.")]
		[StringLength(100, ErrorMessage = "Tên người đặt tour không vượt quá 100 ký tự.")]
		public string CustomerName { get; set; }

		[Required(ErrorMessage = "Email là trường bắt buộc.")]
		[EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
		public string CustomerEmail { get; set; }

		[Display(Name = "Ngày đặt tour")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? BookingDate { get; set; }

		[Display(Name = "Số lượng người")]
		[Required(ErrorMessage = "Số lượng người là trường bắt buộc.")]
		[Range(1, int.MaxValue, ErrorMessage = "Số lượng người phải lớn hơn 0.")]
		public int? NumberOfPersons { get; set; }

		[Display(Name = "Tổng tiền")]
		[Required(ErrorMessage = "Tổng tiền là trường bắt buộc.")]
		[Range(0, int.MaxValue, ErrorMessage = "Tổng tiền tối thiểu phải bằng 0.")]
		public double? TotalPrice { get; set; }

	}
}
