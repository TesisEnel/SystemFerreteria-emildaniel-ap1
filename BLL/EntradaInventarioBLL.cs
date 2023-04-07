using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class EntradaInventarioBLL {
    private readonly Context _contexto;

    public EntradaInventarioBLL(Context contexto) {
        _contexto = contexto;
    }

    public bool Existe(int entradaId) {
        return _contexto.entradaInventario.Any(o => o.idEntrada == entradaId);
    }

    public bool Inserta(EntradaInventario entradaInventario) {

        var producto = _contexto.producto.Find(entradaInventario.idProducto);
        if (producto != null) {
            producto.Cantidad += entradaInventario.Cantidad;
            _contexto.Entry(producto).State = EntityState.Modified;
        }
        _contexto.entradaInventario.Add(entradaInventario);
        return _contexto.SaveChanges() > 0;
    }

private bool Modificar(EntradaInventario entradaInventario) {
    var entradaExistente = _contexto.entradaInventario.Find(entradaInventario.idEntrada);
    if (entradaExistente != null) {
        var producto = _contexto.producto.Find(entradaExistente.idProducto);
        if (producto != null) {
            producto.Cantidad -= entradaExistente.Cantidad;
            producto.Cantidad += entradaInventario.Cantidad;
            _contexto.Entry(producto).State = EntityState.Modified;
        }
        _contexto.Entry(entradaExistente).CurrentValues.SetValues(entradaInventario);
        return _contexto.SaveChanges() > 0;
    }
    return false;
}


    public bool Guardar(EntradaInventario entradaInventario) {
        if (!Existe(entradaInventario.idEntrada))
            return Inserta(entradaInventario);
        else
            return Modificar(entradaInventario);
    }

    public bool Eliminar(int entradaId) {
        var entradaAEliminar = _contexto.entradaInventario.Where(o => o.idEntrada == entradaId).SingleOrDefault();
        if (entradaAEliminar != null) {
            var producto = _contexto.producto.Find(entradaAEliminar.idProducto);
                if (producto != null && _contexto.entradaInventario.Any(o => o.idEntrada == entradaId)) {
                    producto.Cantidad -= entradaAEliminar.Cantidad;
                    _contexto.Entry(producto).State = EntityState.Modified;
                    _contexto.SaveChanges();
                }
            _contexto.Entry(entradaAEliminar).State = EntityState.Deleted;
            return _contexto.SaveChanges() > 0;
        }
        return false;
    }
    public EntradaInventario? Buscar(int entradaId) {
        return _contexto.entradaInventario.Where(o => o.idEntrada == entradaId).AsNoTracking().SingleOrDefault();
    }

    public List<EntradaInventario> GetList(Expression<Func<EntradaInventario, bool>> criterio) {
        return _contexto.entradaInventario.AsNoTracking().Where(criterio).ToList();
    }
}
