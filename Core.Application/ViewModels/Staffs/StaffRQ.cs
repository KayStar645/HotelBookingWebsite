using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Staffs
{
    public class StaffRQ
    {

		[Required(ErrorMessage = "Id is required.")]
		public int? Id { get; set; }

		[Required(ErrorMessage = "Mã nhân viên là trường bắt buộc.")]
		public string? InternalCode { get; set; }

		[Required(ErrorMessage = "Tên nhân viên là trường bắt buộc.")]
		[StringLength(100, ErrorMessage = "Tên nhân viên không vượt quá 100 ký tự.")]
		public string? Name { get; set; }

		[Display(Name = "Ngày sinh")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? DateOfBirth { get; set; }

		[Required(ErrorMessage = "Giới tính là trường bắt buộc.")]
		public string? Gender { get; set; }

		[StringLength(300, ErrorMessage = "Địa chỉ không vượt quá 300 ký tự.")]
		public string? Address { get; set; }

		[RegularExpression(@"^\d{10,15}$", ErrorMessage = "Số điện thoại không hợp lệ.")]
		public string? Phone { get; set; }
	}
}
