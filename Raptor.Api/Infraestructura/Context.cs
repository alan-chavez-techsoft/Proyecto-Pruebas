using Microsoft.EntityFrameworkCore;
using Raptor.Dominio.Entidades;

namespace Raptor.Api.Infraestructura
{
    public class Context : DbContext
    {
        public DbSet<Tabla> Tablas { get; set; }
        public DbSet<Columnas> Columnas { get; set; }
        public DbSet<PrimaryKey> PrimaryKeys { get; set; }
        public DbSet<ForeignKey> ForeignKeys { get; set; }
        public DbSet<Trigger> Triggers { get; set; }
        public DbSet<Indices> Indices { get; set; }
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
