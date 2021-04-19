using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoDDD.Sensores.Domain.Interfaces.Generics
{
    public interface IGenericsRepository<T> : IDisposable where T : class
    {
        Task Add(T Objeto);
        Task Update(T Objeto);
        Task Delete(T Objeto);
        Task<T> GetEntityById(int Id);
        Task<List<T>> List();
    }
}
