using Microsoft.AspNetCore.Mvc;
using Productos_BusinessLayer.DTOs;
using Productos_BusinessLayer.Services;

namespace Productos_PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        // Inyección de dependencias del servicio de productos
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        // Read
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var productos = _productoService.ObtenerTodos();
            return Ok(productos);
        }

        // Read by Id
        [HttpGet("{id:int}")]
        public IActionResult ObtenerPorId(int id)
        {
            var producto = _productoService.ObtenerPorId(id);
            if (producto == null)
            {
                return NotFound($"No se encontró un producto con el ID {id}.");
            }

            return Ok(producto);
        }

        // Create
        [HttpPost]
        public IActionResult Crear([FromBody] CreateProductoDto createProductoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productoCreado = _productoService.Crear(createProductoDto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = productoCreado.Id }, productoCreado);
        }

        // Update
        [HttpPut("{id:int}")]
        public IActionResult Actualizar(int id, [FromBody] CreateProductoDto createProductoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productoActualizado = _productoService.Actualizar(id, createProductoDto);
            if (productoActualizado == null)
            {
                return NotFound($"No se encontró un producto con el ID {id}.");
            }

            return Ok(productoActualizado);
        }

        // Delete
        [HttpDelete("{id:int}")]
        public IActionResult Eliminar(int id)
        {
            var eliminado = _productoService.Eliminar(id);
            if (!eliminado)
            {
                return NotFound($"No se encontró un producto con el ID {id}.");
            }

            return NoContent();
        }
    }
}
