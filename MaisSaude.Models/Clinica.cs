using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Models
{
    public class Clinica
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string NomeClinica { get; set; }
        [Required]
        [StringLength(50)]
        public string Cep { get; set; }
        [Required]
        [StringLength(15)]
        public string Telefone { get; set; }
        [Required]
        [StringLength(50)]
        public string Cidade { get; set; }
        [Required]
        [StringLength(2)]
        public string Estado { get; set; }
        [Required]
        [StringLength(50)]
        public string Complemento { get; set; }
        [Required]
        [StringLength(50)]
        public string Numero { get; set; }
        [Required]
        [StringLength(50)]
        public string Logradouro { get; set; }
        [Required]
        public DateTime DataInclusao { get; set; }

        public DateTime? DataAlteracao { get; set; }


    }
}
