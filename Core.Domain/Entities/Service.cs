using Core.Domain.Common;

namespace Core.Domain.Entities
{
    public class Service : AuditableEntity
    {
        public string? InternalCode { get; set; }

        public string? Name { get; set; }

        public string? Describe { get; set; }

        public double? Price { get; set; }
    }
}
