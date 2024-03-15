using Core.Domain.Common;
using Core.Domain.Common.Interfaces;

namespace Core.Domain.Entities
{
    public class Service : AuditableEntity, IInternalCode
    {
        public string? InternalCode { get; set; }

        public string? Name { get; set; }

        public string? Describe { get; set; }

        public double? Price { get; set; }
    }
}
