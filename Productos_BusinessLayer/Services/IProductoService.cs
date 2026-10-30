using Productos_BusinessLayer.DTOs;

namespace Productos_BusinessLayer.Services
{
    public interface IProductoService
    {
        List<ProductoDto> ObtenerTodos();
        ProductoDto? ObtenerPorId(int id);
        ProductoDto Crear(CreateProductoDto createProductoDto);
        ProductoDto? Actualizar(int id, CreateProductoDto createProductoDto);
        bool Eliminar(int id);
    }
}
