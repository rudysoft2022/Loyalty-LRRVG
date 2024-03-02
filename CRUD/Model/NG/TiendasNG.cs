using CRUD.Model.DAO;
using CRUD.Model.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CRUD.Model.NG
{
    public class TiendasNG
    {
        string _conn = string.Empty;
        public TiendasNG(string conn)
        {
            _conn = conn;
        }
        public List<Tiendas> GetTienda()
        {
            return new SqlDAOTiendas(_conn).GetTiendas();
        }
        public Tiendas GetTiendaById(string IdTienda)
        {
            return new SqlDAOTiendas(_conn).GetTiendaById(IdTienda);
        }
        public Respuesta CreaActualizaTienda(Tiendas tienda)
        {
            return new SqlDAOTiendas(_conn).CreaActualizatienda(tienda);
        }
        public Respuesta BajaTienda(string IdTienda)
        {
            return new SqlDAOTiendas(_conn).Bajatienda(IdTienda);
        }




    }
}
