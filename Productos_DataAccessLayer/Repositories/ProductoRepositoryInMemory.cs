using Productos_DataAccessLayer.Data;
using Productos_DataAccessLayer.Models;

namespace Productos_DataAccessLayer.Repositories
{
    public class ProductoRepositoryInMemory : IProductoRepository
    {
        private readonly AlmacenProductos _almacen;
        private readonly List<Producto> _productos;

        // Inyección de dependencias del almacenamiento compartido (Singleton).
        // La lista es la misma en todas las solicitudes, aunque el repositorio sea Scoped.
        public ProductoRepositoryInMemory(AlmacenProductos almacen)
        {
            _almacen = almacen;
            _productos = almacen.Productos;
        }

        public List<Producto> ObtenerTodos()
        {
            return _productos.ToList();
        }

        public Producto? ObtenerPorId(int id)
        {
            return _productos.FirstOrDefault(p => p.Id == id);
        }

        public Producto Agregar(Producto producto)
        {
            producto.Id = _almacen.ObtenerSiguienteId();
            _productos.Add(producto);
            return producto;
        }

        public bool Actualizar(Producto producto)
        {
            var existente = _productos.FirstOrDefault(p => p.Id == producto.Id);
            if (existente == null)
            {
                return false;
            }

            existente.Nombre = producto.Nombre;
            existente.Precio = producto.Precio;
            existente.Categoria = producto.Categoria;
            existente.Activo = producto.Activo;
            return true;
        }

        public bool Eliminar(int id)
        {
            var existente = _productos.FirstOrDefault(p => p.Id == id);
            if (existente == null)
            {
                return false;
            }

            _productos.Remove(existente);
            return true;
        }
    }
}
