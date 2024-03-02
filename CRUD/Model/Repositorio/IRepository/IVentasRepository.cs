using CRUD.Model.Entidades;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;

namespace CRUD.Model.Repositorio.IRepository
{
    public interface IVentasRepository
    {
        Respuesta RegistraVentas(Ventas venta);
    }
}
