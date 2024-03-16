namespace Core.Application.ViewModels.Auth
{
	public class PermissionChild
	{
		public int? Id { get; set; }

		public string? Name { get; set; }

		public bool? IsGranted { get; set; }
	}

	public class PermissionDto
	{
		public string? Module { get; set; }

		public List<PermissionChild>? Permissions { get; set; }

		public bool? IsGranted { get; set; }
	}
}
