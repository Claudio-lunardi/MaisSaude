using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaisSaude.Models;


public class Clinica
{
    [Key]
    public int ID { get; set; }

    public string Nome { get; set; }


    [StringLength(20)]
    public string CNPJ { get; set; }

    [StringLength(50)]
    public string Cep { get; set; }

    [StringLength(15)]
    public string Telefone { get; set; }

    [StringLength(50)]
    public string Cidade { get; set; }

    [StringLength(2)]
    public string Estado { get; set; }

    [StringLength(50)]
    public string Complemento { get; set; }

    [StringLength(20)]
    public string Numero { get; set; }

    [StringLength(50)]
    public string Logradouro { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DataInclusao { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DataAlteracao { get; set; }

    [StringLength(50)]
    public string TipoPermissao { get; set; }

    [StringLength(100)]
    public string Usuario { get; set; }

    public string Senha { get; set; }

    public bool Ativo { get; set; }
}
