using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SensoresAPP.ViewModels
{
    public class PaisViewModel
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [Display(Name = "Código ISO (2 letras)")]
        public string IsoDuasLetras { get; set; }

        [Display(Name = "Código ISO (3 letras)")]
        public string IsoTresLetras { get; set; }

        [Display(Name = "Número do Código ISO")]
        public string NumeroCodigoIso { get; set; }

        [Required(ErrorMessage = "Data Cadastro é obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayName("Data Cadastro")]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "Data Alteração é obrigatório")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayName("Data Alteração")]
        public DateTime DataAlteracao { get; set; }
    }
}
