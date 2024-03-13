﻿using Core.Application.ViewModels.Auth;

namespace Core.Application.Interfaces.Auth
{
	public interface IPermissionService
	{
        Task<List<PermissionVM>> Create(List<string> pPermissions);
		Task<List<PermissionVM>> Get();
    }
}
