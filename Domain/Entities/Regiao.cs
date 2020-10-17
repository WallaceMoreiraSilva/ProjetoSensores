using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Entities
{
    public class Regiao : Base
    {           
        public DateTime DataCadastro { get; set; }
       
        public DateTime DataAlteracao { get; set; }      
    }
}
