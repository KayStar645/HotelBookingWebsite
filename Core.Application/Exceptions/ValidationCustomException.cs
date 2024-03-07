using System.ComponentModel.DataAnnotations;

namespace Core.Application.Exceptions
{
	public class ValidationCustomException : ValidationException
	{
		public List<string> Errors { get; }

		public ValidationCustomException(List<string> errors)
		{
			Errors = errors;
		}
	}

}
