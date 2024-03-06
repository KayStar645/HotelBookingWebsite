using AutoMapper;
using Core.Application.ViewModels.Staffs;
using Core.Domain.Entities;

namespace Core.Application.Profiles
{
    public class ModuleMappingProfile : Profile
    {
        public ModuleMappingProfile() 
        {
            CreateMap<Staff, StaffRQ>().ReverseMap();
            CreateMap<Staff, StaffVM>().ReverseMap();
        }
    }
}
