using ProjetoDDD.Sensores.Application.Interfaces;
using ProjetoDDD.Sensores.Domain.Entities;
using ProjetoDDD.Sensores.Domain.Interfaces;
using System.Threading.Tasks;


namespace ProjetoDDD.Sensores.Application.Services
{
    public class LogAuditoriaService : ILogAuditoriaService
    {
        ILogAuditoriaRepository _ILogAuditoria;

        public LogAuditoriaService(ILogAuditoriaRepository ILogAuditoria)
        {
            _ILogAuditoria = ILogAuditoria;
        }

        public async Task Add(LogAuditoria Objeto)
        {
            await _ILogAuditoria.Add(Objeto);
        }
    } 
}
