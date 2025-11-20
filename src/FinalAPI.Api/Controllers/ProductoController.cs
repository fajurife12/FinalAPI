using Microsoft.AspNetCore.Mvc;
using MiProyectoAPI.Api.Models;

namespace MiProyectoAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private static List<Producto> productos = new()
        {
            new Producto { Id = 1, Nombre = "Laptop", Precio = 999.99m, Stock = 5 },
            new Producto { Id = 2, Nombre = "Mouse", Precio = 29.99m, Stock = 50 },
            new Producto { Id = 3, Nombre = "Teclado", Precio = 79.99m, Stock = 30 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Producto>> GetProductos()
        {
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public ActionResult<Producto> GetProducto(int id)
        {
            var producto = productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
                return NotFound();
            return Ok(producto);
        }

        [HttpPost]
        public ActionResult<Producto> CreateProducto([FromBody] Producto producto)
        {
            if (string.IsNullOrEmpty(producto.Nombre) || producto.Precio <= 0)
                return BadRequest("Datos inválidos");

            producto.Id = productos.Max(p => p.Id) + 1;
            productos.Add(producto);
            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProducto(int id, [FromBody] Producto producto)
        {
            var existente = productos.FirstOrDefault(p => p.Id == id);
            if (existente == null)
                return NotFound();

            existente.Nombre = producto.Nombre;
            existente.Precio = producto.Precio;
            existente.Stock = producto.Stock;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProducto(int id)
        {
            var producto = productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
                return NotFound();

            productos.Remove(producto);
            return NoContent();
        }
    }
}