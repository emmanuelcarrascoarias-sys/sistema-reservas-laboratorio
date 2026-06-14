namespace LabReservation.API.Models.Entities;

public class Estudiante
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string Matricula { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Carrera { get; set; } = string.Empty;
    public DateTime CreadoEn { get; set; }
    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
