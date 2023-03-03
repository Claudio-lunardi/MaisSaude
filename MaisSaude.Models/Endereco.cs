using System.ComponentModel.DataAnnotations;

namespace MaisSaude.Models
{
    public class Endereco
    {
        [Required(ErrorMessage = "Cep é obrigatório!")]
        [StringLength(50)]
        public string Cep { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Cidade é obrigatório!")]
        public string Cidade { get; set; }

        [StringLength(2)]
        [Required(ErrorMessage = "Estado é obrigatório!")]
        public string Estado { get; set; }

        [StringLength(50)]
        public string? Complemento { get; set; }

        [StringLength(20, MinimumLength = 1, ErrorMessage = "Este campo deve ter no mínimo 1 a 20 caracteres.")]
        [Display(Name = "Número")]
        [Required(ErrorMessage = "Numero é obrigatório!")]
        public string Numero { get; set; }

        [StringLength(50, MinimumLength = 5, ErrorMessage = "Este campo deve ter no mínimo 5 a 50 caracteres.")]
        [Required(ErrorMessage = "Logradouro é obrigatório!")]
        public string Logradouro { get; set; }
    }
}
