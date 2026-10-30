using Productos_DataAccessLayer.Models;

namespace Productos_DataAccessLayer.Data
{
    // Almacenamiento en memoria compartido. Se registra como Singleton para que
    // los productos persistan entre solicitudes HTTP, aunque el repositorio sea Scoped.
    public class AlmacenProductos
    {
        private int _ultimoId;

        public List<Producto> Productos { get; }

        public AlmacenProductos()
        {
            // Datos de prueba (seed data)
            Productos = new List<Producto>
            {
                new Producto { Id = 1, Nombre = "Notebook", Precio = 850000m, Categoria = "Tecnología", Activo = true },
                new Producto { Id = 2, Nombre = "Mouse inalámbrico", Precio = 15000m, Categoria = "Tecnología", Activo = true },
                new Producto { Id = 3, Nombre = "Silla de oficina", Precio = 120000m, Categoria = "Muebles", Activo = true }
            };
            _ultimoId = 3;
        }

        public int ObtenerSiguienteId()
        {
            _ultimoId++;
            return _ultimoId;
        }
    }
}
