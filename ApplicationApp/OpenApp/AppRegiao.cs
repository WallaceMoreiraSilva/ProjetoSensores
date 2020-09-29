using ApplicationApp.Interfaces;
using Domain.Interfaces.InterfaceRegiao;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationApp.OpenApp
{
    public class AppRegiao : InterfaceRegiaoApp
    {
        IRegiao _IRegiao;

        public AppRegiao(IRegiao IRegiao)
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
