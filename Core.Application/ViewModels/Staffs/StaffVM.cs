﻿using Core.Application.ViewModels.Common;

namespace Core.Application.ViewModels.Staffs
{
    public class StaffVM : BaseVM
    {
        public string? InternalCode { get; set; }

        public string? Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Gender { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

		public bool? IsDeleted { get; set; }
	}
}
