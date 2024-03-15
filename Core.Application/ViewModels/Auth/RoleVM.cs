namespace Core.Application.ViewModels.Auth
{
    public class RoleVM
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public List<string>? PermissionsName { get; set; }
    }
}
