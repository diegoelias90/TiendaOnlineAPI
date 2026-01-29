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
        public async Task<ActionResult<IEnumerable<TiendaContext.Producto>>> GetProductos() 
        {
            return await _context.Productos.Include(p => p.Categoria).ToListAsync();
        }

        public async Task<ActionResult<TiendaContext.Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.ProductoId == id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }
    }
}
