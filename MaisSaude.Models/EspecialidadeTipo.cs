using System.ComponentModel.DataAnnotations;


namespace MaisSaude.Models;


public class EspecialidadeTipo
{
    [Key]
    public int ID { get; set; }

    [StringLength(150)]
    public string Especialidade { get; set; }


}
