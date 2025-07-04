using Microsoft.EntityFrameworkCore;
using Raptor.Dominio.Entidades;
using Raptor.Dominio.Repositories;

namespace Raptor.Api.Infraestructura
{
    public class ConsultasRepository(Context context) : IConsultasRepository
    {
        protected readonly Context _context = context;

        public async Task<List<Columnas>> ObtenerColumnas()
        {
            return await _context.Columnas.FromSqlRaw(@"SELECT 
                c.object_id,
                c.column_id AS ORDINAL_POSITION,
                c.name AS COLUMN_NAME,
                c.is_nullable,
                ty.name AS DATA_TYPE,
                c.max_length AS CHARACTER_MAXIMUM_LENGTH,
                c.precision AS NUMERIC_PRECISION,
                c.scale AS NUMERIC_SCALE,
                c.default_object_id,
                c.is_identity
            FROM sys.columns c
            JOIN sys.types ty ON c.user_type_id = ty.user_type_id").ToListAsync();
        }

        public async Task<List<ForeignKey>> ObtenerForeignKeys()
        {
            return await _context.ForeignKeys.FromSqlRaw(@"SELECT 
                fk.parent_object_id,
                fkc.parent_column_id,
                fk.name AS FK_NAME,
                rt.name AS REFERENCED_TABLE,
                rc.name AS REFERENCED_COLUMN,
                pc.name AS LOCAL_COLUMN
            FROM sys.foreign_keys fk
            JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
            JOIN sys.tables rt ON rt.object_id = fkc.referenced_object_id
            JOIN sys.columns rc ON rc.object_id = fkc.referenced_object_id AND rc.column_id = fkc.referenced_column_id
            JOIN sys.columns pc ON pc.object_id = fkc.parent_object_id AND pc.column_id = fkc.parent_column_id").ToListAsync();
        }

        public async Task<List<Indices>> ObtenerIndices()
        {
            return await _context.Indices.FromSqlRaw(@"SELECT 
                ic.object_id,
                ic.column_id,
                ic.index_id,
                i.name AS INDEX_NAME,
                i.type_desc AS INDEX_TYPE
            FROM sys.index_columns ic
            JOIN sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id").ToListAsync();
        }

        public async Task<List<PrimaryKey>> ObtenerPrimaryKeys()
        {
            return await _context.PrimaryKeys.FromSqlRaw(@"SELECT 
                ic.object_id,
                ic.column_id
            FROM sys.key_constraints kc
            JOIN sys.index_columns ic ON kc.parent_object_id = ic.object_id AND kc.unique_index_id = ic.index_id
            WHERE kc.type = 'PK'").ToListAsync();
        }

        public async Task<List<Tabla>> ObtenerTablas()
        {
            return await _context.Tablas.FromSqlRaw(@"SELECT 
        t.object_id,
        t.name AS TABLE_NAME,
        s.name AS TABLE_SCHEMA
    FROM sys.tables t
    JOIN sys.schemas s ON s.schema_id = t.schema_id
    WHERE t.is_ms_shipped = 0").ToListAsync();
        }

        public async Task<List<Trigger>> ObtenerTriggers()
        {
            return await _context.Triggers.FromSqlRaw(@"SELECT 
                t.parent_id,
                t.name AS TRIGGER_NAME,
                te.type_desc AS TRIGGER_EVENT,
                m.definition AS TRIGGER_DEFINITION
            FROM sys.triggers t
            LEFT JOIN sys.trigger_events te ON t.object_id = te.object_id
            LEFT JOIN sys.sql_modules m ON t.object_id = m.object_id
            WHERE t.is_ms_shipped = 0 AND t.name NOT LIKE '%SYM%'").ToListAsync();
        }
    }
}
