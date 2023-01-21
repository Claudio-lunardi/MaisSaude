﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaisSaude.Models
{
    public class Dependente
    {
        [Key]
        [Required]
        [StringLength(14)]
        [DisplayName("CPF do dependente")]
        public string CPFDependente { get; set; }
        [Required]
        [StringLength(14)]
        [DisplayName("CPF do Títular")]
        public string CPFTitular { get; set; }
        public Titular? Titular { get; set; }
        [Required]
        [StringLength(50)]
        public string RG { get; set; }
        [Required]
        [StringLength(150)]
        public string Nome { get; set; }
        [Required]
        [DisplayName("Data de nascimento")]
        public DateTime DataNascimento { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }
        [StringLength(15)]
        public string? Telefone { get; set; }
        [Required]
        [StringLength(15)]
        public string Celular { get; set; }
        [Required]
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        [StringLength(50)]
        [DisplayName("Tipo de permisão")]
        public string TipoPermissao { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Usuário")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório")]
        public string Senha { get; set; }
    }
}
