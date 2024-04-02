using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstracciones.DA;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DA.Repositorio
{
    public class RepositorioDapper : IRepositorioDapper
    {
        private readonly IConfiguration _configuration;
        public SqlConnection _conexionBaseDatos { get; }

        public RepositorioDapper(IConfiguration configuration)
        {
            _configuration = configuration;
            _conexionBaseDatos = new SqlConnection(_configuration.GetConnectionString("BD"));
        }

        public SqlConnection ObtenerRepositorioDapper()
        {
            return _conexionBaseDatos;
        }
    }
}
