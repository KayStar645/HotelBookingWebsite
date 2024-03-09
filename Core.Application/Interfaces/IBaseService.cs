using Core.Application.Responses;
using Core.Application.ViewModels.Common;

namespace Core.Application.Interfaces
{
	public interface IBaseService<TEntity, TRequest, TViewModel, TListRequest>
	{
		Task<PaginatedResult<TViewModel>> List(TListRequest pRequest);
		Task<TViewModel> Detail(int pId);
		Task<TViewModel> Create(TRequest pRequest);
		Task<TViewModel> Update(TRequest pRequest);
		Task<bool> Delete(int pId);
	}
}
