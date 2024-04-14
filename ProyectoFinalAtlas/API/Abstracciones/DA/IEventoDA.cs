using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.DA
{
    public interface IEventoDA
    {
        Task<Guid> CrearEvento(Abstracciones.Models.Evento evento, Guid IdUsuario);

        Task<IEnumerable<Abstracciones.Models.Evento>> MostrarEventos();

        Task<int> ActualizarEventoID(Guid IDEvento, Abstracciones.Models.Evento evento);
        Task<Abstracciones.Models.Evento> EliminarEventoPorID(Guid IDEvento);
        Task<Abstracciones.Models.Evento> ObtenerNoticiaPorID(Guid IDEvento);

        Task<IEnumerable<Abstracciones.Models.Evento>> ObtenereEventosPorUsuario(Guid IDUsuario);
    }
}
