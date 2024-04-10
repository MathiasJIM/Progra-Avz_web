using Abstracciones.BW;
using Abstracciones.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW
{
    public class EventoBW : IEventoBW
    {
        private IEventoDA _eventoDA;

        public EventoBW(IEventoDA eventoDA)
        {
            _eventoDA = eventoDA;
        }


        public async Task<Guid> CrearEvento(Abstracciones.Models.Evento evento, Guid IdUsuario)
        {
            return await _eventoDA.CrearEvento(evento, IdUsuario);
        }

        public async Task<IEnumerable<Abstracciones.Models.Evento>> MostrarEventos()
        {
            return await _eventoDA.MostrarEventos();
        }
    }
}
