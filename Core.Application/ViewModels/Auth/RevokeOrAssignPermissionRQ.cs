namespace Core.Application.ViewModels.Auth
{
    public class RevokeOrAssignPermissionRQ
    {
        public int UserId { get; set; }

        public List<string>? Permissions { get; set; }
    }
}
