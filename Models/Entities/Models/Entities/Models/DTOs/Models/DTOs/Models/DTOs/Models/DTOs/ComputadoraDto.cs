namespace LabReservation.API.Models.DTOs
{
    public class ComputadoraDto
    {
        public int Id { get; set; }
        public string Numero { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public bool EstaDisponible { get; set; }
        public int LaboratorioId { get; set; }
    }
}
