using Core.Application.ViewModels.Common;

namespace Core.Application.ViewModels.Services
{
    public class ServiceVM : BaseVM
    {
        public string? InternalCode { get; set; }

        public string? Name { get; set; }

        public string? Describe { get; set; }

        public double? Price { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
