using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoDDD.Sensores.Domain.Interfaces.Generics
{
    public interface ILogGenericsRepository<T> : IDisposable where T : class
    {
        Task Add(T Objeto); 
    }
}
