using Core.Application.ViewModels.Staffs;

namespace Core.Application.ViewModels.Auth
{
    public class UserVM
    {
        public int? Id { get; set; }

        public string? UserName { get; set; }

		public string? Password{ get; set; }

		public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Type { get; set; }

        public List<RoleVM>? Roles { get; set; }

        public List<PermissionVM>? Permissions { get; set; }

        public int? StaffId { get; set; }
        public virtual StaffVM? Staff { get; set; }
    }
}
