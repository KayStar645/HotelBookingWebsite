using Core.Domain.Common;

namespace Core.Domain.Entities
{
    public class Customer : AuditableEntity
    {
        public string? Name { get; set; }

        public string? Phone { get; set; }
    }
}
