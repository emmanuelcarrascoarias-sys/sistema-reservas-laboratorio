namespace LabReservation.API.Models.Entities
{
    public class Computadora
    {
        public int Id { get; set; }
        public string Numero { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public bool EstaDisponible { get; set; } = true;
        public int LaboratorioId { get; set; }
        public Laboratorio? Laboratorio { get; set; }
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
