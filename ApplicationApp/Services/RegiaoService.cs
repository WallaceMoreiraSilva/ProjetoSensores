using ProjetoDDD.Sensores.Application.Interfaces;
using ProjetoDDD.Sensores.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjetoDDD.Sensores.Domain.Interfaces;

namespace ProjetoDDD.Sensores.Application.Services
{
    public class RegiaoService : IRegiaoService
    {
        IRegiaoRepository _IRegiao;

        public RegiaoService(IRegiaoRepository IRegiao)
        {
            _IRegiao = IRegiao;
        }

        public async Task Add(Regiao Objeto)
        {
            await _IRegiao.Add(Objeto);
        }

        public async Task Delete(Regiao Objeto)
        {
            await _IRegiao.Delete(Objeto);
        }

        public async Task<Regiao> GetEntityById(int Id)
        {
            return await _IRegiao.GetEntityById(Id);
        }

        public async Task<List<Regiao>> List()
        {
            return await _IRegiao.List();
        }

        public async Task Update(Regiao Objeto)
        {
            await _IRegiao.Update(Objeto);
        }
    }
}
