using System;

namespace Domain.Entities
{
    public class EventoDisparado
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int Valor { get; set; }
      
        public DateTime DataCadastro { get; set; }

        public int SensorId { get; set; }

        public virtual Sensor Sensor { get; set; }

        public int StatusEventoDisparadoId { get; set; }

        public virtual StatusEventoDisparado StatusEventoDisparado { get; set; }       
    }
}
