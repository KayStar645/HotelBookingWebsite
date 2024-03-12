using Core.Application.Common;
using Core.Application.Exceptions;
using Core.Application.Interfaces.Auth;
using Core.Application.Interfaces.Common;
using Core.Application.ViewModels.Auth;
using Core.Domain.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Application.Services.Auth
{
	public class AccountService : IAccountService
	{
		private readonly IHotelBookingWebsiteDbContext _context;
		private readonly IPasswordHasher<User> _passwordHasher;
		private readonly IConfiguration _configuration;

		public AccountService(IHotelBookingWebsiteDbContext pContext,
			IPasswordHasher<User> pPasswordHasher, IConfiguration pConfiguration)
		{
			_context = pContext;
			_passwordHasher = pPasswordHasher;
			_configuration = pConfiguration;
		}

		public async Task<RegisterVM> RegisterAsync(RegisterRQ pRequest)
		{
			try
			{
				var user = new User
				{
					UserName = pRequest.UserName,
					Email = pRequest.Email,
					PhoneNumber = pRequest.PhoneNumber,
				};

				var hashedPassword = _passwordHasher.HashPassword(user, pRequest.Password);
				user.Password = hashedPassword;

				var result = await _context.Users.AddAsync(user);
				await _context.SaveChangesAsync(default(CancellationToken));

				return new RegisterVM() { Id = result.Entity.Id };
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<LoginVM> LoginAsync(LoginRQ pRequest)
		{
			try
			{
				User? user = await _context.Users.FirstOrDefaultAsync(x => x.Email == pRequest.UserName ||
									x.UserName == pRequest.UserName || x.PhoneNumber == pRequest.UserName);


				if (user == null)
				{
					throw new BadRequestException("Tài khoản không tồn tại");
				}
				var result = _passwordHasher.VerifyHashedPassword(user, user.Password, pRequest.Password);

				if (result != PasswordVerificationResult.Success)
				{
					throw new BadRequestException("Thông tin xác thực không hợp lệ!");
				}

				JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

				LoginVM auth = new LoginVM
				{
					Id = user.Id,
					Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
				};

				return auth;

			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		private async Task<JwtSecurityToken> GenerateToken(User pUser)
		{
			var roles = await _context.Roles
					.Where(x => x.UserRoles.Any(x => x.UserId == pUser.Id))
					.ToListAsync();
			var permissions = await _context.Permissions
					.Where(x => x.UserPermissions.Any(x => x.UserId == pUser.Id) ||
								x.RolePermissions.Any(x => x.Role.UserRoles.Any(x => x.UserId == pUser.Id)))
					.ToListAsync();

			var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role.Name));
			var permissionClaims = permissions.Select(permission => new Claim(ClaimCommon.Permission, permission.Name));

			var claims = new[]
			{
				new Claim(ClaimCommon.Uid, pUser.Id.ToString()),
			}
			.Union(permissionClaims)
			.Union(roleClaims);

			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
			var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

			var jwtSecurityToken = new JwtSecurityToken(
				issuer: _configuration["JwtSettings:Issuer"],
				audience: _configuration["JwtSettings:Audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:DurationInMinutes"])),
				signingCredentials: signingCredentials);
			return jwtSecurityToken;
		}
	}
}
