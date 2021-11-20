using ClinicaProyect_DPWA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ClinicaProyect_DPWA.Context
{
    public class ClinicaContext : DbContext
    {
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Reservacion> Reservaciones { get; set; }
        public DbSet<EstadoReservacion> EstadoReservaciones { get; set; }
        public DbSet<EstadoPago> EstadoPagos { get; set; }
        public DbSet<CategoriaMedico> CategoriaMedicos { get; set; }
        public DbSet<Correo> Correos { get; set; }
    }
}