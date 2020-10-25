using Domain.Entities;
using Domain.Interfaces;
using SensoresAPP.Interfaces;
using System;
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
            try
            {
                await _ILogAuditoria.Add(Objeto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }
    }
}
