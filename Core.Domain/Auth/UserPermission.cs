﻿using Core.Domain.Common;

namespace Core.Domain.Auth
{
	public class UserPermission : AuditableEntity
	{
		public int UserId { get; set; }

		public User? User { get; set; }

		public int PermissionId { get; set; }

		public Permission? Permission { get; set; }
	}
}
