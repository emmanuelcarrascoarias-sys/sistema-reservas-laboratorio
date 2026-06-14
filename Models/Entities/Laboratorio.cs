namespace LabReservation.API.Models.Entities;

public enum TipoLaboratorio { Computo, Redes, Electronica, Multimedia }

public class Laboratorio
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public TipoLaboratorio Tipo { get; set; }
    public int Capacidad { get; set; }
    public string Ubicacion { get; set; } = string.Empty;
    public bool EstaDisponible { get; set; }
    public DateTime CreadoEn { get; set; }
    public ICollection<Computadora> Computadoras { get; set; } = new List<Computadora>();
    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
