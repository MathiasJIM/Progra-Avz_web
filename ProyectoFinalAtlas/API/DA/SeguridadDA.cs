using Abstracciones.DA;
using Abstracciones.Entities;
using Abstracciones.Models;
using Dapper;
using Helpers;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class SeguridadDA : ISeguridadDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public SeguridadDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<Abstracciones.Models.Usuario> ObtenerUsuario(Abstracciones.Models.Usuario usuario)
        {
            string sql = @"[ObtenerUsuario]";
            var Consulta = await _sqlConnection.QueryAsync<Abstracciones.Entities.Usuario>(sql, new { usuario.CorreoElectronico });
            return Convertidor.Convertir<Abstracciones.Entities.Usuario, Abstracciones.Models.Usuario>(Consulta.FirstOrDefault());
        }
    }
}
