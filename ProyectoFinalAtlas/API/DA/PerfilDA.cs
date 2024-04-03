using Abstracciones.DA;
using Abstracciones.Models;
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
    public class PerfilDA : IPerfilDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public PerfilDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<Abstracciones.Models.Perfil> GerPerfilPorIdUser(Guid IDUsuario)
        {
            string sql = "[GetPerfilByIDUsuario]";
            var parametros = new { IDUsuario = IDUsuario };
            return await _sqlConnection.QueryFirstOrDefaultAsync<Abstracciones.Models.Perfil>(sql, parametros);
        }

        public async Task<Guid> CrearPerfil(Perfil perfil, Guid IDUsuario)
        {
            if (perfil == null)
            {
                throw new ArgumentNullException(nameof(perfil), "El perfil no puede ser nulo.");
            }

            string sql = "[CrearPerfil]";
            var parameters = new
            {
                IDUsuario = IDUsuario,
                Nombre = perfil.Nombre,
                NombrePadreEncargado = perfil.NombrePadreEncargado,
                ContactoEmergencia = perfil.ContactoEmergencia,
                NumeroTelefono = perfil.NumeroTelefono,
                FechaNacimiento = perfil.FechaNacimiento,
                FotoPerfil = perfil.FotoPerfil
            };

            var result = await _sqlConnection.ExecuteScalarAsync<Guid>(
                sql,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return result;
        }

        public async Task<int> ActualizarPerfilPorUsuarioId(Guid usuarioId, Abstracciones.Models.Perfil perfil)
        {
            string sql = "[ActualizarPerfilPorUsuarioId]";
            var parameters = new
            {
                IDUsuario = usuarioId,
                Nombre = perfil.Nombre,
                NombrePadreEncargado = perfil.NombrePadreEncargado,
                ContactoEmergencia = perfil.ContactoEmergencia,
                NumeroTelefono = perfil.NumeroTelefono,
                FechaNacimiento = perfil.FechaNacimiento,
                FotoPerfil = perfil.FotoPerfil
            };

            var filasActualizadas = await _sqlConnection.ExecuteAsync(
                sql,
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return filasActualizadas;
        }



    }
}
