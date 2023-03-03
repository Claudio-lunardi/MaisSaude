using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MaisSaude.Models
{
    public class Titular : Endereco
    {
        [Key]
        [Required(ErrorMessage = "CPF é obrigatório!")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "Este campo deve ter 14 caracteres")]
        [DisplayName("CPF")]
        public string CPFTitular { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório!")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Este campo deve ter de 5 a 150 caractéres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "RG é obrigatório!")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Este campo deve ter no mínimo 8 a 50 caracteres.")]
        public string RG { get; set; }

        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "Data de nascimento é obrigatório!")]
        public DateTime DataNascimento { get; set; }

        [StringLength(15)]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "Celular é obrigatório!")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "Este campo deve ter no mínimo 15 caracteres.")]
        public string Celular { get; set; }

        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Email é obrigatório!")]
        [StringLength(100, ErrorMessage = "Este campo deve ter no maximo 100 caracteres.")]
        public string Email { get; set; }

        [DisplayName("Data de inclusão")]
        public DateTime DataInclusao { get; set; }

        [DisplayName("Data de alteração")]
        public DateTime? DataAlteracao { get; set; }


        [StringLength(50)]
        [DisplayName("Tipo de permisão")]
        public string TipoPermissao { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Usuário")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        public string Senha { get; set; }
    }
}
