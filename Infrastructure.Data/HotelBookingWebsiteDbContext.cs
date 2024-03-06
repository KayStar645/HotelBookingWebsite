using Core.Application.Interfaces;
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Order of the interceptors is important
            optionsBuilder.AddInterceptors(_saveChangesInterceptor);
        }
    }
}
