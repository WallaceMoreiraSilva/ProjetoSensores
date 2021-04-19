using ProjetoDDD.Sensores.Application.Interfaces;
using ProjetoDDD.Sensores.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjetoDDD.Sensores.Domain.Interfaces;

namespace ProjetoDDD.Sensores.Application.Services
{
    public class EventoDisparadoService : IEventoDisparadoService
    {
        IEventoDisparadoRepository _IEventoDisparado;

        public EventoDisparadoService(IEventoDisparadoRepository IEventoDisparado)
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
