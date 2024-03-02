using CRUD.Model.Repositorio.Repository;
using Microsoft.Extensions.Configuration;

namespace CRUD.Model.Repositorio.IRepository
{
    public class IRepository
    {


        public static IArticulosRepository GetArticulos()
        {
            return new ArticulosRepository();
        }
        public static IClientesRepository GetClientes()
        {
            return new ClientesRepository();
        }
        public static ITiendaRepository GetTienda()
        {
            return new TIendaRepository();
        }

        public static IVentasRepository GetVentas()
        {
            return new VentasRepository();
        }

    }

    internal static class ConfigurationHelperHelpers
    {
        public static IConfiguration? config;
    }
    public static class ConfigurationHelper
    {
        public static void Initialize(IConfiguration Configuration)
        {
            ConfigurationHelperHelpers.config = Configuration;
        }
        public static string SQL => ConfigurationHelperHelpers.config["ConnectionStrings:SQL"];
    }

}
