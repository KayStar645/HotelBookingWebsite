using Core.Application.Common;
using Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Auth
{
	public class RegisterRQ
	{
        [Required(ErrorMessage = "Tên đăng nhập là trường bắt buộc.")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email là trường bắt buộc.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại là trường bắt buộc.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Mật khẩu là trường bắt buộc.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Loại người dùng là trường bắt buộc.")]
        [RegularExpression($"^({ClaimValue.TYPE_ADMIN}|{ClaimValue.TYPE_SUPER_ADMIN}|{ClaimValue.TYPE_USER})$",
            ErrorMessage = "Giá trị không hợp lệ cho loại người dùng.")]
        public bool Type { get; set; }
	}
}
