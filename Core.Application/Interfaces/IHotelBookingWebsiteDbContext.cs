using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Interfaces
{
    public interface IHotelBookingWebsiteDbContext
    {
        DbSet<Staff> Staffs { get; }
		DbSet<KindRoom> KindRooms { get; }
		DbSet<Room> Rooms { get; }

		DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
