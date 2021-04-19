using System;
using System.Threading.Tasks;

namespace ProjetoDDD.Sensores.Domain.Interfaces.Generics
{
    public interface IGenericsLogAuditoriaRepository<T> : IDisposable where T : class
    {
        Task Add(T Objeto);
    }
}
