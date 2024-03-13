using Core.Domain.Common;

namespace Core.Domain.Auth
{
	public class RolePermission : AuditableEntity
	{
		public int RoleId { get; set; }

		public Role? Role { get; set; }

		public int PermissionId { get; set; }

		public Permission? Permission { get; set; }
	}
}
