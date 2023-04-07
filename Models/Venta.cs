using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Venta
{
    [Key]
    public int idVenta { get; set; }
    [Required(ErrorMessage = "La fecha es requerida")]
    public DateTime Fecha { get; set;} = DateTime.Now;
    [Range(1, double.MaxValue, ErrorMessage = "El Total debe estar en el rango valido {1} hasta {2}.")]
    public double Total { get; set;}
    public string? Concepto { get; set; }

    [ForeignKey("idVenta")]
    public List<VentaDetalle> ventaDetalle { get; set; } = new List<VentaDetalle>();
}