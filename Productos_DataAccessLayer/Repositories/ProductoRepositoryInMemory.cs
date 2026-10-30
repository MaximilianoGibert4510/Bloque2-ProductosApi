using Productos_DataAccessLayer.Data;
using Productos_DataAccessLayer.Models;

namespace Productos_DataAccessLayer.Repositories
{
    public class ProductoRepositoryInMemory : IProductoRepository
    {
        private readonly AlmacenProductos _almacen;

        // Inyección de dependencias del almacenamiento compartido (Singleton)
        public ProductoRepositoryInMemory(AlmacenProductos almacen)
        {
            _almacen = almacen;
        }

        public List<Producto> ObtenerTodos()
        {
            return _almacen.Productos.ToList();
        }

        public Producto? ObtenerPorId(int id)
        {
            return _almacen.Productos.FirstOrDefault(p => p.Id == id);
        }

        public Producto Agregar(Producto producto)
        {
            producto.Id = _almacen.ObtenerSiguienteId();
            _almacen.Productos.Add(producto);
            return producto;
        }

        public bool Actualizar(Producto producto)
        {
            var existente = _almacen.Productos.FirstOrDefault(p => p.Id == producto.Id);
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
            var existente = _almacen.Productos.FirstOrDefault(p => p.Id == id);
            if (existente == null)
            {
                return false;
            }

            _almacen.Productos.Remove(existente);
            return true;
        }
    }
}
