using Core.Application.Responses;
using Core.Application.ViewModels.Booking;
using Core.Application.ViewModels.Common;


namespace Core.Application.Interfaces
{
	public interface IBookingService
	{
		Task<PaginatedResult<BookingVM>> List(BaseListRQ pRequest);

		Task<BookingVM> Detail(int pId);

		Task<BookingVM> Create(BookingRQ pRequest);

		Task<BookingVM> Update(BookingRQ pRequest);

		Task<bool> Delete(int pId);
	}
}
