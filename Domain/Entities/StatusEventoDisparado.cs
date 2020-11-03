using System;


namespace Domain.Entities
{
    public class StatusEventoDisparado
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public DateTime DataCadastro { get; set; }
       
        public DateTime DataAlteracao { get; set; }       
    }
}
