using System;

namespace ProjetoDDD.Sensores.Application.Interfaces
{
    public interface ILogComIdentificadorUnico
    {
        bool DepuracaoEstaAtiva
        {
            get;
        }

        string IdentificadorLog
        {
            get;
            set;
        }

        ILogComIdentificadorUnico CriarLog(Func<IGestorLog, ILog> funcaoCriacaoLogCustomizada, bool empilhar = true);

        void Depurar(string mensagem);

        void Depurar(string mensagem, params object[] parametros);

        void Informacao(string mensagem);

        void Informacao(string mensagem, params object[] parametros);

        void Erro(string mensagem);

        void Erro(string mensagem, params object[] parametros);

        void LiberarLog();
    }
}
