using ProjetoDDD.Sensores.Application.Interfaces;
using ProjetoDDD.Sensores.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjetoDDD.Sensores.Domain.Interfaces;

namespace ProjetoDDD.Sensores.Application.Services
{
    public class SensorService : ISensorService
    {
        ISensorRepository _ISensor;

        public SensorService(ISensorRepository ISensor)
        {
            _ISensor = ISensor;
        }

        public async Task Add(Sensor Objeto)
        {
            await _ISensor.Add(Objeto);
        }

        public async Task Delete(Sensor Objeto)
        {
            await _ISensor.Delete(Objeto);
        }

        public async Task<Sensor> GetEntityById(int Id)
        {
            return await _ISensor.GetEntityById(Id);
        }

        public async Task<List<Sensor>> List()
        {
            return await _ISensor.List();
        }

        public async Task Update(Sensor Objeto)
        {
            await _ISensor.Update(Objeto);
        }
    }
}
