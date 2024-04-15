using Abstracciones.DA;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class RecursoDA : IRecursoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public RecursoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<Guid> CrearRecurso(Abstracciones.Models.Recurso recurso, Guid IdUsuario)
        {
            string sql = @"[spCrearRecurso]";
            var Consulta = await _sqlConnection.ExecuteScalarAsync<Guid>(sql, new
            {
                Titulo = recurso.Titulo,
                Descripcion = recurso.Descripcion,
                TipoRecurso = recurso.TipoRecurso,
                FechaCreacion = DateTime.Now,
                IDUsuarioCreacion = IdUsuario,
                IDNivel = recurso.IDNivel,
                LinkRefuerzo = recurso.LinkRefuerzo,
            });
            return Consulta;
        }


        public async Task<int> ActualizarRecursoID(Guid IDRecurso, Abstracciones.Models.Recurso recurso)
        {
            string sql = "[spActualizarRecurso]";
            var parameters = new
            {
                ID = IDRecurso,
                Titulo = recurso.Titulo,
                Descripcion = recurso.Descripcion,
                TipoRecurso = recurso.TipoRecurso,
                FechaCreacion = DateTime.Now,
                IDNivel = recurso.IDNivel,
                LinkRefuerzo = recurso.LinkRefuerzo,
            };

            var filasActualizadas = await _sqlConnection.ExecuteAsync(
                sql,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return filasActualizadas;
        }

        public async Task<Abstracciones.Models.Recurso> EliminarRecursoPorID(Guid IDRecurso)
        {
            string sql = "[spEliminarRecursoPorID]";
            var parametros = new { ID = IDRecurso };
            return await _sqlConnection.QueryFirstOrDefaultAsync<Abstracciones.Models.Recurso>(sql, parametros);
        }


        public async Task<Abstracciones.Models.Recurso> ObtenerRecursoPorID(Guid IDRecurso)
        {
            string sql = "[spObtenerRecursoPorID]";
            var parametros = new { ID = IDRecurso };
            return await _sqlConnection.QueryFirstOrDefaultAsync<Abstracciones.Models.Recurso>(sql, parametros);
        }

        public async Task<IEnumerable<Abstracciones.Models.Recurso>> MostrarRecursos()
        {
            string sql = "[spObtenerTodosLosRecursos]";
            return await _sqlConnection.QueryAsync<Abstracciones.Models.Recurso>(sql);
        }

        public async Task<IEnumerable<Abstracciones.Models.Recurso>> ObtenereRecursosPorUsuario(Guid IDUsuario)
        {
            string sql = "[spObtenerRecursosPorUsuario]";
            var parametros = new { IDUsuario = IDUsuario };
            return await _sqlConnection.QueryAsync<Abstracciones.Models.Recurso>(sql, parametros);
        }
    }
}
