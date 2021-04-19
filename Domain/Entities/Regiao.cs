using System;

namespace ProjetoDDD.Sensores.Domain.Entities
{
    public class Regiao
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public DateTime DataCadastro { get; set; }
       
        public DateTime DataAlteracao { get; set; }      
    }
}
