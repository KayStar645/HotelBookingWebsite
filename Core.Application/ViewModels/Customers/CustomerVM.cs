using Core.Application.ViewModels.Common;

namespace Core.Application.ViewModels.Customers
{
    public class CustomerVM : BaseVM
    {
        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? Type { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
