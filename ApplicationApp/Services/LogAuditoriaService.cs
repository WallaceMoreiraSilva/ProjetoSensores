using ProjetoDDD.Sensores.Application.Interfaces;
using ProjetoDDD.Sensores.Domain.Entities;
using ProjetoDDD.Sensores.Domain.Interfaces;
using System.Threading.Tasks;

namespace ProjetoDDD.Sensores.Application.Services
{
    public class LogService : ILogService
    {
        ILogRepository _log;

        public LogService(ILogRepository log)
        {
            _log = log;
        }

        public async Task Add(Log Objeto)
        {
            await _log.Add(Objeto);
        }
    } 
}
