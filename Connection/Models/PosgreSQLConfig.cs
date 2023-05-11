using Microsoft.EntityFrameworkCore;
using FamsGames.Model;

namespace FamsGames.Model
{
    public class PosgreSQLConfig : DbContext
    {

        protected readonly IConfiguration Configuration;
        public string ConnectionString;

        /// <summary>
        /// Método que lee la configuracion de coneccion a nuestra bd del fichero appsettings
        /// </summary>
        /// <param name="configuration"></param>
        public PosgreSQLConfig(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = configuration.GetConnectionString("WebApiDatabase");
        }
    }
}
