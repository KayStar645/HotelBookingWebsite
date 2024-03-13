using Core.Domain.Common;

namespace Core.Domain.Auth
{
	public class Role : AuditableEntity
	{
		public string? Name { get; set; }

		public string? Description { get; set; }

		public List<UserRole>? UserRoles { get; set; }

		public List<RolePermission>? RolePermissions { get; set; }
	}
}
