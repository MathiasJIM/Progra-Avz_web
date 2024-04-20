using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Models;
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

        public async Task<int> ActualizarEventoID(Guid IDEvento, Abstracciones.Models.Evento evento)
        {
            return await _eventoDA.ActualizarEventoID(IDEvento, evento);
        }
        public async Task<Abstracciones.Models.Evento> EliminarEventoPorID(Guid IDEvento)
        {
            return await _eventoDA.EliminarEventoPorID(IDEvento);
        }
        public async Task<Abstracciones.Models.Evento> ObtenerEventoPorID(Guid IDEvento)
        {
            return await _eventoDA.ObtenerEventoPorID(IDEvento);
        }

        public async Task<IEnumerable<Abstracciones.Models.Evento>> ObtenereEventosPorUsuario(Guid IDUsuario)
        {
            return await _eventoDA.ObtenereEventosPorUsuario(IDUsuario);
        }

        public async Task<Guid> AddAsistencia(Guid IDEvento, Guid IdUsuario)
        {
            return await _eventoDA.AddAsistencia(IDEvento, IdUsuario);
        }

        public async Task<IEnumerable<UsuarioAsistente>> ObtenerAsistentesPorIdEvento(Guid IDEvento)
        {
            return await _eventoDA.ObtenerAsistentesPorIdEvento(IDEvento);
        }

        public async Task<Evento> EliminarAsistenciaPorID(Guid IDEvento, Guid IdUsuario)
        {
            return await _eventoDA.EliminarAsistenciaPorID(IDEvento, IdUsuario);
        }


        public async Task<int> VerificarAsistencia(Guid IDEvento, Guid IdUsuario)
        {
            return await _eventoDA.VerificarAsistencia(IDEvento, IdUsuario);
        }
    }
}
