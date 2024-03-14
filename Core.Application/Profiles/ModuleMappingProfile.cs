using AutoMapper;
using Core.Application.ViewModels.Auth;
using Core.Application.ViewModels.KindRooms;
using Core.Application.ViewModels.Rooms;
using Core.Application.ViewModels.Services;
using Core.Application.ViewModels.Staffs;
using Core.Domain.Auth;
using Core.Domain.Entities;

namespace Core.Application.Profiles
{
	public class ModuleMappingProfile : Profile
    {
        public ModuleMappingProfile() 
        {
            CreateMap<Staff, StaffRQ>().ReverseMap();
            CreateMap<Staff, StaffVM>().ReverseMap();

			CreateMap<KindRoom, KindRoomRQ>().ReverseMap();
			CreateMap<KindRoom, KindRoomVM>().ReverseMap();

			CreateMap<Room, RoomRQ>().ReverseMap();
			CreateMap<Room, RoomVM>().ReverseMap();

			CreateMap<Permission, PermissionRQ>().ReverseMap();
			CreateMap<Permission, PermissionVM>().ReverseMap();

            CreateMap<User, UserVM>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role)))
            .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.UserPermissions.Select(up => up.Permission)))
            .ForMember(dest => dest.Staff, opt => opt.MapFrom(src => src.Staff))
            .ReverseMap();

            CreateMap<Service, ServiceRQ>().ReverseMap();
            CreateMap<Service, ServiceVM>().ReverseMap();
        }
    }
}
