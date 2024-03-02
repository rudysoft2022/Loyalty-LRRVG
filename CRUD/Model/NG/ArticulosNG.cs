using CRUD.Model.Entidades;
using CRUD.Model.Repositorio.IRepository;
using System.Collections.Generic;

namespace CRUD.Model.NG
{
    public class ArticulosNG
    {
        public List<Articulos> GetArticulos()
        {
            
            return IRepository.GetArticulos().GetArticulos();
        }
        public Articulos GetArticuloById(string codigo)
        {
            return IRepository.GetArticulos().GetArticuloById(codigo);
        }
        public Respuesta CreaActualizaArticulo(Articulos articulo)
        {
            return IRepository.GetArticulos().CreaActualizaArticulo(articulo);
        }
        public Respuesta BajaArticulo(string codigo)
        {
            return IRepository.GetArticulos().BajaArticulo(codigo);
        }
      

    }
}
