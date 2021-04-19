namespace ProjetoDDD.Sensores.Domain
{
    public class Configuracao
    {
        public AppSettings AppSettings { get; set; }

        public string CaminhoArquivoLog { get; set; }       

        public string UrlBaseDownloadDocumento { get; set; }

        public RetentativaExcecoesTrataveis RetentativaExcecoesTrataveis { get; set; }       
    }

    public class AppSettings
    {
        public string ConnectionString { get; set; }

        public int TimeoutTransacaoEmSegundos { get; set; }

        public string NivelIsolamento { get; set; }

        public int DbProvider { get; set; }
    }

    public class RetentativaExcecoesTrataveis
    {
        public int QuantidadeLimiteRetentativas { get; set; }

        public int TempoEntreRetentativas { get; set; }
    }
}