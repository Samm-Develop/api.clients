using api.clients.Models;
using api.clients.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace api.clients.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("store-procedure")]
        public async Task<IActionResult> GetClientesPaginadosSP(int pageNumber = 1, int pageSize = 10)
        {
            SqlParameter[] parametros = [
                new("@PageNumber", pageNumber),
                new("@PageSize", pageSize) ];

            var clientes = await _context.Database
                .SqlQueryRaw<ClienteDTO>("EXEC GetClientesPaginados @PageNumber, @PageSize", parametros)
                .ToListAsync();

            return Ok(clientes);
        }

        [HttpGet("ef-core")]
        public async Task<IActionResult> GetClientesPaginadosEF(int pageNumber = 1, int pageSize = 10)
        {
            var clientes = await _context.Clientes
                .Include(c => c.Pais)
                .OrderBy(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new
                {
                    c.Id,
                    c.Nombre,
                    c.Telefono,
                    PaisNombre = c.Pais!.Nombre
                })
                .ToListAsync();

            return Ok(clientes);
        }
    }
}