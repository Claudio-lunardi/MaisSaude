using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Models
{
    public class Agendamento
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Titular_CPFTitular { get; set; }
        public Titular? Titular { get; set; }

        public string Clinica_Id { get; set; }
        public Clinica? Clinica { get; set; }

        public string Medico_ID { get; set; }

        public Medico? Medico { get; set; }

        [Required]
        [StringLength(100)]
        public string Especialidade { get; set; }

        [Required]
        public DateTime DataConsulta { get; set; }
        [Required]
        [StringLength(150)]
        public string Paciente { get; set; }

        [Required]
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
