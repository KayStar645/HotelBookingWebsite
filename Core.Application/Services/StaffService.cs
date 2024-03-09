using AutoMapper;
using Core.Application.Exceptions;
using Core.Application.Interfaces;
using Core.Application.Responses;
using Core.Application.Services.Common;
using Core.Application.Services.Extensions;
using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Staffs;
using Core.Domain.Entities;
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

            if (pRequest.Search != null)
            {
				var sanitizedSearch = BaseService.RemoveDiacritics(pRequest.Search.ToLower());

				query = query.Where(x =>
					x.InternalCode.ToLower().Contains(sanitizedSearch) ||
					x.Name.ToLower().Contains(sanitizedSearch) ||
					x.Address.ToLower().Contains(sanitizedSearch) ||
					x.Phone.ToLower().Contains(sanitizedSearch));
			}

            if(pRequest.Filters != null)
            {
                query = BaseService.ApplyFilters(query, pRequest.Filters);
            }   
            
            if(pRequest.Sorts != null)
            {
				query = BaseService.ApplySorting(query, pRequest.Sorts);
			}    

			var totalItems = await query.CountAsync();
			var staff = await query
                        .Skip(((int)pRequest.Page - 1) * (int)pRequest.PageSize)
		                .Take((int)pRequest.PageSize)
		                .ToListAsync();

            var staffVM = _mapper.Map<List<StaffVM>>(staff);

            return new PaginatedResult<StaffVM>(staffVM, totalItems, pRequest.Page, pRequest.PageSize);
        }

        public async Task<StaffVM> Detail(int pId)
        {
            var staff = await _context.Staffs.FindAsync(pId);

            var staffVM = _mapper.Map<StaffVM>(staff);

            return staffVM;
        }

        public async Task<StaffVM> Create(StaffRQ pRequest)
        {
			var validationResults = BaseService.ValidateModel(pRequest);

			if (validationResults.Any())
			{
				var errorMessage = validationResults.Select(result => result.ErrorMessage).ToList();
				throw new ValidationCustomException(errorMessage);
			}

			var staff = _mapper.Map<Staff>(pRequest);

            var newStaff = await _context.Staffs.AddAsync(staff);
			await _context.SaveChangesAsync(default(CancellationToken));

			var staffVM = _mapper.Map<StaffVM>(newStaff.Entity);

            return staffVM;
        }

        public async Task<StaffVM> Update(StaffRQ pRequest)
		{
			var validationResults = BaseService.ValidateModel(pRequest);

			if (validationResults.Any())
			{
				var errorMessage = validationResults.Select(result => result.ErrorMessage).ToList();
				throw new ValidationCustomException(errorMessage);
			}

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
