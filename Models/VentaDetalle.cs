using System.ComponentModel.DataAnnotations;

public class VentaDetalle
{
    [Key]
    public int idVentaDetalle { get; set; }
    public int idVenta { get; set;}
    public int idProducto { get; set;}
    public string? Descripcion { get; set;}
    public int Cantidad { get; set;}
    public double Importe {get; set;}
    public double Precio {get; set;}

}