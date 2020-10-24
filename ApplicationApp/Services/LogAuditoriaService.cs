using Domain.Entities;
using Domain.Interfaces;
using SensoresAPP.Interfaces;
using System.Threading.Tasks;

namespace SensoresAPP.SensoresService
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
