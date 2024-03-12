using Core.Application.ViewModels.Auth;

namespace Core.Application.Interfaces.Auth
{
	public interface IAccountService
	{
		Task<RegisterVM> RegisterAsync(RegisterRQ pRequest);

		Task<LoginVM> LoginAsync(LoginRQ pRequest);
	}
}
