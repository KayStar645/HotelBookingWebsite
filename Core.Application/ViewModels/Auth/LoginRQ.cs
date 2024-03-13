using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Auth
{
	public class LoginRQ
	{
		[Required(ErrorMessage = "Tên đăng nhập là trường bắt buộc.")]
		public string? UserName { get; set; }

		[Required(ErrorMessage = "Mật khẩu là trường bắt buộc.")]
		public string? Password { get; set; }
	}
}
