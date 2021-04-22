﻿using ProjetoDDD.Sensores.Domain.Entities;
using ProjetoDDD.Sensores.Application.Interfaces.Generics;

namespace ProjetoDDD.Sensores.Application.Interfaces
{
    public interface ILogService : ILogGenericsService<Log>
    {

    }
}