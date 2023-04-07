using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Producto
{
    [Key]
    public int IdProducto { get; set; }

    [Required(ErrorMessage = "El Nombre debe ser relleno.")]
    public string? Nombre { get; set;}

    [Range(1, double.MaxValue, ErrorMessage = "El precio debe estar en el rango valido {1} hasta {2}.")]
    public double Precio { get; set;}
    [Range(1, double.MaxValue, ErrorMessage = "El Costo debe estar en el rango valido {1} hasta {2}.")]
    public double Costo { get; set;}

    [Range(1, int.MaxValue, ErrorMessage = "La cantidad es necesaria y debe estar en el rango valido {1} hasta {2}.")]
    public int Cantidad { get; set;}

    [Required(ErrorMessage = "La categoria debe ser necesario para registrarlo.")]
    public int IdCategoria { get; set; }
    
    [Required(ErrorMessage = "La fecha es requerida")]
    public DateTime Fecha { get; set;} = DateTime.Now;
}