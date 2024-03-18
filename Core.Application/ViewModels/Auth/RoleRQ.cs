using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Common.ValidationAttributes;
using Core.Domain.Auth;
using Core.Domain.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Auth
{
	public class RoleRQ : BaseRQ
    {
        [Required(ErrorMessage = "Tên vai trò là trường bắt buộc.")]
		[Name<Role, RoleRQ>(ErrorMessage = "Tên loại phòng đã tồn tại.")]
		public string? Name { get; set; }

        public string? Description { get; set; }

        public List<string>? PermissionsName { get; set; }
    }
}
