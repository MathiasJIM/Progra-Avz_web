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
    public class UsuarioDA : IUsuarioDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public UsuarioDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }
        public async Task<Guid> RegistrarUsuario(Abstracciones.Models.RegistroUsuario usuario)
        {
            string sql = @"[AgregarUsuario]";
            var Consulta = await _sqlConnection.ExecuteScalarAsync<Guid>(sql, new { NombreUsuario = usuario.NombreCompleto, PasswordHash = usuario.ContrasenaHash, CorreoElectronico = usuario.CorreoElectronico, IDRol = usuario.IDRol });
            return Consulta;
        }

        public async Task<int> ActualizarUsuarioPorId(Guid id, Abstracciones.Models.ActualizarUsuario usuario)
        {
            string sql = @"UPDATE Usuarios SET NombreCompleto = @NombreCompleto, CorreoElectronico = @CorreoElectronico WHERE Id = @Id";
            var parametros = new { Id = id, NombreCompleto = usuario.NombreCompleto, CorreoElectronico = usuario.CorreoElectronico };
            var filasActualizadas = await _sqlConnection.ExecuteAsync(sql, parametros);
            return filasActualizadas;

        }

        public async Task<Abstracciones.Models.Usuario> ObtenerUsuarioPorId(Guid id)
        {
            string sql = @"SELECT * FROM Usuarios WHERE Id = @Id";
            var parametros = new { Id = id };
            return await _sqlConnection.QueryFirstOrDefaultAsync<Abstracciones.Models.Usuario>(sql, parametros);
        }


        public async Task<Rol> ObtenerNombreRolPorId(int idRol)
        {
            string sql = @"SELECT [NombreRol] FROM [Roles] WHERE [ID] = @Id";
            return await _sqlConnection.QueryFirstOrDefaultAsync<Rol>(sql, new { Id = idRol });
        }


        public async Task<EstadoCuenta> ObtenerEstadoPorId(int idEst)
        {
            string sql = @"SELECT [Estado] FROM [EstadoCuenta] WHERE [ID] = @Id";
            return await _sqlConnection.QueryFirstOrDefaultAsync<EstadoCuenta>(sql, new { Id = idEst });
        }



    }
}
