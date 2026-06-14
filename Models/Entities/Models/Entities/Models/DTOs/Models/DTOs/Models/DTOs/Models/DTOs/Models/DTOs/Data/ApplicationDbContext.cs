using LabReservation.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabReservation.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Laboratorio> Laboratorios { get; set; }
        public DbSet<Computadora> Computadoras { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
    }
}
