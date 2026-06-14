namespace LabReservation.API.Models.Entities
{
    public class Reserva
    {
        public int Id { get; set; }
        public DateTime FechaReserva { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Proposito { get; set; } = string.Empty;
        public int EstudianteId { get; set; }
        public Estudiante? Estudiante { get; set; }
        public int ComputadoraId { get; set; }
        public Computadora? Computadora { get; set; }
    }
}
