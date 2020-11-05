using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SensoresAPP.ViewModels
{
    public class EventoDisparadoViewModel
    {
        [Key]       
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        public int Valor { get; set; }

        //[Required(ErrorMessage = "Data Cadastro é obrigatório")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        //[DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        //[DisplayName("Data Cadastro")]
        //public DateTime DataCadastro { get; set; }         

        public int SensorId { get; set; }

        //public virtual SensorViewModel Sensor { get; set; }

        public int StatusEventoDisparado { get; set; }       
    }
}
