using System.ComponentModel.DataAnnotations;

namespace EtecVeiculos.Api.DTOs;

public class ModeloVM
{
    [Required]
    [StringLength(30)]
    [Display(Name = "Nome")]
    public string Name { get; set; }
}
