using Raptor.Api.Dtos;
using Raptor.Dominio.Repositories;
using System.Text;

namespace Raptor.Api.Services
{
    public class ConsultaService(IConsultasRepository repo)
    {
        private readonly IConsultasRepository _repo = repo;
        public async Task<List<ConsultaDto>> ConsultaBD()
        {
            var tablas = await _repo.ObtenerTablas();
            var columnas = await _repo.ObtenerColumnas();
            var primaryKeys = await _repo.ObtenerPrimaryKeys();
            var foreignKeys = await _repo.ObtenerForeignKeys(tablas, columnas);
            var triggers = await _repo.ObtenerTriggers();
            var indices = await _repo.ObtenerIndices();

            var stop = "stop";

            var response = tablas.Select(bt => new ConsultaDto
            {
                Table_Schema = bt.Table_Schema,
                Table_Name = bt.Table_Name,
                Table_Type = "BASE TABLE",
                Columnas = [.. columnas
                    .Where(col => col.Object_Id == bt.Object_Id)
                    .Select(col => new ColumnDto
                    {
                        Column_Name = col.Column_Name,
                        Data_Type = col.Data_Type,
                        Is_Nullable = col.Is_Nullable ? "YES" : "NO",
                        Character_Maximum_Length = col.Character_Maximum_Length,
                        Numeric_Precision = col.Numeric_Precision,
                        Numeric_Scale = col.Numeric_Scale,
                        EsIdentity = col.Is_Identity
                    })],
                PrimaryKeys = [.. primaryKeys.Where(pk => pk.Object_Id == bt.Object_Id)
                    .Select(pk => new PrimaryKeyDto
                    {
                        PK_Column_Name = columnas.FirstOrDefault(col => col.Object_Id == pk.Object_Id && col.Ordinal_Position == pk.Column_Id)?.Column_Name ?? string.Empty
                    })],
                ForeignKeys = [.. foreignKeys.Where(fk => fk.Parent_Object_Id == bt.Object_Id)
                    .Select(fk => new ForeignKeyDto
                    {
                        FK_Name = fk.FK_Name,
                        FK_Table_Referenced = fk.Referenced_Table,
                        FK_Column_Local = columnas.FirstOrDefault(col => col.Object_Id == fk.Parent_Object_Id && col.Ordinal_Position == fk.Parent_Column_Id)?.Column_Name ?? string.Empty,
                        FK_Column_Referenced = columnas.FirstOrDefault(col => col.Object_Id == fk.Parent_Object_Id && col.Ordinal_Position == fk.Parent_Column_Id)?.Column_Name ?? string.Empty
                    })],
                Triggers = [.. triggers.Where(tr => tr.Parent_Id == bt.Object_Id)
                    .Select(tr => new TriggerDto
                    {
                        Trigger_Name = tr.Trigger_Name,
                        Trigger_Event = tr.Trigger_Event,
                        Trigger_Definition = CalculateChecksum(tr.Trigger_Definition)
                    })],
                Indices = [.. indices.Where(ix => ix.Object_Id == bt.Object_Id)
                    .Select(ix => new IndexDto
                    {
                        Index_Id = ix.Index_Id,
                        Index_Name = ix.Index_Name,
                        Index_Type = ix.Index_Type
                    })]
            }).ToList();

            return response;
        }
        public static int CalculateChecksum(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            int sum = 0;
            foreach (byte b in bytes)
            {
                sum += b;
            }
            return sum;
        }
    }
}
