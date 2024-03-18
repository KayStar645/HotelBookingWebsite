using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Auth
{
	public class LinkToStaffRQ
	{
		[Required(ErrorMessage = "Vui lòng chọn tài khoản để liên kết.")]
		public int? UserId { get; set; }

		[Required(ErrorMessage = "Vui lòng chọn nhân viên liên kết.")]
		public int? StaffId { get; set; }

		public List<int>? RoleIds { get; set; }
	}
}
