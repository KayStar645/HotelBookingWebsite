using AutoMapper;
using Core.Application.Interfaces.Auth;
using Core.Application.Interfaces.Common;
using Core.Application.ViewModels.Auth;
using Core.Domain.Auth;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Services.Auth
{
    public class PermissionService : IPermissionService
	{
        private readonly IHotelBookingWebsiteDbContext _context;
        private readonly IMapper _mapper;

        public PermissionService(IHotelBookingWebsiteDbContext pContext, IMapper pMapper)
        {
            _context = pContext;
            _mapper = pMapper;
        }

        public async Task<List<PermissionVM>> Create(List<PermissionRQ> pRequest)
        {
            try
            {
                List<PermissionVM> result = new List<PermissionVM>();

                foreach (var permission in pRequest)
                {
                    var per = await _context.Permissions
                        .FirstOrDefaultAsync(x => x.Name == permission.Name);

                    if (per == null)
                    {
                        var newPer = _mapper.Map<Permission>(permission);
                        var newPermission = await _context.Permissions.AddAsync(newPer);
                        await _context.SaveChangesAsync(default(CancellationToken));
                        result.Add(_mapper.Map<PermissionVM>(newPermission.Entity));
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PermissionVM>> Get()
        {
            var permissions = await _context.Permissions.ToListAsync();

            var result = _mapper.Map<List<PermissionVM>>(permissions);

            return result;
        }
    }
}
