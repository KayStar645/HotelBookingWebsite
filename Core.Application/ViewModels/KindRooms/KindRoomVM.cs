using Core.Application.ViewModels.Common;

namespace Core.Application.ViewModels.KindRooms
{
	public class KindRoomVM : BaseVM
	{
		public string? InternalCode { get; set; }

		public string? Name { get; set; }

		public List<string>? Images { get; set; }

		public long? Price { get; set; }

		public int? MaximumPeople { get; set; }

		public string? Describe { get; set; }

		public bool? IsDeleted { get; set; }
	}
}
