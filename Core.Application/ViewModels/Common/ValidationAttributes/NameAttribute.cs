using Core.Application.Interfaces.Common;
using Core.Domain.Common;
using Core.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Common.ValidationAttributes
{
	public class NameAttribute<TEntity, TRquest> : ValidationAttribute
	where TEntity : AuditableEntity, IName
		where TRquest : BaseRQ
	{

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var httpContext = validationContext.GetService<IHttpContextAccessor>()?.HttpContext;
			var dbContext = httpContext?.RequestServices?.GetService(typeof(IHotelBookingWebsiteDbContext)) as IHotelBookingWebsiteDbContext;

			if (dbContext == null)
			{
				return new ValidationResult("Không thể truy cập cơ sở dữ liệu.");
			}

			var query = dbContext.Set<TEntity>().AsQueryable();
			var currentEntity = validationContext.ObjectInstance as TRquest;

			if (currentEntity.Id != null && currentEntity.Id != 0)
			{
				query = query.Where(e => e.Id != currentEntity.Id).AsQueryable();
			}

			var isDuplicate = query.Any(s => s.Name == value.ToString());

			if (isDuplicate)
			{
				return new ValidationResult(ErrorMessage);
			}

			return ValidationResult.Success;
		}
	}

}
