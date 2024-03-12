using Core.Domain.Common;
using Core.Domain.Common.Interfaces;

namespace Core.Domain.Entities
{
    public class Staff : AuditableEntity, IInternalCode
	{
        public string? InternalCode { get; set; }

        public string? Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }
    }
}
