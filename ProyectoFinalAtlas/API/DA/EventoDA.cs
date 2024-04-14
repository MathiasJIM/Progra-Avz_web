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
    public class EventoDA : IEventoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public EventoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<Guid> CrearEvento(Abstracciones.Models.Evento evento, Guid IdUsuario)
        {
            string sql = @"[CrearEvento]";
            var Consulta = await _sqlConnection.ExecuteScalarAsync<Guid>(sql, new
            {
                Titulo = evento.Titulo,
                Descripcion = evento.Descripcion,
                FechaHoraInicio = evento.FechaHoraInicio,
                Duracion = evento.Duracion,
                Ubicacion = evento.Ubicacion,
                TipoEvento = evento.TipoEvento,
                Concurrencia = evento.Concurrencia,
                IDUsuarioCreacion = IdUsuario,
                FechaHoraFinal = evento.FechaHoraFinal,
            });
            return Consulta;
        }

        public async Task<int> ActualizarEventoID(Guid IDEvento, Abstracciones.Models.Evento evento)
        {
            string sql = "[spActualizarEvento]";
            var parameters = new
            {
                ID = IDEvento,
                Titulo = evento.Titulo,
                Descripcion = evento.Descripcion,
                FechaHoraInicio = evento.FechaHoraInicio,
                Duracion = evento.Duracion,
                Ubicacion = evento.Ubicacion,
                TipoEvento = evento.TipoEvento,
                Concurrencia = evento.Concurrencia,
                FechaHoraFinal = evento.FechaHoraFinal,
            };

            var filasActualizadas = await _sqlConnection.ExecuteAsync(
                sql,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return filasActualizadas;
        }

        public async Task<Abstracciones.Models.Evento> EliminarEventoPorID(Guid IDEvento)
        {
            string sql = "[spEliminarEventoPorID]";
            var parametros = new { ID = IDEvento };
            return await _sqlConnection.QueryFirstOrDefaultAsync<Abstracciones.Models.Evento>(sql, parametros);
        }


        public async Task<Abstracciones.Models.Evento> ObtenerNoticiaPorID(Guid IDEvento)
        {
            string sql = "[spObtenerEventoPorID]";
            var parametros = new { ID = IDEvento };
            return await _sqlConnection.QueryFirstOrDefaultAsync<Abstracciones.Models.Evento>(sql, parametros);
        }

        public async Task<IEnumerable<Abstracciones.Models.Evento>> MostrarEventos()
        {
            string sql = "[ObtenerEventos]";
            return await _sqlConnection.QueryAsync<Abstracciones.Models.Evento>(sql);
        }

        public async Task<IEnumerable<Abstracciones.Models.Evento>> ObtenereEventosPorUsuario(Guid IDUsuario)
        {
            string sql = "[spObtenerEventosPorUsuario]";
            var parametros = new { IDUsuario = IDUsuario };
            return await _sqlConnection.QueryAsync<Abstracciones.Models.Evento>(sql, parametros);
        }
    }
}
