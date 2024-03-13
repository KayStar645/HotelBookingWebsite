using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Auth
{
    public class RoleRQ
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên vai trò là trường bắt buộc.")]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public List<string>? PermissionsName { get; set; }
    }
}
