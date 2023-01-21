using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaisSaude.Models;


public class Clinica
{
    [Key]
    public int Id { get; set; }

    [StringLength(150)]
    public string NomeClinica { get; set; } = null!;

    [Column("CNPJ")]
    [StringLength(20)]
    public string Cnpj { get; set; } = null!;

    [StringLength(50)]
    public string Cep { get; set; } = null!;

    [StringLength(15)]
    public string Telefone { get; set; } = null!;

    [StringLength(50)]
    public string Cidade { get; set; } = null!;

    [StringLength(2)]
    public string Estado { get; set; } = null!;

    [StringLength(50)]
    public string Complemento { get; set; } = null!;

    [StringLength(20)]
    public string Numero { get; set; } = null!;

    [StringLength(50)]
    public string Logradouro { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DataInclusao { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DataAlteracao { get; set; }

    [StringLength(50)]
    public string TipoPermissao { get; set; } = null!;

    [StringLength(100)]
    public string Usuario { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public bool Ativo { get; set; }
}
