using Core.Domain.Common;
using Core.Domain.Entities;

namespace Core.Domain.Auth
{
	public class User : AuditableEntity
	{
		public const string TYPE_ADMIN = "admin";

		public const string TYPE_SUPER_ADMIN = "super admin";

		public const string TYPE_USER = "user";


		public string? UserName { get; set; }

		public string? Password { get; set; }

		public string? Email { get; set; }

		public string? PhoneNumber { get; set; }

		public string? Type { get; set; }

		public List<UserRole>? UserRoles { get; set; }

		public List<UserPermission>? UserPermissions { get; set; }

		public int? StaffId { get; set; }
		public virtual Staff? Staff { get; set; }
	}
}
