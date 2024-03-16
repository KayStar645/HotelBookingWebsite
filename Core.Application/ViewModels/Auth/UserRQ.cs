using Core.Application.ViewModels.Common.ValidationAttributes;
using Core.Domain.Auth;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Auth
{
	public class UserRQ
	{
		public int? Id { get; set; }

		[Required(ErrorMessage = "Tên tài khoản là trường bắt buộc.")]
		[User(ErrorMessage = "Tên tài khoản đã tồn tại.")]
		public string? UserName { get; set; }

		[Required(ErrorMessage = "Tên tài khoản là trường bắt buộc.")]
		public string? Password { get; set; }

		[User(ErrorMessage = "Email đã tồn tại.")]
		public string? Email { get; set; }

		[User(ErrorMessage = "Số điện thoại đã tồn tại.")]
		public string? PhoneNumber { get; set; }

		public string? Type { get; set; }

		public List<int>? RoleIds { get; set; }

		public int? StaffId { get; set; }
	}
}
