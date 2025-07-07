using Raptor.Api.Dtos;
using Raptor.Dominio.Repositories;

namespace Raptor.Api.Services
{
    public class ConsultaService(IConsultasRepository repo)
    {
        private readonly IConsultasRepository _repo = repo;
        public async Task<List<ConsultaDto>> ConsultaBD()
        {
            var tabla = await _repo.ObtenerTablas();
            var columnas = await _repo.ObtenerColumnas();
            var primaryKeys = await _repo.ObtenerPrimaryKeys();
            var foreignKeys = await _repo.ObtenerForeignKeys();
            var triggers = await _repo.ObtenerTriggers();
            var indices = await _repo.ObtenerIndices();

            var response = (from bt in tabla
                            join col in columnas on bt.Object_Id equals col.Object_Id
                            join pk in primaryKeys on new { col.Object_Id, Column_Id = col.Ordinal_Position } equals new { pk.Object_Id, pk.Column_Id }
                            join fk in foreignKeys on new { Object_Id = col.Object_Id, Parent_Column_Id = col.Ordinal_Position } equals new { Object_Id = fk.Parent_Object_Id, Parent_Column_Id = fk.Parent_Column_Id }
                            join tr in triggers on bt.Object_Id equals tr.Parent_Id
                            join ix in indices on new { Object_Id = col.Object_Id, Column_Id = col.Ordinal_Position } equals new { Object_Id = ix.Object_Id, Column_Id = ix.Column_Id }
                            select new ConsultaDto
                            {
                                Table_Schema = bt.Table_Schema,
                                Table_Name = bt.Table_Name,
                                Table_Type = "BASE TABLE",
                                //Column
                                Column_Name = col.Column_Name,
                                Data_Type = col.Data_Type,
                                Is_Nullable = col.Is_Nullable ? "YES": "NO",
                                Character_Maximum_Length = col.Character_Maximum_Length,
                                Numeric_Precision = col.Numeric_Precision,
                                Numeric_Scale = col.Numeric_Scale,
                                //COLUMN_DEFAULT
                                EsIdentity = col.Is_Identity,
                                //PK
                                PK_Column_Name = pk.Column_Id != null ? col.Column_Name : null,
                                //FK
                                FK_Name = fk.FK_Name,
                                FK_Table_Referenced = fk.Referenced_Table,
                                FK_Column_Local = fk.Local_Column,
                                FK_Column_Referenced = fk.Referenced_Column,
                                //TRIGGER
                                Trigger_Name = tr.Trigger_Name,
                                Trigger_Event = tr.Trigger_Event,
                                //Trigger_Definition = tr.Trigger_Definition, CHECKSUM?
                                //INDEX
                                Index_Id = ix.Index_Id,
                                Index_Name = ix.Index_Name,
                                Index_Type = ix.Index_Type
                            }).ToList();

            return response;
        }
    }
}
