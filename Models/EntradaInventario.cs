using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class EntradaInventario
{
    [Key]
    public int idEntrada { get; set; }
    [Required(ErrorMessage = "El idProducto es necesario para completar la acción")]
    public int idProducto { get; set;}
    [Required(ErrorMessage = "La fecha es requerida")]
    public DateTime Fecha { get; set;} = DateTime.Now;

    [Range(2, int.MaxValue, ErrorMessage = "La cantidad es necesaria. y debe ser mayor que {1} y menor que {2}")]
    public int Cantidad { get; set;}
}