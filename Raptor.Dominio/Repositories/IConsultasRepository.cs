using Raptor.Dominio.Entidades;

namespace Raptor.Dominio.Repositories
{
    public interface IConsultasRepository
    {
        Task<List<Tabla>> ObtenerTablas();
        Task<List<Columna>> ObtenerColumnas(List<Tabla> tablas);
        Task<List<PrimaryKey>> ObtenerPrimaryKeys(List<Tabla> tablas);
        Task<List<ForeignKey>> ObtenerForeignKeys(List<Tabla> tablas, List<Columna> columnas);
        Task<List<Trigger>> ObtenerTriggers();
        Task<List<Indice>> ObtenerIndices(List<Tabla> tablas);
    }
}
