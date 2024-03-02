using CRUD.Model.Entidades;
using System.Collections.Generic;

namespace CRUD.Model.Repositorio.IRepository
{
    public interface IArticulosRepository
    {
        List<Articulos> GetArticulos();
        Articulos GetArticuloById(string codigo);
        Respuesta CreaActualizaArticulo(Articulos articulo);
        Respuesta BajaArticulo(string codigo);
    }
}
