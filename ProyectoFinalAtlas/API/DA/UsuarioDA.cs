using Abstracciones.DA;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
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
    }
}
