using CRUD.Model.Entidades;
using CRUD.Model.Repositorio.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CRUD.Model.NG
{
    public class TiendasNG
    {
     
        public List<Tiendas> GetTienda()
        {
            return IRepository.GetTienda().GetTiendas();
        }
        public Tiendas GetTiendaById(string IdTienda)
        {
            return IRepository.GetTienda().GetTiendaById(IdTienda);
        }
        public Respuesta CreaActualizaTienda(Tiendas tienda)
        {
            return IRepository.GetTienda().CreaActualizatienda(tienda);
        }
        public Respuesta BajaTienda(string IdTienda)
        {
            return IRepository.GetTienda().Bajatienda(IdTienda);
        }




    }
}
