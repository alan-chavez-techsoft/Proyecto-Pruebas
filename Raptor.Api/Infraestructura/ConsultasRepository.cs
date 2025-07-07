using Microsoft.EntityFrameworkCore;
using Raptor.Dominio.Entidades;
using Raptor.Dominio.Repositories;

namespace Raptor.Api.Infraestructura
{
    public class ConsultasRepository(Context context) : IConsultasRepository
    {
        protected readonly Context _context = context;
        public async Task<List<Tabla>> ObtenerTablas()
        {
            var tablas = await _context.Tables.Where(x => x.Is_Ms_Shipped == false).ToListAsync();
            var schemas = await _context.Schemas.ToListAsync();
            return [.. tablas.Select(t =>
                new Tabla(t.Object_Id,
                t.Name,
                schemas.First(x => x.Schema_Id == t.Schema_Id).Name)
            )];
        }
        public async Task<List<Columna>> ObtenerColumnas()
        {
            var columnas = await _context.Columns.ToListAsync();
            var tipos = await _context.Types.ToListAsync();
            return [.. columnas.Select(col => 
            new Columna(col.Object_Id,
            col.Column_Id, 
            col.Name,
            col.Is_Nullable,
            tipos.First(x => x.User_Type_Id == col.User_Type_Id).Name,
            col.Max_Length,
            col.Precision,
            col.Scale,
            col.Default_Object_Id,
            col.Is_Identity)
            )];
        }
        public async Task<List<PrimaryKey>> ObtenerPrimaryKeys()
        {
            var keyConstraints = await _context.KeyConstraints
                .Where(x => x.Type == "PK").ToListAsync();
            var indexColumns = await _context.IndexesColumn.ToListAsync();
            return [.. from kc in keyConstraints
                    join ic in indexColumns on 
                    new {Object_Id = kc.Parent_Object_Id, Index_Id = kc.Unique_Index_Id} 
                    equals new { ic.Object_Id, ic.Index_Id}
                    select new PrimaryKey(ic.Object_Id, ic.Column_Id)];
        }
        public async Task<List<ForeignKey>> ObtenerForeignKeys(List<Tabla> tablas, List<Columna> columnas)
        {
            var foreignKey = await _context.ForeignKeys.ToListAsync();
            var foreignKeyColumn = await _context.ForeignKeysColumn.ToListAsync();
            //var columnas = await _context.Columns.ToListAsync();

            return [.. from fk in foreignKey
                   join fkc in foreignKeyColumn on fk.Object_Id equals fkc.Constraint_Object_Id
                   join rt in tablas on fkc.Referenced_Object_Id equals rt.Object_Id
                   join rc in columnas
                   on new { Object_Id = fkc.Referenced_Object_Id, Ordinal_Position = fkc.Referenced_Column_Id }
                   equals new { rc.Object_Id, rc.Ordinal_Position }
                   join pc in columnas
                   on new { Object_Id = fkc.Parent_Object_Id, Ordinal_Position = fkc.Parent_Column_Id }
                   equals new { pc.Object_Id, pc.Ordinal_Position }
                   select new ForeignKey(
                       fk.Parent_Object_Id,
                       fkc.Parent_Column_Id,
                       fk.Name,
                       rt.Table_Name,
                       rc.Column_Name,
                       pc.Column_Name)];
        }
        public async Task<List<Trigger>> ObtenerTriggers()
        {
            var triggers = await _context.Triggers
                .Where(x => !EF.Functions.Like(x.Name, "%SYM%") && x.Is_Ms_Shipped == false).ToListAsync();
            var triggerEvents = await _context.TriggersColumn.ToListAsync();
            var sqlModules = await _context.SqlModules.ToListAsync();

            return [.. triggers.Select(t =>
            {
                var te = triggerEvents.FirstOrDefault(x => x.Object_Id == t.Object_Id);
                var sm = sqlModules.FirstOrDefault(x => x.Object_Id == t.Object_Id);
                return new Trigger(t.Object_Id,
                    t.Name,
                    te?.Type_Desc ?? string.Empty,
                    sm?.Definition ?? string.Empty);
            })];
        }
        public async Task<List<Indice>> ObtenerIndices()
        {
            var index = await _context.Indexes.ToListAsync();
            var indexColumns = await _context.IndexesColumn.ToListAsync();
            return [.. indexColumns.Select(ic =>
            {
            var idx = index.First(x => x.Index_Id == ic.Index_Id && x.Object_Id == ic.Object_Id);
            return new Indice(ic.Object_Id,
                ic.Column_Id,
                ic.Index_Id,
                idx.Name,
                idx.Type_Desc);
            })];
        }
    }
}
