using Microsoft.EntityFrameworkCore;
using Raptor.Api.Infraestructura.Modelos;
using Raptor.Api.Infraestructura.Models;
using Raptor.Dominio.Entidades;

namespace Raptor.Api.Infraestructura
{
    public class Context : DbContext
    {
        //Tabla
        public DbSet<TableModel> Tables { get; set; }
        public DbSet<SchemaModel> Schemas { get; set; }
        //Columna
        public DbSet<ColumnModel> Columns { get; set; }
        public DbSet<TypeModel> Types { get; set; }
        //PrimaryKey
        public DbSet<KeyConstraintModel> KeyConstraints { get; set; }
        //ForeignKey
        public DbSet<ForeignKeyModel> ForeignKeys { get; set; }
        public DbSet<ForeignKeyColumnModel> ForeignKeysColumn { get; set; }
        //Trigger
        public DbSet<TriggerModel> Triggers { get; set; }
        public DbSet<TriggerEventModel> TriggersColumn { get; set; }
        public DbSet<SqlModuleModel> SqlModules { get; set; }

        //Indice
        public DbSet<IndexModel> Indexes { get; set; }
        public DbSet<IndexColumnModel> IndexesColumn { get; set; }

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
