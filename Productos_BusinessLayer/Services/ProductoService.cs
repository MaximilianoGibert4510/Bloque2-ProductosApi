using Productos_BusinessLayer.DTOs;
using Productos_DataAccessLayer.Models;
using Productos_DataAccessLayer.Repositories;

namespace Productos_BusinessLayer.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        // Inyección de dependencias del repositorio
        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public List<ProductoDto> ObtenerTodos()
        {
            return _productoRepository.ObtenerTodos()
                .Select(p => ConvertirADto(p))
                .ToList();
        }

        public ProductoDto? ObtenerPorId(int id)
        {
            var producto = _productoRepository.ObtenerPorId(id);
            if (producto == null)
            {
                return null;
            }

            return ConvertirADto(producto);
        }

        public ProductoDto Crear(CreateProductoDto createProductoDto)
        {
            var producto = new Producto
            {
                Nombre = createProductoDto.Nombre,
                Precio = createProductoDto.Precio,
                Categoria = createProductoDto.Categoria,
                Activo = true // Por defecto, el producto está activo
            };

            var productoCreado = _productoRepository.Agregar(producto);
            return ConvertirADto(productoCreado);
        }

        public ProductoDto? Actualizar(int id, CreateProductoDto createProductoDto)
        {
            var existente = _productoRepository.ObtenerPorId(id);
            if (existente == null)
            {
                return null;
            }

            var producto = new Producto
            {
                Id = id,
                Nombre = createProductoDto.Nombre,
                Precio = createProductoDto.Precio,
                Categoria = createProductoDto.Categoria,
                Activo = existente.Activo // El DTO no incluye Activo: se conserva el valor actual
            };

            _productoRepository.Actualizar(producto);
            return ConvertirADto(producto);
        }

        public bool Eliminar(int id)
        {
            return _productoRepository.Eliminar(id);
        }

        private static ProductoDto ConvertirADto(Producto producto)
        {
            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Categoria = producto.Categoria,
                Activo = producto.Activo
            };
        }
    }
}
