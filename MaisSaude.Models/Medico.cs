using System.ComponentModel.DataAnnotations;


namespace MaisSaude.Models;


public partial class Medico
{
    [Key]
    public int ID { get; set; }

    [StringLength(150)]
    public string Nome { get; set; }

    public string Especialidade { get; set; }

    public DateTime DataInclusao { get; set; }

    public DateTime? DataAlteracao { get; set; }

    [StringLength(100)]
    public string Email { get; set; }
}
