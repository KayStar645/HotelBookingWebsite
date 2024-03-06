using AutoMapper;
using Core.Application.Exceptions;
using Core.Application.Interfaces;
using Core.Application.Responses;
using Core.Application.Services.Extensions;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Staffs;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

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

            var staffVM = _mapper.Map<List<StaffVM>>(staff);

            return new PaginatedResult<StaffVM>(staffVM);
        }

        public async Task<StaffVM> Detail(int pId)
        {
            var staff = await _context.Staffs.FindAsync(pId);

            var staffVM = _mapper.Map<StaffVM>(staff);

            return staffVM;
        }

        public async Task<StaffVM> Create(StaffRQ pRequest)
        {
            var staff = _mapper.Map<Staff>(pRequest);

            var newStaff = await _context.Staffs.AddAsync(staff);
			await _context.SaveChangesAsync(default(CancellationToken));

			var staffVM = _mapper.Map<StaffVM>(newStaff.Entity);

            return staffVM;
        }

        public async Task<StaffVM> Update(StaffRQ pRequest)
        {
            var findStaff = await _context.Staffs.FindAsync(pRequest.Id);

            if(findStaff == null)
            {
                throw new BadRequestException($"Id = {pRequest.Id} không tồn tại!");
            }

            findStaff.CopyPropertiesFrom(pRequest);

            var newStaff = _context.Staffs.Update(findStaff);
            await _context.SaveChangesAsync(default(CancellationToken));

            var staffVM = _mapper.Map<StaffVM>(newStaff.Entity);

            return staffVM;
        }

        public async Task<bool> Delete(int pId)
        {
            var findStaff = await _context.Staffs.FindAsync(pId);

            if (findStaff == null)
            {
                throw new BadRequestException($"Id = {pId} không tồn tại!");
            }

            _context.Staffs.Remove(findStaff);
            await _context.SaveChangesAsync(default(CancellationToken));

            return true;
        }
    }
}
