using CRUD.Model.Entidades;
using System.Collections.Generic;

namespace CRUD.Model.Repositorio.IRepository
{
    public interface IClientesRepository
    {
        List<Clientes> GetClientes();
        Clientes GetclienteById(string idcliente);
        Respuesta CreaActualizacliente(Clientes cliente);
        Respuesta Bajacliente(string idcliente);
        Clientes LoginCliente(LoginCliente data);
    }
}
