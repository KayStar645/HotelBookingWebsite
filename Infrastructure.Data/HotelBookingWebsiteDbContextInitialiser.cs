using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class HotelBookingWebsiteDbContextInitialiser
    {
        private readonly ILogger<HotelBookingWebsiteDbContextInitialiser> _logger;
        private readonly HotelBookingWebsiteDbContext _context;

        public HotelBookingWebsiteDbContextInitialiser(ILogger<HotelBookingWebsiteDbContextInitialiser> logger, HotelBookingWebsiteDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitializeAsync()
        {
            try
            {
                //await _context.Database.MigrateAsync();
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
            // Dùng khi cần tạo dữ liệu trước
            // Ví dụ tạo trước tài khoản admin - Full quyền
        }
    }
}
