using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.Responses;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Staffs;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Services
{
    public class StaffService : IStaffService
    {
        private readonly IHotelBookingWebsiteDbContext _context;
        private readonly IMapper _mapper;

        public StaffService(IHotelBookingWebsiteDbContext pContext, IMapper pMapper)
        {
            _context = pContext;
            _mapper = pMapper;
        }

        public async Task<PaginatedResult<StaffVM>> List(BaseListRQ pRequest)
        {
            var query = _context.Staffs.AsQueryable();

            var staff = await query.ToListAsync();

            var staffVM = _mapper.Map<StaffVM>(staff);

            return new PaginatedResult<StaffVM>(staffVM);
        }

        public async Task<StaffVM> Detail(int pId)
        {
            throw new NotImplementedException();
        }

        public async Task<StaffVM> Create(StaffRQ pRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<StaffVM> Update(StaffRQ pRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int pId)
        {
            throw new NotImplementedException();
        }
    }
}
