using AutoMapper;
using Core.Application.Exceptions;
using Core.Application.Interfaces.Common;
using Core.Application.Responses;
using Core.Application.Services.Extensions;
using Core.Application.ViewModels.Common;
using Core.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Services.Common
{
    public abstract class BaseService<TEntity, TRequest, TViewModel, TListRequest> 
		: IBaseService<TEntity, TRequest, TViewModel, TListRequest>
		where TEntity : AuditableEntity
		where TRequest : BaseRQ
		where TViewModel : BaseVM
		where TListRequest : BaseListRQ
	{
		protected readonly IHotelBookingWebsiteDbContext _context;
		protected readonly IMapper _mapper;

		public BaseService(IHotelBookingWebsiteDbContext pContext, IMapper pMapper)
		{
			_context = pContext;
			_mapper = pMapper;
		}

		public virtual async Task<PaginatedResult<TViewModel>> List(TListRequest pRequest)
		{
			var query = _context.Set<TEntity>().AsQueryable();

			if (pRequest.Search != null)
			{
				var sanitizedSearch = ValidateExtensions.RemoveDiacritics(pRequest.Search.ToLower());
				query = ApplySearch(query, sanitizedSearch);
			}

			if (pRequest.Filters != null)
			{
				query = QueryableExtensions.ApplyFilters(query, pRequest.Filters);
			}

			if (pRequest.Sorts != null)
			{
				query = QueryableExtensions.ApplySorting(query, pRequest.Sorts);
			}

			query = Query(query);

			var totalItems = await query.CountAsync();

			if (pRequest.PageSize == -1)
			{
				var entityResult = await query.ToListAsync();

				var viewModels = _mapper.Map<List<TViewModel>>(entityResult);
				return new PaginatedResult<TViewModel>(viewModels, totalItems, pRequest.Page, pRequest.PageSize);
			}
			else
			{
				var entityResult = await query
						.Skip(((int)pRequest.Page - 1) * (int)pRequest.PageSize)
						.Take((int)pRequest.PageSize)
						.ToListAsync();

				var viewModels = _mapper.Map<List<TViewModel>>(entityResult);
				return new PaginatedResult<TViewModel>(viewModels, totalItems, pRequest.Page, pRequest.PageSize);
			}
		}

		protected virtual IQueryable<TEntity> Query(IQueryable<TEntity> query)
		{
			return query.AsQueryable();
		}

		protected virtual IQueryable<TEntity> ApplySearch(IQueryable<TEntity> query, string keyword)
		{
			return query.AsQueryable();
		}

		public virtual async Task<TViewModel> Detail(int pId)
		{
			var entity = await _context.Set<TEntity>().FindAsync(pId);

			var viewModel = _mapper.Map<TViewModel>(entity);

			return viewModel;
		}

		public virtual async Task<TViewModel> Create(TRequest pRequest)
		{
			var validationResults = ValidateExtensions.ValidateModel(pRequest);

			if (validationResults.Any())
			{
				var errorMessage = validationResults.Select(result => result.ErrorMessage).ToList();
				throw new ValidationCustomException(errorMessage);
			}

			var entity = _mapper.Map<TEntity>(pRequest);

			var createEntity = await _context.Set<TEntity>().AddAsync(entity);
			await _context.SaveChangesAsync(default(CancellationToken));

			var viewModel = _mapper.Map<TViewModel>(createEntity.Entity);

			return viewModel;
		}

		public virtual async Task<TViewModel> Update(TRequest pRequest)
		{
			var validationResults = ValidateExtensions.ValidateModel(pRequest);

			if (validationResults.Any())
			{
				var errorMessage = validationResults.Select(result => result.ErrorMessage).ToList();
				throw new ValidationCustomException(errorMessage);
			}

			var entity = await _context.Set<TEntity>().FindAsync(pRequest.Id);

			if (entity == null)
			{
				throw new BadRequestException($"Id = {pRequest.Id} không tồn tại!");
			}
			var newEntity = _mapper.Map<TEntity>(pRequest);

			entity.CopyPropertiesFrom(newEntity);

			var updatedEntity = _context.Set<TEntity>().Update(entity);
			await _context.SaveChangesAsync(default(CancellationToken));

			var viewModel = _mapper.Map<TViewModel>(updatedEntity.Entity);

			return viewModel;
		}

		public virtual async Task<bool> Delete(int pId)
		{
			var entity = await _context.Set<TEntity>().FindAsync(pId);

			if (entity == null)
			{
				throw new BadRequestException($"Id = {pId} không tồn tại!");
			}

			_context.Set<TEntity>().Remove(entity);
			await _context.SaveChangesAsync(default(CancellationToken));

			return true;
		}
	}
}