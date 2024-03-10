using System.Linq.Expressions;
using System.Reflection;

namespace Core.Application.Services.Extensions
{
	public static class QueryableExtensions
	{
		public static IQueryable<T> ApplySorting<T>(IQueryable<T> query, string sortExpression)
		{
			if (!string.IsNullOrWhiteSpace(sortExpression))
			{
				var sorts = sortExpression.Split(',');

				foreach (var sort in sorts)
				{
					var descending = false;
					var propertyName = sort.Trim();

					if (propertyName.StartsWith("-"))
					{
						descending = true;
						propertyName = propertyName.Substring(1);
					}

					query = ApplyOrder(query, propertyName, descending);
				}
			}

			return query;
		}

		private static IQueryable<T> ApplyOrder<T>(IQueryable<T> query, string propertyName, bool descending)
		{
			var entityType = typeof(T);
			var propertyInfo = entityType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

			if (propertyInfo == null)
			{
				return query;
			}

			var parameter = Expression.Parameter(entityType, "entity");
			var property = Expression.Property(parameter, propertyInfo);
			var lambda = Expression.Lambda(property, parameter);

			string methodName = descending ? "OrderByDescending" : "OrderBy";
			Type[] types = new Type[] { entityType, propertyInfo.PropertyType };
			var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == methodName && m.GetParameters().Length == 2);
			var genericMethod = method.MakeGenericMethod(types);

			return (IQueryable<T>)genericMethod.Invoke(null, new object[] { query, lambda });
		}

		public static IQueryable<T> ApplyFilters<T>(IQueryable<T> query, string filterExpression)
		{
			if (!string.IsNullOrWhiteSpace(filterExpression))
			{
				var filters = filterExpression.Split(',');

				foreach (var filter in filters)
				{
					var parts = filter.Trim().Split(':');
					if (parts.Length == 2)
					{
						var propertyName = parts[0];
						var propertyValue = parts[1];

						query = ApplyFilter(query, propertyName, propertyValue);
					}
				}
			}

			return query;
		}

		private static IQueryable<T> ApplyFilter<T>(IQueryable<T> query, string propertyName, string propertyValue)
		{
			if (string.IsNullOrEmpty(propertyValue))
			{
				return query;
			}
			var entityType = typeof(T);
			var propertyInfo = entityType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

			if (propertyInfo == null)
			{
				throw new ArgumentException($"Property '{propertyName}' not found on type {entityType.Name}");
			}

			var parameter = Expression.Parameter(entityType, "entity");
			var property = Expression.Property(parameter, propertyInfo);

			// Thêm xử lý kiểu dữ liệu int?
			if (propertyInfo.PropertyType == typeof(int?))
			{
				if (int.TryParse(propertyValue, out var intValue))
				{
					var value = Expression.Constant(intValue, typeof(int?));
					var equals = Expression.Equal(property, value);
					var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);
					return query.Where(lambda);
				}
				else
				{
					throw new ArgumentException($"Invalid value for property '{propertyName}'. Expected integer.");
				}
			}
			else if (propertyInfo.PropertyType == typeof(int))
			{
				// Xử lý kiểu dữ liệu int
				if (int.TryParse(propertyValue, out var intValue))
				{
					var value = Expression.Constant(intValue);
					var equals = Expression.Equal(property, value);
					var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);
					return query.Where(lambda);
				}
				else
				{
					throw new ArgumentException($"Invalid value for property '{propertyName}'. Expected integer.");
				}
			}
			else
			{
				// Xử lý kiểu dữ liệu khác
				var value = Convert.ChangeType(propertyValue, propertyInfo.PropertyType);
				var equals = Expression.Equal(property, Expression.Constant(value));
				var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);
				return query.Where(lambda);
			}
		}
	}
}
