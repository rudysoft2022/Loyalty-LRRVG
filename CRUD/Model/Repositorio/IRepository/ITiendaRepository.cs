using CRUD.Model.Entidades;
using System.Collections.Generic;

namespace CRUD.Model.Repositorio.IRepository
{
    public interface ITiendaRepository
    {

        List<Tiendas> GetTiendas();
        Tiendas GetTiendaById(string idTienda);
        Respuesta CreaActualizatienda(Tiendas tienda);
        Respuesta Bajatienda(string idTienda);
    }
}
