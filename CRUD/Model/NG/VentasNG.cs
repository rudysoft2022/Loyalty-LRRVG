using CRUD.Model.Entidades;
using CRUD.Model.Repositorio.IRepository;
using System.Collections.Generic;

namespace CRUD.Model.NG
{
    public class VentasNG
    {
      
        public Respuesta RegistraVentas(Ventas venta)
        {
            return IRepository.GetVentas().RegistraVentas(venta);
        }
    }
}
