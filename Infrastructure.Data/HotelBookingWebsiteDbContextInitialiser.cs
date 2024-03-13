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
			if (!await _context.Users.AnyAsync(x => x.UserName == administrator.UserName &&
			    x.Email == administrator.Email && x.PhoneNumber == administrator.PhoneNumber))
            {
                var entity = await _context.Users.AddAsync(administrator);
				await _context.SaveChangesAsync(default(CancellationToken));
			}
            else
            {
                administrator = await _context.Users
                    .FirstOrDefaultAsync(x => x.UserName == administrator.UserName ||
                                        x.Email != administrator.Email ||
                                        x.PhoneNumber != administrator.PhoneNumber);

			}    

            //Default Role
            var role = new Role
            {
                Name = "admin",
                Description = "test",
            };
            if(!await _context.Roles.AnyAsync(x => x.Name == role.Name))
            {
                var entity = await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync(default(CancellationToken));
            }
            else
            {
                role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == role.Name);

			}

            // Add role to admin
            var userRole = new UserRole
            {
                UserId = administrator.Id,
                RoleId = role.Id,
            };
			if (!await _context.UserRoles.AnyAsync(x => x.UserId == administrator.Id && x.RoleId == role.Id))
			{
				await _context.AddAsync(userRole);
				await _context.SaveChangesAsync(default(CancellationToken));
			}

            // Add per to role
            var permissions = await _context.Permissions.ToListAsync();
            foreach (var permission in permissions)
            {
                var rolePermission = new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permission.Id,
                };
				if (!await _context.RolePermissions.AnyAsync(x => x.RoleId == role.Id &&
                        x.PermissionId == permission.Id))
				{
					await _context.AddAsync(rolePermission);
					await _context.SaveChangesAsync(default(CancellationToken));
				}
            }


		}
	}
}
