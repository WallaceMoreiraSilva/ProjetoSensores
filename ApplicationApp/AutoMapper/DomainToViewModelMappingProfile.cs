using ApplicationApp.ViewModels;
using AutoMapper;
using Entities.Entities;

namespace ApplicationApp.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Sensor, SensorViewModel>();
        }
    }
}
