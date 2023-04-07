using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Categoria
{
    [Key]
    public int IdCategoria { get; set; }
    [Required(ErrorMessage = "El Nombre debe ser relleno.")]
    public string? Nombre { get; set;}
    [Required(ErrorMessage = "La descripcion debe ser necesario para registrarlo.")]
    public string? Descripcion { get; set;}
    [Required(ErrorMessage = "La fecha es requerida")]
    public DateTime Fecha { get; set;} = DateTime.Now;
}