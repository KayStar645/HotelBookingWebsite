using Core.Application.Interfaces.Common;
using Core.Application.ViewModels.Auth;
using Core.Domain.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Common.ValidationAttributes
{
	public class UserAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var propertyName = validationContext.MemberName;
			var property = typeof(User).GetProperty(propertyName);

			var httpContext = validationContext.GetService<IHttpContextAccessor>()?.HttpContext;
			var dbContext = httpContext?.RequestServices?.GetService(typeof(IHotelBookingWebsiteDbContext)) as IHotelBookingWebsiteDbContext;

			if (dbContext == null)
			{
				return new ValidationResult("Không thể truy cập cơ sở dữ liệu.");
			}

			var query = dbContext.Users.AsQueryable();
			var currentEntity = validationContext.ObjectInstance as UserRQ;

			if (currentEntity.Id != null && currentEntity.Id != 0)
			{
				query = query.Where(e => e.Id != currentEntity.Id).AsQueryable();
			}

			var entities = query.ToList();
			var isDuplicate = entities.Any(x => (string)property.GetValue(x) == value.ToString());

			if (isDuplicate)
			{
				return new ValidationResult(ErrorMessage);
			}

			return ValidationResult.Success;
		}
	}

}
