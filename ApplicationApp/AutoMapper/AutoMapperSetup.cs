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

            #endregion


            #region DomainToViewModel

                CreateMap<Sensor, SensorViewModel>();
                CreateMap<Pais, PaisViewModel>();
                CreateMap<Regiao, RegiaoViewModel>();
                CreateMap<EventoDisparado, EventoDisparadoViewModel>();                            

            #endregion

        }
    }
}
