using LabReservation.API.Data;
using LabReservation.API.Models.DTOs;
using LabReservation.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabReservation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComputadorasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComputadorasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComputadoraDto>>> ObtenerTodos()
        {
            var lista = await _context.Computadoras.ToListAsync();
            var resultado = lista.Select(c => new ComputadoraDto
            {
                Id = c.Id,
                Numero = c.Numero,
                Marca = c.Marca,
                EstaDisponible = c.EstaDisponible,
                LaboratorioId = c.LaboratorioId
            });
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComputadoraDto>> ObtenerPorId(int id)
        {
            var comp = await _context.Computadoras.FindAsync(id);
            if (comp == null) return NotFound();

            return Ok(new ComputadoraDto
            {
                Id = comp.Id,
                Numero = comp.Numero,
                Marca = comp.Marca,
                EstaDisponible = comp.EstaDisponible,
                LaboratorioId = comp.LaboratorioId
            });
        }

        [HttpPost]
        public async Task<ActionResult<ComputadoraDto>> Crear(ComputadoraDto dto)
        {
            var comp = new Computadora
            {
                Numero = dto.Numero,
                Marca = dto.Marca,
                EstaDisponible = dto.EstaDisponible,
                LaboratorioId = dto.LaboratorioId
            };
            _context.Computadoras.Add(comp);
            await _context.SaveChangesAsync();
            dto.Id = comp.Id;
            return CreatedAtAction(nameof(ObtenerPorId), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ComputadoraDto dto)
        {
            var comp = await _context.Computadoras.FindAsync(id);
            if (comp == null) return NotFound();

            comp.Numero = dto.Numero;
            comp.Marca = dto.Marca;
            comp.EstaDisponible = dto.EstaDisponible;
            comp.LaboratorioId = dto.LaboratorioId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var comp = await _context.Computadoras.FindAsync(id);
            if (comp == null) return NotFound();

            _context.Computadoras.Remove(comp);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
