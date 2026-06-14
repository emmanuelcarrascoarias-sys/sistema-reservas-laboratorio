using LabReservation.API.Data;
using LabReservation.API.Models.DTOs;
using LabReservation.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabReservation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EstudiantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstudianteDto>>> ObtenerTodos()
        {
            var estudiantes = await _context.Estudiantes.ToListAsync();
            var resultado = estudiantes.Select(e => new EstudianteDto
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Apellido = e.Apellido,
                Matricula = e.Matricula,
                Correo = e.Correo
            });
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EstudianteDto>> ObtenerPorId(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null) return NotFound();

            return Ok(new EstudianteDto
            {
                Id = estudiante.Id,
                Nombre = estudiante.Nombre,
                Apellido = estudiante.Apellido,
                Matricula = estudiante.Matricula,
                Correo = estudiante.Correo
            });
        }

        [HttpPost]
        public async Task<ActionResult<EstudianteDto>> Crear(EstudianteDto dto)
        {
            var estudiante = new Estudiante
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Matricula = dto.Matricula,
                Correo = dto.Correo
            };
            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();
            dto.Id = estudiante.Id;
            return CreatedAtAction(nameof(ObtenerPorId), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, EstudianteDto dto)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null) return NotFound();

            estudiante.Nombre = dto.Nombre;
            estudiante.Apellido = dto.Apellido;
            estudiante.Matricula = dto.Matricula;
            estudiante.Correo = dto.Correo;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null) return NotFound();

            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
