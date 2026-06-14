namespace LabReservation.API.Models.DTOs
{
    public class EstudianteDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Matricula { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
    }
}
