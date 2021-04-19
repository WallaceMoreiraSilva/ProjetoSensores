using ProjetoDDD.Sensores.Application.Interfaces;
using ProjetoDDD.Sensores.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjetoDDD.Sensores.Domain.Interfaces;

namespace ProjetoDDD.Sensores.Application.Services
{
    public class PaisService : IPaisService
    {
        IPaisRepository _IPais;

        public PaisService(IPaisRepository IPais)
        {
            _IPais = IPais;
        }

        public async Task Add(Pais Objeto)
        {
            await _IPais.Add(Objeto);
        }

        public async Task Delete(Pais Objeto)
        {
            await _IPais.Delete(Objeto);
        }

        public async Task<Pais> GetEntityById(int Id)
        {
            return await _IPais.GetEntityById(Id);
        }

        public async Task<List<Pais>> List()
        {
            return await _IPais.List();
        }

        public async Task Update(Pais Objeto)
        {
            await _IPais.Update(Objeto);
        }
    }
}
