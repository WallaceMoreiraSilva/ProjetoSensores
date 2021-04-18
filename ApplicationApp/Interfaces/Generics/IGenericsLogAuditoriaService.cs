using System.Threading.Tasks;

namespace SensoresAPP.Interfaces.Generics
{
    public interface IGenericsLogAuditoriaService<T> where T : class
    {
        Task Add(T Objeto);        
    }
}



