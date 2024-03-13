using Core.Application.Common;
using Core.Domain.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
	public class HotelBookingWebsiteDbContextInitialiser
    {
        private readonly ILogger<HotelBookingWebsiteDbContextInitialiser> _logger;
        private readonly HotelBookingWebsiteDbContext _context;
		private readonly IPasswordHasher<User> _passwordHasher;

		public HotelBookingWebsiteDbContextInitialiser(ILogger<HotelBookingWebsiteDbContextInitialiser> logger,
            HotelBookingWebsiteDbContext context, IPasswordHasher<User> pPasswordHasher)
        {
            _logger = logger;
            _context = context;
            _passwordHasher = pPasswordHasher;
        }

        public async Task InitializeAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            //Default Admin
            var administrator = new User
            { 
                UserName = "admin",
                Email = "administrator@localhost",
                PhoneNumber = "0987654321",
                Type = ClaimValue.TYPE_ADMIN
			};

			var hashedPassword = _passwordHasher.HashPassword(administrator, "123");
			administrator.Password = hashedPassword;

			if (_context.Users.All(x => x.UserName != administrator.UserName &&
			    x.Email != administrator.Email && x.PhoneNumber != administrator.PhoneNumber))
            {
                await _context.AddAsync(administrator);
				await _context.SaveChangesAsync(default(CancellationToken));
			}

			//Default Permission


			//Default Role


		}
	}
}
