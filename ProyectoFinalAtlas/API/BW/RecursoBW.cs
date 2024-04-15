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
    public class RecursoBW : IRecursoBW
    {
        private IRecursoDA _recursoDA;

        public RecursoBW(IRecursoDA recursoDA)
        {
            _recursoDA = recursoDA;
        }
        public async Task<int> ActualizarRecursoID(Guid IDRecurso, Recurso recurso)
        {
            return await _recursoDA.ActualizarRecursoID(IDRecurso, recurso);
        }

        public async Task<Guid> CrearRecurso(Recurso recurso, Guid IdUsuario)
        {
            return await _recursoDA.CrearRecurso(recurso, IdUsuario);
        }

        public async Task<Recurso> EliminarRecursoPorID(Guid IDRecurso)
        {
            return await _recursoDA.EliminarRecursoPorID(IDRecurso);
        }

        public async Task<IEnumerable<Recurso>> MostrarRecursos()
        {
            return await _recursoDA.MostrarRecursos();
        }

        public async Task<IEnumerable<Recurso>> ObtenereRecursosPorUsuario(Guid IDUsuario)
        {
            return await _recursoDA.ObtenereRecursosPorUsuario(IDUsuario);
        }

        public async Task<Recurso> ObtenerRecursoPorID(Guid IDRecurso)
        {
            return await _recursoDA.ObtenerRecursoPorID(IDRecurso);
        }
    }
}
