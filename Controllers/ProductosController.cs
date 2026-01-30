using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaOnlineAPI.Models;

namespace TiendaOnlineAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly TiendaContext _context;
        public ProductosController(TiendaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.Include(p => p.Categoria).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.ProductoId == id);
            if (producto == null) return NotFound();
            return producto;
        }

        //Intento de hacer un post para agregar productos (lo difícil es ver que se agregue la categoría)
        [HttpPost]
        public IActionResult CreateProducto([FromBody] Producto producto)
        {
            if (producto == null) return BadRequest();
            _context.Productos.Add(producto);
            _context.SaveChanges();
            return Ok(producto);
        }

        [HttpPut("{id}")] //Indica que el id vendrá en la URL para hacer el put (update)
        public IActionResult UpdateProducto(int id, [FromBody] Producto producto) //FromBody indica que el producto vendrá en el cuerpo de la petición
        {
            if (id != producto.ProductoId) return BadRequest();

            var existingProducto = _context.Productos.Find(id);
            if (existingProducto == null) return NotFound();

            existingProducto.Nombre = producto.Nombre;
            existingProducto.Descripcion = producto.Descripcion;
            existingProducto.Precio = producto.Precio;
            existingProducto.UrlImagen = producto.UrlImagen;
            existingProducto.CategoriaId = producto.CategoriaId;

            _context.SaveChanges();
            return Ok(existingProducto); //Regresa el producto actualizado
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateProductoPartial(int id, [FromBody] Producto producto)
        {
            //esta es importante para que no falle si el id del producto en el cuerpo no coincide con el de la URL
            var existingProducto = _context.Productos.Find(id);
            if (existingProducto == null) return NotFound();

            if (!string.IsNullOrEmpty(producto.Nombre))
                existingProducto.Nombre = producto.Nombre;
            if (!string.IsNullOrEmpty(producto.Descripcion))
                existingProducto.Descripcion = producto.Descripcion;
            if (producto.Precio > 0)
                existingProducto.Precio = producto.Precio;
            if (!string.IsNullOrEmpty(producto.UrlImagen))
                existingProducto.UrlImagen = producto.UrlImagen;
            if (producto.CategoriaId > 0)
                existingProducto.CategoriaId = producto.CategoriaId;

            _context.SaveChanges();
            return Ok(existingProducto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProducto(int id)
        {
            var existingProducto = _context.Productos.Find(id);
            if (existingProducto == null) return NotFound();

            _context.Productos.Remove(existingProducto);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
