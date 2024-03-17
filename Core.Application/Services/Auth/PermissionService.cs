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

        public async Task<List<PermissionVM>> Create(List<string> pRequest)
        {
            try
            {
                List<PermissionVM> result = new List<PermissionVM>();

                foreach (var permission in pRequest)
                {
                    var per = await _context.Permissions
                        .FirstOrDefaultAsync(x => x.Name == permission);

                    if (per == null)
                    {
                        var newPer = new Permission { Name = permission };
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

            return result ?? new List<PermissionVM>();
        }

		public async Task<List<PermissionDto>> PermissionByRole(int? pRoleId = null)
		{
			var permissions = await _context.Permissions.OrderBy(x => x.Name).ToListAsync();

			var permissionsById = await _context.Permissions
	            .Where(p => p.RolePermissions.Any(rp => rp.RoleId == pRoleId))
                .OrderBy(x => x.Name)
				.ToListAsync();
            var newPer = new List<PermissionDto>();

            for (int i = 0; i < permissions.Count; i ++)
            {
                var item = permissions[i];
                string module = GetModule(item.Name);
				PermissionDto dto = new PermissionDto
                {
                    Module = module,
                    IsGranted = pRoleId == null ? false : permissionsById.Any(x => x.Name == item.Name)
                };
                List<PermissionChild> child = new List<PermissionChild>();
                for (; i < permissions.Count; i++)
                {
					item = permissions[i];
                    if(module != GetModule(item.Name))
                    {
                        i--;
                        break;
                    }
					child.Add(new PermissionChild { 
                        Id = item.Id, 
                        Name = GetPermission(item.Name),
						IsGranted = pRoleId == null ? false : permissionsById.Any(x => x.Name == item.Name)
					});
				}
                dto.Permissions = child;
				newPer.Add(dto);

			}

            return newPer;
		}

        private string GetModule(string pPermission)
        {
            string value = pPermission.Split('-').First();
            switch (value)
			{
				case "customer":
					value = "Khách hàng";
					break;
				case "dashboard":
					value = "Thống kê";
					break;
				case "kindroom":
					value = "Loại phòng";
					break;
				case "room":
					value = "Phòng";
					break;
				case "service":
					value = "Dịch vụ";
					break;
				case "staff":
					value = "Nhân viên";
					break;
				case "role":
					value = "Quyền";
					break;
				case "user":
					value = "Người dùng";
					break;
			}
            return value;
		}

		private string GetPermission(string pPermission)
		{
			string value = pPermission.Split('-').Last();
			switch (value)
			{
				case "view":
					value = "Xem";
					break;
				case "create":
					value = "Thêm";
					break;
				case "update":
					value = "Sửa";
					break;
				case "delete":
					value = "Xoá";
					break;
				case "approve":
					value = "Duyệt";
					break;
			}
			return value;
		}
	}
}
