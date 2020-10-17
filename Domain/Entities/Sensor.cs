using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Sensor : Base
    {        
        public int Numero { get; set; }

        //tirar daqui e pegar essa logica da viewModel
        [NotMapped]
        public bool Ativo { get; set; }

        public DateTime DataCadastro { get; set; }
       
        public DateTime DataAlteracao { get; set; }       

        public int RegiaoId { get; set; }

        public virtual Regiao Regiao { get; set; }

        public int PaisId { get; set; }

        public virtual Pais Pais { get; set; }

        public int StatusSensorId { get; set; }

        public virtual StatusSensor StatusSensor { get; set; }    

    }
}
