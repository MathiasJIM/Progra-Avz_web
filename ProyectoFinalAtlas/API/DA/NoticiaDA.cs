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
    public class NoticiaDA : INoticiaDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public NoticiaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<Guid> CrearNoticia(Abstracciones.Models.Noticia noticia, Guid IdUsuario)
        {
            string sql = @"[CrearNoticia]";
            var Consulta = await _sqlConnection.ExecuteScalarAsync<Guid>(sql, new { 
               Titulo = noticia.Titulo,
               Descripcion = noticia.Descripcion,
               Imagen = noticia.imagen,
               FechaPublicacion = noticia.FechaPublicacion,
               IDUsuarioPublico= IdUsuario
            });
            return Consulta;
        }

        public async Task<int> ActualizarNoticiaPorID(Guid IDNoticia, Abstracciones.Models.Noticia noticia)
        {
            string sql = "[ActualizarNoticiaPorID]";
            var parameters = new
            {
                IDNoticia = IDNoticia,
                Titulo = noticia.Titulo,
                Descripcion = noticia.Descripcion,
                Imagen = noticia.imagen,
                FechaPublicacion = noticia.FechaPublicacion,
                IDUsuarioPublico = noticia.IDUsuarioPublico
            };

            var filasActualizadas = await _sqlConnection.ExecuteAsync(
                sql,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return filasActualizadas;
        }

        public async Task<Abstracciones.Models.Noticia> ObtenerNoticiaPorID(Guid IDNoticia)
        {
            string sql = "[ObtenerNoticiaPorID]";
            var parametros = new { IDNoticia = IDNoticia };
            return await _sqlConnection.QueryFirstOrDefaultAsync<Abstracciones.Models.Noticia>(sql, parametros);
        }

        public async Task<IEnumerable<Abstracciones.Models.Noticia>> ObtenerNoticiasPorUsuario(Guid IDUsuario)
        {
            string sql = "[ObtenerNoticiasPorUsuario]";
            var parametros = new { IDUsuarioPublico = IDUsuario };
            return await _sqlConnection.QueryAsync<Abstracciones.Models.Noticia>(sql, parametros);
        }

        public async Task<IEnumerable<Abstracciones.Models.Noticia>> MostrarNoticias()
        {
            string sql = "[MostrarNoticias]";
            return await _sqlConnection.QueryAsync<Abstracciones.Models.Noticia>(sql);
        }


        public async Task<Abstracciones.Models.Noticia> EliminarNoticiasPorUsuario(Guid IDUsuario)
        {
            string sql = "[EliminarNoticiasPorUsuario]";
            var parametros = new { IDUsuarioPublico = IDUsuario };
            return await _sqlConnection.QueryFirstOrDefaultAsync<Abstracciones.Models.Noticia>(sql);
        }

        public async Task<Abstracciones.Models.Noticia> EliminarNoticiaPorID(Guid IDNoticia)
        {
            string sql = "[EliminarNoticiaPorID]";
            var parametros = new { IDNoticia = IDNoticia };
            return await _sqlConnection.QueryFirstOrDefaultAsync<Abstracciones.Models.Noticia>(sql, parametros);
        }
    }
}
