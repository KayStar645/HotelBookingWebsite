using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Common
{
	public class BaseRQ
	{
		[Required(ErrorMessage = "Id là trường bắt buộc.")]
		public int? Id { get; set; }
	}
}
