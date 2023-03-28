using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MaisSaude.Models
{
    public class Titular : Endereco
    {
        [Key]
        public int ID { get; set; }
        
        [Required(ErrorMessage = "CPF é obrigatório!")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "Este campo deve ter 14 caracteres")]
        [DisplayName("CPF")]
        public string CPF_titular { get; set; }

        [Required(ErrorMessage = "RG é obrigatório!")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Este campo deve ter no mínimo 8 a 50 caracteres.")]
        public string RG { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório!")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Este campo deve ter de 5 a 150 caractéres")]
        public string Nome { get; set; }


        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "Data de nascimento é obrigatório!")]
        public DateTime DataNascimento { get; set; }

        [StringLength(15)]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "Celular é obrigatório!")]
        [StringLength(15, MinimumLength = 15, ErrorMessage = "Este campo deve ter no mínimo 15 caracteres.")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "Ativo é obrigatório!")]
        public bool Ativo { get; set; }


        [StringLength(100, ErrorMessage = "Este campo deve ter no máximo 100 caracteres.")]
        [Required(ErrorMessage = "Email é obrigatório!")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Data de inclusão é obrigatório")]
        [DisplayName("Data de inclusão")]
        public DateTime DataInclusao { get; set; }

        [DisplayName("Data de alteração")]
        public DateTime? DataAlteracao { get; set; }


        [StringLength(50)]
        [Required(ErrorMessage = "Tipo de permisão é obrigatório")]
        [DisplayName("Tipo de permisão")]
        public string TipoPermissao { get; set; }


        [StringLength(100)]
        [Required(ErrorMessage = "Usuário é obrigatório")]
        [DisplayName("Usuário")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        [DisplayName("Senha")]
        public string Senha { get; set; }
    }
}
