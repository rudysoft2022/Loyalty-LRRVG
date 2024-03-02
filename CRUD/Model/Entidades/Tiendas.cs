using System.Collections.Generic;

namespace CRUD.Model.Entidades
{
    public class Tiendas
    {
        public int IdTienda { get; set; }
        public string Sucursal { get; set; }
        public string Direccion { get; set; }
        public List<Articulos> Articulos { get; set; }
    }
}
