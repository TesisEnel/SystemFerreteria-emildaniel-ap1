using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
public class CategoriaBLL{
    private Context _contexto;
    
    public CategoriaBLL(Context contexto){
        _contexto = contexto;
    }
    public bool Existe(int categoriaId){
        return _contexto.categoria.Any(o => o.IdCategoria == categoriaId);
    }
    public bool Inserta(Categoria categoria){
        _contexto.categoria.Add(categoria);
        return _contexto.SaveChanges() > 0;
    }
    private bool Modificar(Categoria categoria){
        var categoriaExistente = _contexto.categoria.Find(categoria.IdCategoria);
        if(categoriaExistente != null){
            _contexto.Entry(categoriaExistente).CurrentValues.SetValues(categoria);
            return _contexto.SaveChanges() > 0;
        }
        return false;
    }
    public bool Guardar(Categoria categoria){
        if(!Existe(categoria.IdCategoria))
            return this.Inserta(categoria);
        else
            return this.Modificar(categoria);
    }
    public bool Eliminar(int categoria){
        var CategoriaAEliminar = _contexto.categoria.Where(o=> o.IdCategoria == categoria).SingleOrDefault();
        if(CategoriaAEliminar!=null){
            _contexto.Entry(CategoriaAEliminar).State = EntityState.Deleted;
            return _contexto.SaveChanges() > 0;
        }
        return false;
    }
    public Categoria? Buscar(int categoriaId){
        return _contexto.categoria.Where(o => o.IdCategoria == categoriaId).AsNoTracking().SingleOrDefault();
    }
    public List<Categoria> GetList(Expression<Func<Categoria, bool>> criterio){
        return _contexto.categoria.AsNoTracking().Where(criterio).ToList();
    }
    
}