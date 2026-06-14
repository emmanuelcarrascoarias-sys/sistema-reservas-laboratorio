using LabReservation.API.Data;
using LabReservation.API.Models.DTOs;
using LabReservation.API.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabReservation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public ReservasController(ApplicationDbContext context) { _context = context; }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservaDto>>> GetAll()
    {
        var reservas = await _context.Reservas
            .Select(r => new ReservaDto { Id = r.Id, EstudianteId = r.EstudianteId, LaboratorioId = r.LaboratorioId, HoraInicio = r.HoraInicio, HoraFin = r.HoraFin, Estado = r.Estado.ToString(), Proposito = r.Proposito, CreadoEn = r.CreadoEn })
            .ToListAsync();
        return Ok(reservas);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ReservaDto>> GetById(int id)
    {
        var reserva = await _context.Reservas
            .Where(r => r.Id == id)
            .Select(r => new ReservaDto { Id = r.Id, EstudianteId = r.EstudianteId, LaboratorioId = r.LaboratorioId, HoraInicio = r.HoraInicio, HoraFin = r.HoraFin, Estado = r.Estado.ToString(), Proposito = r.Proposito, CreadoEn = r.CreadoEn })
            .FirstOrDefaultAsync();
        if (reserva is null) return NotFound();
        return Ok(reserva);
    }

    [HttpPost]
    public async Task<ActionResult<ReservaDto>> Create(ReservaDto dto)
    {
        var estudianteExiste = await _context.Estudiantes.AnyAsync(e => e.Id == dto.EstudianteId);
        if (!estudianteExiste) return BadRequest("El estudiante seleccionado no existe.");
        var labExiste = await _context.Laboratorios.AnyAsync(l => l.Id == dto.LaboratorioId);
        if (!labExiste) return BadRequest("El laboratorio seleccionado no existe.");
        if (dto.HoraFin <= dto.HoraInicio) return BadRequest("La hora de fin debe ser mayor a la hora de inicio.");
        if (!Enum.TryParse<EstadoReserva>(dto.Estado, true, out var estado))
            return BadRequest("Estado de reserva no valido.");
        var reserva = new Reserva { EstudianteId = dto.EstudianteId, LaboratorioId = dto.LaboratorioId, HoraInicio = dto.HoraInicio, HoraFin = dto.HoraFin, Estado = estado, Proposito = dto.Proposito, CreadoEn = DateTime.UtcNow };
        _context.Reservas.Add(reserva);
        await _context.SaveChangesAsync();
        dto.Id = reserva.Id;
        dto.Estado = reserva.Estado.ToString();
        dto.CreadoEn = reserva.CreadoEn;
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ReservaDto dto)
    {
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva is null) return NotFound();
        var estudianteExiste = await _context.Estudiantes.AnyAsync(e => e.Id == dto.EstudianteId);
        if (!estudianteExiste) return BadRequest("El estudiante seleccionado no existe.");
        var labExiste = await _context.Laboratorios.AnyAsync(l => l.Id == dto.LaboratorioId);
        if (!labExiste) return BadRequest("El laboratorio seleccionado no existe.");
        if (dto.HoraFin <= dto.HoraInicio) return BadRequest("La hora de fin debe ser mayor a la hora de inicio.");
        if (!Enum.TryParse<EstadoReserva>(dto.Estado, true, out var estado))
            return BadRequest("Estado de reserva no valido.");
        reserva.EstudianteId = dto.EstudianteId;
        reserva.LaboratorioId = dto.LaboratorioId;
        reserva.HoraInicio = dto.HoraInicio;
        reserva.HoraFin = dto.HoraFin;
        reserva.Estado = estado;
        reserva.Proposito = dto.Proposito;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva is null) return NotFound();
        _context.Reservas.Remove(reserva);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
