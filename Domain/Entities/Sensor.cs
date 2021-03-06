﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoDDD.Sensores.Domain.Entities
{
    public class Sensor
    {
        public int Id { get; set; }

        public string Nome { get; set; }       
        
        [NotMapped]
        public bool Ativo { get; set; }

        public DateTime DataCadastro { get; set; }
       
        public DateTime DataAlteracao { get; set; }       

        public int RegiaoId { get; set; }

        public virtual Regiao Regiao { get; set; }

        public int PaisId { get; set; }

        public virtual Pais Pais { get; set; }

        public int StatusSensor { get; set; }
    }
}
