using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.DA
{
    public interface IRecursoDA
    {
        Task<Guid> CrearRecurso(Abstracciones.Models.Recurso recurso, Guid IdUsuario);
        Task<int> ActualizarRecursoID(Guid IDRecurso, Abstracciones.Models.Recurso recurso);
        Task<Abstracciones.Models.Recurso> EliminarRecursoPorID(Guid IDRecurso);
        Task<Abstracciones.Models.Recurso> ObtenerRecursoPorID(Guid IDRecurso);
        Task<IEnumerable<Abstracciones.Models.Recurso>> MostrarRecursos();
        Task<IEnumerable<Abstracciones.Models.Recurso>> ObtenereRecursosPorUsuario(Guid IDUsuario);
    }
}
