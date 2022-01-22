using AutoMapper;
using TruckManagement.Models.Entities;
using TruckManagement.ViewModels;

namespace TruckManagement.Business.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Truck, TruckViewModel>().ReverseMap();
        }
    }
}
