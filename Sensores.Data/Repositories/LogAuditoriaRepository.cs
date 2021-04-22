using ProjetoDDD.Sensores.Domain.Entities;
using ProjetoDDD.Sensores.Domain.Interfaces;
using ProjetoDDD.Sensores.Infra.Data.Context;
using ProjetoDDD.Sensores.Infra.Data.Repository.Generics;

namespace ProjetoDDD.Sensores.Infra.Data.Repositories
{
    public class LogRepository : LogGenericsRepository<Log>, ILogRepository
    {
        public LogRepository(ContextBase context)
           : base(context) { }

    }
}
