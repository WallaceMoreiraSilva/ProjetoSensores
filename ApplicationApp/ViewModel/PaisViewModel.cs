using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoDDD.Sensores.Application.ViewModel
{
    public class PaisViewModel
    {
        [Key]        
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
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
    }
}
