using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

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

		public static string RemoveDiacritics(string input)
		{
			if (string.IsNullOrWhiteSpace(input))
				return input;

			string normalizedString = input.Normalize(NormalizationForm.FormD);
			StringBuilder stringBuilder = new StringBuilder();

			foreach (char c in normalizedString)
			{
				UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(c);
				if (category != UnicodeCategory.NonSpacingMark)
				{
					stringBuilder.Append(c);
				}
			}

			return stringBuilder.ToString();
		}
	}
}
