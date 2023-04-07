using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class VentaBLL {
    private readonly Context _contexto;

    public VentaBLL(Context contexto) {
        _contexto = contexto;
    }

    public bool Existe(int ventaId) {
        return _contexto.venta.Any(o => o.idVenta == ventaId);
    }
    public bool Inserta(Venta venta) {
        InsertarDetalle(venta);
        _contexto.venta.Add(venta);
        bool paso = _contexto.SaveChanges() >0;
        _contexto.Entry(venta).State = EntityState.Detached;
        return paso; 
    }
    public bool Guardar(Venta venta)
    {
        if (!Existe(venta.idVenta))
            return Inserta(venta);
        else
            return Modificar(venta);
    }
    public bool Eliminar(Venta venta){
        foreach (var detalle in venta.ventaDetalle) {
            var producto = _contexto.producto.Find(detalle.idProducto);
            if (producto != null) {
                producto.Cantidad += detalle.Cantidad;
                _contexto.Entry(producto).State = EntityState.Modified;
                _contexto.SaveChanges();
            }
        }
        _contexto.RemoveRange(venta.ventaDetalle);
        _contexto.Entry(venta).State = EntityState.Deleted;
        bool paso = _contexto.SaveChanges() >0;
        _contexto.Entry(venta).State = EntityState.Detached;
        return paso; 
    }
    public Venta? Buscar(int ventaId) {
        return _contexto.venta.Include(o => o.ventaDetalle).Where(o => o.idVenta == ventaId).AsNoTracking().SingleOrDefault();
    }
    public List<Venta> GetList(Expression<Func<Venta, bool>> criterio) {
        return _contexto.venta.AsNoTracking().Where(criterio).ToList();
    }
    void InsertarDetalle(Venta venta)
    {
        if (venta.ventaDetalle == null)
        {
            venta.ventaDetalle = new List<VentaDetalle>(); 
        }
        foreach (var item in venta.ventaDetalle)
        {
            var producto =  _contexto.producto.Find(item.idProducto);
            if (producto != null)
            {
                producto.Cantidad -= item.Cantidad;
                _contexto.Entry(producto).State = EntityState.Modified;
                _contexto.SaveChanges();
            }
        }
    }
    private bool Modificar(Venta venta)
    {
        var detallesOriginales = _contexto.venta.AsNoTracking().Where(o => o.idVenta== venta.idVenta)
                .Include(o =>  o.ventaDetalle)
                .AsNoTracking()
                .SingleOrDefault();

    foreach (var detalle in detallesOriginales!.ventaDetalle)
    {
        var producto = _contexto.producto.Find(detalle.idProducto);
        if(producto!=null){
            producto.Cantidad += detalle.Cantidad;
            _contexto.Entry(producto).State = EntityState.Modified;
        }
    }
    _contexto.Entry(detallesOriginales).State = EntityState.Detached;

    foreach (var detalle in venta.ventaDetalle)
    {
        var producto = _contexto.producto.Find(detalle.idProducto);
        if(producto!=null){
            producto.Cantidad -= detalle.Cantidad;
            _contexto.Entry(producto).State = EntityState.Modified;
        }
    }
    _contexto.Entry(detallesOriginales).State = EntityState.Detached;

    var DetalleEliminar = _contexto.Set<VentaDetalle>().Where(o => o.idVenta == venta.idVenta);
    _contexto.Set<VentaDetalle>().RemoveRange(DetalleEliminar);
    _contexto.Set<VentaDetalle>().AddRange(venta.ventaDetalle);
    _contexto.Entry(venta).State = EntityState.Modified;

    bool paso = _contexto.SaveChanges() >0;
    _contexto.Entry(venta).State = EntityState.Detached;
    return paso; 
    }
}
