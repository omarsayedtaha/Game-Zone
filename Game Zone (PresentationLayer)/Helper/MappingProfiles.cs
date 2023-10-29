using AutoMapper;
using Core_Layer;
using Game_Zone__PresentationLayer_.Models;

namespace Game_Zone__PresentationLayer_.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Game, GameViewModel>()
                .ForMember(d => d.GameDevices, o => o.MapFrom(d => d.Devices))
                .ForMember(d => d.DevicesIds, o => o.MapFrom(d => d.Devices.Select(d => d.DeviceId)))
                 .ReverseMap().ForMember(d=>d.Devices , o=>o.MapFrom(d=>d.DevicesIds.Select(d => new GameDevices { DeviceId = d }).ToList()));

           //CreateMap<GameViewModel,Game>()
           //     .ForMember(d=>d.Devices)
        }
    }
}
