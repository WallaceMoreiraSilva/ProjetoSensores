using AutoMapper;
using Domain.Entities;
using SensoresAPP.ViewModels;

namespace SensoresApp.AutoMapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {

            #region ViewModelToDomain

                CreateMap<SensorViewModel,Sensor>();
                CreateMap<PaisViewModel, Pais>();
                CreateMap<RegiaoViewModel, Regiao>();
                CreateMap<EventoDisparadoViewModel, EventoDisparado>();
                CreateMap<StatusEventoDisparadoViewModel, StatusEventoDisparado>();
                CreateMap<StatusSensorViewModel, StatusSensor>();

            #endregion


            #region DomainToViewModel

                CreateMap<Sensor, SensorViewModel>();
                CreateMap<Pais, PaisViewModel>();
                CreateMap<Regiao, RegiaoViewModel>();
                CreateMap<EventoDisparado, EventoDisparadoViewModel>();
                CreateMap<StatusEventoDisparado, StatusEventoDisparadoViewModel>();
                CreateMap<StatusSensor, StatusSensorViewModel>();

            #endregion

        }
    }
}
