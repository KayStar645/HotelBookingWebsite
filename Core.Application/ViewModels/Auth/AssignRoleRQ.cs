namespace Core.Application.ViewModels.Auth
{
    public class AssignRoleRQ
    {
        public int UserId { get; set; }

        public List<int>? RolesId { get; set; }
    }
}
