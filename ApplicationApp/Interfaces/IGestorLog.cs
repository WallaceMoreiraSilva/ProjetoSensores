using System;

namespace ProjetoDDD.Sensores.Application.Interfaces
{
    public interface IGestorLog
    {
        ILog GetLogger<T>();

        ILog GetLogger(Type type);

        ILog GetLogger(string name);
    }
}
