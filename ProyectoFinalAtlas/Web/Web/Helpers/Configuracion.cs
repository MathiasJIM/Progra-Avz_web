using Abstracciones.Modelos;

namespace Web.Helpers
{
    public class Configuracion
    {
        private readonly IConfiguration _configuration;

        public Configuracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetEndpoint(string nombreEndPoint)
        {
            return _configuration.GetSection("APIEndPoints").Get<List<ApiEndPoint>>().Where(e => e.Nombre == nombreEndPoint).First().Valor;
        }
    }
}
