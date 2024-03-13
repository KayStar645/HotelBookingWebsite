using Core.Application.Interfaces.Common;
using Core.Domain.Auth;
using Core.Domain.Entities;
using Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class HotelBookingWebsiteDbContext : DbContext, IHotelBookingWebsiteDbContext
    {
        private readonly EntitySaveChangesInterceptor _saveChangesInterceptor;

        public HotelBookingWebsiteDbContext(
            DbContextOptions options,
            EntitySaveChangesInterceptor saveChangesInterceptor)
            : base(options)
        {
            _saveChangesInterceptor = saveChangesInterceptor;
        }
        public DbSet<Staff> Staffs => Set<Staff>();
		public DbSet<KindRoom> KindRooms => Set<KindRoom>();
		public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Service> Services => Set<Service>();

		public DbSet<User> Users => Set<User>();
		public DbSet<Role> Roles => Set<Role>();
		public DbSet<Permission> Permissions => Set<Permission>();
		public DbSet<UserRole> UserRoles => Set<UserRole>();
		public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
		public DbSet<UserPermission> UserPermissions => Set<UserPermission>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

			modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");
		}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Order of the interceptors is important
            optionsBuilder.AddInterceptors(_saveChangesInterceptor);
        }
    }
}
