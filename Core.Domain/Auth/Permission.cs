using Core.Domain.Common;

namespace Core.Domain.Auth
{
	public class Permission : AuditableEntity
	{
		public string? Name { get; set; }

		public List<UserPermission>? UserPermissions { get; set; }

		public List<RolePermission>? RolePermissions { get; set; }
	}
}
