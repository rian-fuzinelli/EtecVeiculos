using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EtecVeiculos.Api.Models;

public class Modelo
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Nome")]
    public string Nome { get; set; }

    [Required]
    public int MarcaId { get; set; }
    [ForeignKey("MarcaId")]
    public Marca Marca { get; set; }        
}
