using Core.Domain.Common;

namespace Core.Domain.Entities
{
	public class KindRoom : AuditableEntity
	{
		public string? InternalCode { get; set; }

		public string? Name { get; set; }

		public string? Images { get; set; }

		public long? Price { get; set; }

		public int? MaximumPeople { get; set; }

		public string? Describe { get; set; }
	}
}
