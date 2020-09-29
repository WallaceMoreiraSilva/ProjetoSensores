using ApplicationApp.ViewModels;
using AutoMapper;
using Entities.Entities;

namespace ApplicationApp.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<SensorViewModel, Sensor>();
        }
    }
}
