using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaisSaude.Models;


public class Clinica : Endereco
{
    [Key]
    public int ID { get; set; }

    public string Nome { get; set; }


    [StringLength(20)]
    public string CNPJ { get; set; }

    [StringLength(15)]
    public string Telefone { get; set; }

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

    [StringLength(100, ErrorMessage = "Este campo deve ter no máximo 100 caracteres.")]
    [Required(ErrorMessage = "Email é obrigatório!")]
    public string Email { get; set; }

    public bool Ativo { get; set; }
}
