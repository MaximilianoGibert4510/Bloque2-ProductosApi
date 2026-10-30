using Productos_DataAccessLayer.Models;

namespace Productos_DataAccessLayer.Repositories
{
    public interface IProductoRepository
    {
        List<Producto> ObtenerTodos();
        Producto? ObtenerPorId(int id);
        Producto Agregar(Producto producto);
        bool Actualizar(Producto producto);
        bool Eliminar(int id);
    }
}
