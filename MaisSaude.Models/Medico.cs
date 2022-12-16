using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Models
{
    public class Medico
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Nome { get; set; }

        [StringLength(15)]
        public string? Telefone { get; set; }
        [Required]
        [StringLength(15)]
        public string Celular { get; set; }
        [Required]
        [StringLength(100)]
        public string Especialidade { get; set; }

        [Required]
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }

    }
}
