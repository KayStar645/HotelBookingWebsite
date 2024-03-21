using Core.Domain.Common.Interfaces;
using Core.Domain.Common;


namespace Core.Domain.Entities
{
    public class Tour : AuditableEntity, IInternalCode, IName
    {
        public string? InternalCode { get; set; }

        public string? Name { get; set; }

        public string? TourGuide { get; set; }

        public DateTime? DateStart { get; set; }

        public string? Describe { get; set; }

        public double? Price { get; set; }
    }
}
