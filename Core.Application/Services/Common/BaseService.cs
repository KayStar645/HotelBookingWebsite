using Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.Services.Common
{
    public class BaseService
    {
		public static List<ValidationResult> ValidateModel(object model)
		{
			var validationResults = new List<ValidationResult>();
			var context = new ValidationContext(model, serviceProvider: null, items: null);
			Validator.TryValidateObject(model, context, validationResults, true);
			return validationResults;
		}
	}
}
