using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.DA
{
    public interface INoticiaDA
    {
        Task<Guid> CrearNoticia(Abstracciones.Models.Noticia noticia, Guid IdUsuario);
        Task<IEnumerable<Abstracciones.Models.Noticia>> ObtenerNoticiasPorUsuario(Guid IDUsuario);


        Task<int> ActualizarNoticiaPorID(Guid IDNoticia, Abstracciones.Models.Noticia noticia);
        Task<IEnumerable<Abstracciones.Models.Noticia>> MostrarNoticias();

        Task<Abstracciones.Models.Noticia> EliminarNoticiasPorUsuario(Guid IDUsuario);
        Task<Abstracciones.Models.Noticia> EliminarNoticiaPorID(Guid IDNoticia);

        Task<Abstracciones.Models.Noticia> ObtenerNoticiaPorID(Guid IDNoticia);


    }
}
