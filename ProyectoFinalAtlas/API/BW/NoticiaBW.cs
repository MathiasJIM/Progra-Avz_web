using Abstracciones.BC;
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
    public class NoticiaBW : INoticiaBW
    {
        private INoticiaDA _noticiaDA;

        public NoticiaBW(INoticiaDA noticiaDA)
        {
            _noticiaDA = noticiaDA;
        }

        public async Task<int> ActualizarNoticiaPorID(Guid IDNoticia, Noticia noticia)
        {
            return await _noticiaDA.ActualizarNoticiaPorID(IDNoticia, noticia);
        }

        public async Task<Guid> CrearNoticia(Noticia noticia, Guid IdUsuario)
        {
            return await _noticiaDA.CrearNoticia(noticia, IdUsuario);
        }

        public async Task<Noticia> EliminarNoticiaPorID(Guid IDNoticia)
        {
            return await _noticiaDA.EliminarNoticiaPorID(IDNoticia);
        }

        public async Task<Noticia> EliminarNoticiasPorUsuario(Guid IDUsuario)
        {
            return await _noticiaDA.EliminarNoticiasPorUsuario(IDUsuario);
        }

        public async Task<IEnumerable<Abstracciones.Models.Noticia>> MostrarNoticias()
        {
            return await _noticiaDA.MostrarNoticias();
        }

        public async Task<IEnumerable<Abstracciones.Models.Noticia>> ObtenerNoticiasPorUsuario(Guid IDUsuario)
        {
            return await _noticiaDA.ObtenerNoticiasPorUsuario(IDUsuario);
        }

        public async Task<Abstracciones.Models.Noticia> ObtenerNoticiaPorID(Guid IDNoticia)
        {
            return await _noticiaDA.ObtenerNoticiaPorID(IDNoticia);
        }
    }
}
