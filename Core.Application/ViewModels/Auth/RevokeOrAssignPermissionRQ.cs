using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Auth
{
    public class RevokeOrAssignPermissionRQ
    {
        [Required(ErrorMessage = "Vui lòng chọn người dùng cần gán quyền.")]
        public int UserId { get; set; }

        public List<string>? Permissions { get; set; }
    }
}
