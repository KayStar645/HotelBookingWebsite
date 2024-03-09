﻿using AutoMapper;
using Core.Application.ViewModels.KinbRoom;
using Core.Application.ViewModels.Room;
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

			CreateMap<KindRoom, KindRoomRQ>().ReverseMap();
			CreateMap<KindRoom, KindRoomVM>().ReverseMap();

			CreateMap<Room, RoomRQ>().ReverseMap();
			CreateMap<Room, RoomVM>().ReverseMap();
		}
    }
}
