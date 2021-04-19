using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetoDDD.Sensores.Application.ViewModel
{
    public class RegiaoViewModel
    {
        [Key]        
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Nome { get; set; }
    }
}
