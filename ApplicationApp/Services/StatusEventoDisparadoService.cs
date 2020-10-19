using SensoresAPP.Interfaces;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace SensoresAPP.SensoresService
{
    public class StatusEventoDisparadoService : IStatusEventoDisparadoService
    {
        IStatusEventoDisparadoRepository _IStatusEventoDisparado;

        public StatusEventoDisparadoService(IStatusEventoDisparadoRepository IStatusEventoDisparado)
        {
            _IStatusEventoDisparado = IStatusEventoDisparado;
        }

        public async Task Add(StatusEventoDisparado Objeto)
        {
            await _IStatusEventoDisparado.Add(Objeto);
        }

        public async Task Delete(StatusEventoDisparado Objeto)
        {
            await _IStatusEventoDisparado.Delete(Objeto);
        }

        public async Task<StatusEventoDisparado> GetEntityById(int Id)
        {
            return await _IStatusEventoDisparado.GetEntityById(Id);
        }

        public async Task<List<StatusEventoDisparado>> List()
        {
            return await _IStatusEventoDisparado.List();
        }

        public async Task Update(StatusEventoDisparado Objeto)
        {
            await _IStatusEventoDisparado.Update(Objeto);
        }
    }
}
