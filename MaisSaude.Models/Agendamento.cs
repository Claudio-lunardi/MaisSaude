using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaisSaude.Models;

public class Agendamento
{
    [Key]
    public int ID { get; set; }
    [Required(ErrorMessage = "Clinica é obrigatório!")]
    [StringLength(100)]
    public string Clinica { get; set; }

    [Required(ErrorMessage = "Medico é obrigatório!")]
    [StringLength(100)]
    public string Medico { get; set; }

    [Required(ErrorMessage = "Especialidade é obrigatório!")]
    [StringLength(100)]
    public string Especialidade { get; set; }

    [Required(ErrorMessage = "Usuario é obrigatório!")]
    [StringLength(100)]
    public string UsuarioPaciente  { get; set; }

    [Required(ErrorMessage = "Data da consulta é obrigatório!")]
    public DateTime DataConsulta { get; set; }

    [Required(ErrorMessage = "Paciente é obrigatório!")]
    [StringLength(150)]
    public string Paciente { get; set; }

    [Required(ErrorMessage = "data de inclusao é obrigatório!")]
    public DateTime DataInclusao { get; set; }

    public DateTime? DataAlteracao { get; set; }

    [Required(ErrorMessage = "Ativo é obrigatório!")]
    public bool Ativo { get; set; }

    [StringLength(100, ErrorMessage = "Este campo deve ter no máximo 100 caracteres.")]
    [Required(ErrorMessage = "Email é obrigatório!")]
    public string Email { get; set; }

}
