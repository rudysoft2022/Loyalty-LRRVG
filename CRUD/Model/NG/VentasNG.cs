using CRUD.Model.DAO;
using CRUD.Model.Entidades;
using System.Collections.Generic;

namespace CRUD.Model.NG
{
    public class VentasNG
    {
        string _conn = string.Empty;
        public VentasNG(string conn)
        {
            _conn = conn;
        }
        public Respuesta RegistraVentas(Ventas venta)
        {
            return new SqlDAOVentas(_conn).RegistraVentas(venta);
        }
    }
}
