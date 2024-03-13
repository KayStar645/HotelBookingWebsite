using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Auth
{
    public class PermissionRQ
    {
        [Required(ErrorMessage = "Tên quyền là trường bắt buộc.")]
        public string? Name { get; set; }
    }
}
