using Core.Application.ViewModels.Common;

namespace Core.Application.ViewModels.Tour
{
    public class TourVM : BaseVM
    {
        public string? InternalCode { get; set; }

        public string? Name { get; set; }

        public string? TourGuide { get; set; }
        public DateTime? DateStart { get; set; }

        public string? Describe { get; set; }

        public double? Price { get; set; }
        public bool? IsDeleted { get; set; }

    }
}
