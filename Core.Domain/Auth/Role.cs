using Core.Domain.Common;
using Core.Domain.Common.Interfaces;

namespace Core.Domain.Auth
{
	public class Role : AuditableEntity, IName
	{
		public string? Name { get; set; }

		public string? Description { get; set; }

		public List<UserRole>? UserRoles { get; set; }

		public List<RolePermission>? RolePermissions { get; set; }
	}
}
