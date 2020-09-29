using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceEventoDisparado;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AppEventoDisparado : InterfaceEventoDisparadoApp
    {
        IEventoDisparado _IEventoDisparado;

        public AppEventoDisparado(IEventoDisparado IEventoDisparado)
        {
            _IEventoDisparado = IEventoDisparado;
        }

        public async Task Add(EventoDisparado Objeto)
        {
            await _IEventoDisparado.Add(Objeto);
        }

        public async Task Delete(EventoDisparado Objeto)
        {
            await _IEventoDisparado.Delete(Objeto);
        }

        public async Task<EventoDisparado> GetEntityById(int Id)
        {
            return await _IEventoDisparado.GetEntityById(Id);
        }

        public async Task<List<EventoDisparado>> List()
        {
            return await _IEventoDisparado.List();
        }

        public async Task Update(EventoDisparado Objeto)
        {
            await _IEventoDisparado.Update(Objeto);
        }
    }
}
