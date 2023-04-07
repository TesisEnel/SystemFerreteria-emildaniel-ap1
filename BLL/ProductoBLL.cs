using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class ProductoBLL {
    private readonly Context _contexto;

    public ProductoBLL(Context contexto) {
        _contexto = contexto;
    }

    public bool Existe(int productoId) {
        return _contexto.producto.Any(o => o.IdProducto == productoId);
    }

    public bool Inserta(Producto producto) {
        _contexto.producto.Add(producto);
        return _contexto.SaveChanges() > 0;
    }

    private bool Modificar(Producto producto) {
        var productoExistente = _contexto.producto.Find(producto.IdProducto);
        if (productoExistente != null) {
            _contexto.Entry(productoExistente).CurrentValues.SetValues(producto);
            return _contexto.SaveChanges() > 0;
        }
        return false;
    }

    public bool Guardar(Producto producto) {
        if (!Existe(producto.IdProducto))
            return Inserta(producto);
        else
            return Modificar(producto);
    }

    public bool Eliminar(int productoId) {
        var productoAEliminar = _contexto.producto.Where(o => o.IdProducto == productoId).SingleOrDefault();
        if (productoAEliminar != null) {
            _contexto.Entry(productoAEliminar).State = EntityState.Deleted;
            return _contexto.SaveChanges() > 0;
        }
        return false;
    }

    public Producto? Buscar(int productoId) {
        return _contexto.producto.Where(o => o.IdProducto == productoId).AsNoTracking().SingleOrDefault();
    }

    public List<Producto> GetList(Expression<Func<Producto, bool>> criterio) {
        return _contexto.producto.AsNoTracking().Where(criterio).ToList();
    }
}
