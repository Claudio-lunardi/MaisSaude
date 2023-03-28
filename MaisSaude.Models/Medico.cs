using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace MaisSaude.Models;


public partial class Medico
{
    [Key]
    public int ID { get; set; }

    [Required(ErrorMessage = "Nome é obrigatório!")]
    [StringLength(150, MinimumLength = 5, ErrorMessage = "Este campo deve ter de 5 a 150 caractéres")]
    public string Nome { get; set; }
    [Required(ErrorMessage = "Especialidade é obrigatório!")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Este campo deve ter de 5 a 150 caractéres")]
    public string Especialidade { get; set; }
    [Required(ErrorMessage = "Data de inclusão é obrigatório")]
    [DisplayName("Data de inclusão")]
    public DateTime DataInclusao { get; set; }

    [DisplayName("Data de alteração")]
    public DateTime? DataAlteracao { get; set; }

    [StringLength(100, ErrorMessage = "Este campo deve ter no máximo 100 caracteres.")]
    [Required(ErrorMessage = "Email é obrigatório!")]
    public string Email { get; set; }
}
