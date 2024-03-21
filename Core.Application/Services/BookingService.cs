using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.Interfaces.Common;
using Core.Application.Services.Common;
using Core.Application.ViewModels.Booking;
using Core.Application.ViewModels.Common;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Core.Application.Services
{
	public class BookingService : BaseService<Booking, BookingRQ, BookingVM, BaseListRQ>, IBookingService
	{
		public BookingService(IHotelBookingWebsiteDbContext dbContext, IMapper mapper) :
			base(dbContext, mapper)
		{ }

		protected override IQueryable<Booking> ApplySearch(IQueryable<Booking> query, string keyword)
		{
			query = query.Where(x =>
				x.InternalCode.ToLower().Contains(keyword.ToLower()) ||
				x.Tour.InternalCode.ToLower().Contains(keyword.ToLower()) ||
				x.Tour.Name.ToLower().Contains(keyword.ToLower()) ||
				x.CustomerName.ToLower().Contains(keyword.ToLower()) ||
				x.CustomerEmail.ToLower().Contains(keyword.ToLower()));

			return query;
		}

		protected override IQueryable<Booking> Query(IQueryable<Booking> query)
		{
			query = query.Include(x => x.Tour);

			return base.Query(query);
		}
	}
}
