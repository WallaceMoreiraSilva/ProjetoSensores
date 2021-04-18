using System;

namespace SensoresAPP.Interfaces
{
    public interface IGestorLog
    {
        ILog GetLogger<T>();

        ILog GetLogger(Type type);

        ILog GetLogger(string name);
    }
}
