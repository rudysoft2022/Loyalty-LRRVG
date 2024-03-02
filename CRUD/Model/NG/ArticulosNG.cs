using CRUD.Model.DAO;
using CRUD.Model.Entidades;
using System.Collections.Generic;

namespace CRUD.Model.NG
{
    public class ArticulosNG
    {
        string _conn = string.Empty;
        public ArticulosNG(string conn)
        {
            _conn = conn;
        }
        public List<Articulos> GetArticulos()
        {
            return new SqlDAOArticulos(_conn).GetArticulos();
        }
        public Articulos GetArticuloById(string codigo)
        {
            return new SqlDAOArticulos(_conn).GetArticuloById(codigo);
        }
        public Respuesta CreaActualizaArticulo(Articulos articulo)
        {
            return new SqlDAOArticulos(_conn).CreaActualizaArticulo(articulo);
        }
        public Respuesta BajaArticulo(string codigo)
        {
            return new SqlDAOArticulos(_conn).BajaArticulo(codigo);
        }
      

    }
}
