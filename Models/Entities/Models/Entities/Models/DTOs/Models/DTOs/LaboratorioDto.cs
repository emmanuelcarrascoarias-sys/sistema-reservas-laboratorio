namespace LabReservation.API.Models.DTOs
{
    public class LaboratorioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Edificio { get; set; } = string.Empty;
        public int NumeroSalon { get; set; }
        public int Capacidad { get; set; }
    }
}
