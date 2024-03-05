namespace Core.Domain.Common.Interfaces
{
    public interface IAuditableEntity
    {
        int Id { get; set; }

        DateTimeOffset? CreatedAt { get; set; }

        string? CreatedBy { get; set; }

        DateTimeOffset? UpdatedAt { get; set; }

        string? UpdatedBy { get; set; }

        bool? IsDeleted { get; set; }
    }
}
