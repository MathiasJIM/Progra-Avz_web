using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.BW
{
    public interface IEventoBW
    {
        Task<Guid> CrearEvento(Abstracciones.Models.Evento evento, Guid IdUsuario);
        Task<IEnumerable<Abstracciones.Models.Evento>> MostrarEventos();

        Task<int> ActualizarEventoID(Guid IDEvento, Abstracciones.Models.Evento evento);
        Task<Abstracciones.Models.Evento> EliminarEventoPorID(Guid IDEvento);
        Task<Abstracciones.Models.Evento> ObtenerEventoPorID(Guid IDEvento);
        Task<IEnumerable<Abstracciones.Models.Evento>> ObtenereEventosPorUsuario(Guid IDUsuario);

        Task<Guid> AddAsistencia(Guid IDEvento, Guid IdUsuario);

        Task<IEnumerable<Abstracciones.Models.UsuarioAsistente>> ObtenerAsistentesPorIdEvento(Guid IDEvento);
        Task<Abstracciones.Models.Evento> EliminarAsistenciaPorID(Guid IDEvento, Guid IdUsuario);

        Task<int> VerificarAsistencia(Guid IDEvento, Guid IdUsuario);


    }
}
