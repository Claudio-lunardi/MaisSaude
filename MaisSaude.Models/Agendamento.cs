using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaisSaude.Models;

public class Agendamento
{
    [Key]
    public int ID { get; set; }

    public string? Clinica { get; set; }

    public string? Medico { get; set; }

    [StringLength(100)]
    public string? Especialidade { get; set; }

    [StringLength(100)]
    public string? UsuarioPaciente  { get; set; }

    public DateTime? DataConsulta { get; set; }

    [StringLength(150)]
    public string? Paciente { get; set; }

    public DateTime? DataInclusao { get; set; }

    public DateTime? DataAlteracao { get; set; }

    public bool Ativo { get; set; }
    public string Email { get; set; }

}
