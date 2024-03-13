using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Auth
{
    public class AssignRoleRQ
    {
        [Required(ErrorMessage = "Vui lòng chọn người dùng cần gán vai trò.")]
        public int UserId { get; set; }

        public List<int>? RolesId { get; set; }
    }
}
