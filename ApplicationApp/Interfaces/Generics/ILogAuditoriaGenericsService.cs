using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoDDD.Sensores.Application.Interfaces.Generics
{
    public interface ILogAuditoriaGenericsService<T> where T : class
    {
        Task Add(T Objeto);
    }
}
