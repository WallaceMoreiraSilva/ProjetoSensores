using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApplicationApp.ViewModels
{
    public class StatusSensorViewModel
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Nome { get; set; }           

        [Required(ErrorMessage = "Data Cadastro é obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayName("Data Cadastro")]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "Data Alteração é obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayName("Data Alteração")]
        [Display(Name = "Data de Alteração")]
        public DateTime DataAlteracao { get; set; }
    }
}
