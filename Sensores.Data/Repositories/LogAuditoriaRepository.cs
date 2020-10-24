using Domain.Entities;
using Domain.Interfaces;
using Infra.Configuration;
using Infra.Repository.Generics;

namespace Infra.Repositories
{
    public class LogAuditoriaRepository : GenericsRepository<LogAuditoria>, ILogAuditoriaRepository
    {
        public LogAuditoriaRepository(ContextBase context)
           : base(context) { }

    }
}
