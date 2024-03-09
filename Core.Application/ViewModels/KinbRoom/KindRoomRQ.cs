using Core.Application.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.KinbRoom
{
	public class KindRoomRQ : BaseRQ
	{
		[Required(ErrorMessage = "Mã loại phòng là trường bắt buộc.")]
		public string? InternalCode { get; set; }

		[Required(ErrorMessage = "Tên loại phòng là trường bắt buộc.")]
		[StringLength(100, ErrorMessage = "Tên loại phòng không vượt quá 100 ký tự.")]
		public string? Name { get; set; }

		[Required(ErrorMessage = "Hình ảnh là trường bắt buộc.")]
		[RegularExpression(@"^[^.]+\.(jpg|jpeg|png|gif)$", ErrorMessage = "Định dạng hình ảnh không hợp lệ.")]
		public List<string>? Images { get; set; }

		[Required(ErrorMessage = "Giá là trường bắt buộc.")]
		[Range(0, int.MaxValue, ErrorMessage = "Giá tối thiểu phải bằng 0.")]
		public long? Price { get; set; }

		[Required(ErrorMessage = "Số người là trường bắt buộc.")]
		[Range(1, int.MaxValue, ErrorMessage = "Số người tối thiểu phải bằng 1.")]
		public int? MaximumPeople { get; set; }

		[Required(ErrorMessage = "Mô tả là trường bắt buộc.")]
		[StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự.")]
		public string? Describe { get; set; }
	}
}
