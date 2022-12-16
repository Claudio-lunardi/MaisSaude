
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Models
{
    public class Titular
    {
        [Key]
        [StringLength(14)]
        public string CPFTitular { get; set; }
        [Required]
        [StringLength(150)]
        public string Nome { get; set; }
        [Required]
        [StringLength(50)]
        public string RG { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }
    
        [StringLength(15)]
        public string? Telefone { get; set; }
        [Required]
        [StringLength(15)]
        public string Celular { get; set; }
        [Required]
        public bool Ativo { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Cep { get; set; }
        [Required]
        [StringLength(50)]
        public string Cidade { get; set; }
        [Required]
        [StringLength(2)]
        public string Estado { get; set; }

        [StringLength(50)]
        public string? Complemento { get; set; }

        [Required]
        [StringLength(20)]
        public string Numero { get; set; }

        [Required]
        [StringLength(50)]
        public string Logradouro { get; set; }

        [Required]
        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }
    }
}
