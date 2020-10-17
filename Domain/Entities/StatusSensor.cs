using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Entities
{
    public class StatusSensor : Base
    {        
        public DateTime DataCadastro { get; set; }
       
        public DateTime DataAlteracao { get; set; }       
    }
}
