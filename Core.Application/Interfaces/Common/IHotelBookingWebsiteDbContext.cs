using Core.Domain.Auth;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Interfaces.Common
{
    public interface IHotelBookingWebsiteDbContext
    {
        DbSet<Staff> Staffs { get; }
        DbSet<KindRoom> KindRooms { get; }
        DbSet<Room> Rooms { get; }
        DbSet<Service> Services { get; }

		DbSet<User> Users { get; }
		DbSet<Role> Roles { get; }
		DbSet<Permission> Permissions { get; }
		DbSet<UserRole> UserRoles { get; }
		DbSet<RolePermission> RolePermissions { get; }
		DbSet<UserPermission> UserPermissions { get; }


		DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
