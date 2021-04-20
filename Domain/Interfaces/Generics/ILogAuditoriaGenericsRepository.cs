using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoDDD.Sensores.Domain.Interfaces.Generics
{
    public interface ILogAuditoriaGenericsRepository<T> : IDisposable where T : class
    {
        Task Add(T Objeto); 
    }
}
