using AutoMapper;
using ProjetoDDD.Sensores.Domain.Entities;
using ProjetoDDD.Sensores.Application.ViewModel;

namespace ProjetoDDD.Sensores.Application.AutoMapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {

            #region ViewModelToDomain
                CreateMap<SensorViewModel,Sensor>();
                CreateMap<PaisViewModel, Pais>();
                CreateMap<RegiaoViewModel, Regiao>();     
            #endregion

            #region DomainToViewModel
                CreateMap<Sensor, SensorViewModel>();
                CreateMap<Pais, PaisViewModel>();
                CreateMap<Regiao, RegiaoViewModel>();     
            #endregion

        }
    }
}
