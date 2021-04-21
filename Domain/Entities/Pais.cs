﻿using System;

namespace ProjetoDDD.Sensores.Domain.Entities
{
    public class Pais
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string IsoDuasLetras { get; set; }
        
        public string IsoTresLetras { get; set; }
       
        public string NumeroCodigoIso  { get; set; }    
    }
}
