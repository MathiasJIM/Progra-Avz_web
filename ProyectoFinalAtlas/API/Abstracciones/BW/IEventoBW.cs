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
    }
}
