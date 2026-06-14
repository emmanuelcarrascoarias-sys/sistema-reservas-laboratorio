using LabReservation.API.Data;
using LabReservation.API.Models.DTOs;
using LabReservation.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabReservation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReservasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservaDto>>> ObtenerTodos()
        {
            var lista = await _context.Reservas.ToListAsync();
            var resultado = lista.Select(r => new ReservaDto
            {
                Id = r.Id,
                FechaReserva = r.FechaReserva,
                HoraInicio = r.HoraInicio,
                HoraFin = r.HoraFin,
                Proposito = r.Proposito,
                EstudianteId = r.EstudianteId,
                ComputadoraId = r.ComputadoraId
            });
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservaDto>> ObtenerPorId(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return NotFound();

            return Ok(new ReservaDto
            {
                Id = reserva.Id,
                FechaReserva = reserva.FechaReserva,
                HoraInicio = reserva.HoraInicio,
                HoraFin = reserva.HoraFin,
                Proposito = reserva.Proposito,
                EstudianteId = reserva.EstudianteId,
                ComputadoraId = reserva.ComputadoraId
            });
        }

        [HttpPost]
        public async Task<ActionResult<ReservaDto>> Crear(ReservaDto dto)
        {
            var reserva = new Reserva
            {
                FechaReserva = dto.FechaReserva,
                HoraInicio = dto.HoraInicio,
                HoraFin = dto.HoraFin,
                Proposito = dto.Proposito,
                EstudianteId = dto.EstudianteId,
                ComputadoraId = dto.ComputadoraId
            };
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
            dto.Id = reserva.Id;
            return CreatedAtAction(nameof(ObtenerPorId), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ReservaDto dto)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return NotFound();

            reserva.FechaReserva = dto.FechaReserva;
            reserva.HoraInicio = dto.HoraInicio;
            reserva.HoraFin = dto.HoraFin;
            reserva.Proposito = dto.Proposito;
            reserva.EstudianteId = dto.EstudianteId;
            reserva.ComputadoraId = dto.ComputadoraId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return NotFound();

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
