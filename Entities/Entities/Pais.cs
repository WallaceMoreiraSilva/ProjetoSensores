using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Entities
{
    public class Pais : Base
    {                 
        public string IsoDuasLetras { get; set; }
        
        public string IsoTresLetras { get; set; }
       
        public string NumeroCodigoIso  { get; set; }
       
        public DateTime DataCadastro { get; set; }
       
        public DateTime DataAlteracao { get; set; }        
    }
}
