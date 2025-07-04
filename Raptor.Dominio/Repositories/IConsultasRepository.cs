using Raptor.Dominio.Entidades;

namespace Raptor.Dominio.Repositories
{
    public interface IConsultasRepository
    {
        Task<List<Tabla>> ObtenerTablas();
        Task<List<Columnas>> ObtenerColumnas();
        Task<List<PrimaryKey>> ObtenerPrimaryKeys();
        Task<List<ForeignKey>> ObtenerForeignKeys();
        Task<List<Trigger>> ObtenerTriggers();
        Task<List<Indices>> ObtenerIndices();
    }
}
