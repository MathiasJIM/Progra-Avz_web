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
    }
}
