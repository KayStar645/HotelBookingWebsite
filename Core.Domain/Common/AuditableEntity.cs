using Core.Domain.Common.Interfaces;

namespace Core.Domain.Common
{
    public abstract class AuditableEntity : IAuditableEntity
    {
        public int Id { get; set; } = default!;

        public DateTimeOffset? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public bool? IsDeleted { get; set; } = false;
    }
}
