using LabReservation.API.Data;
using LabReservation.API.Models.DTOs;
using LabReservation.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabReservation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LaboratoriosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LaboratoriosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LaboratorioDto>>> ObtenerTodos()
        {
            var lista = await _context.Laboratorios.ToListAsync();
            var resultado = lista.Select(l => new LaboratorioDto
            {
                Id = l.Id,
                Nombre = l.Nombre,
                Edificio = l.Edificio,
                NumeroSalon = l.NumeroSalon,
                Capacidad = l.Capacidad
            });
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LaboratorioDto>> ObtenerPorId(int id)
        {
            var lab = await _context.Laboratorios.FindAsync(id);
            if (lab == null) return NotFound();

            return Ok(new LaboratorioDto
            {
                Id = lab.Id,
                Nombre = lab.Nombre,
                Edificio = lab.Edificio,
                NumeroSalon = lab.NumeroSalon,
                Capacidad = lab.Capacidad
            });
        }

        [HttpPost]
        public async Task<ActionResult<LaboratorioDto>> Crear(LaboratorioDto dto)
        {
            var lab = new Laboratorio
            {
                Nombre = dto.Nombre,
                Edificio = dto.Edificio,
                NumeroSalon = dto.NumeroSalon,
                Capacidad = dto.Capacidad
            };
            _context.Laboratorios.Add(lab);
            await _context.SaveChangesAsync();
            dto.Id = lab.Id;
            return CreatedAtAction(nameof(ObtenerPorId), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, LaboratorioDto dto)
        {
            var lab = await _context.Laboratorios.FindAsync(id);
            if (lab == null) return NotFound();

            lab.Nombre = dto.Nombre;
            lab.Edificio = dto.Edificio;
            lab.NumeroSalon = dto.NumeroSalon;
            lab.Capacidad = dto.Capacidad;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var lab = await _context.Laboratorios.FindAsync(id);
            if (lab == null) return NotFound();

            _context.Laboratorios.Remove(lab);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
