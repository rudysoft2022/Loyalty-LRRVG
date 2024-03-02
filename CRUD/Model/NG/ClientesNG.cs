using CRUD.Model.Entidades;
using CRUD.Model.Repositorio.IRepository;
using System.Collections.Generic;

namespace CRUD.Model.NG
{
    public class ClientesNG
    {
      
        public List<Clientes> GetClientes()
        {
            return IRepository.GetClientes().GetClientes();
        }
        public Clientes GetClienteById(string idcliente)
        {
            return IRepository.GetClientes().GetclienteById(idcliente);
        }
        public Respuesta CreaActualizaCliente(Clientes cliente)
        {
            return IRepository.GetClientes().CreaActualizacliente(cliente);
        }
        public Respuesta BajaCliente(string idcliente)
        {
            return IRepository.GetClientes().Bajacliente(idcliente);
        }

        public Clientes LoginCliente(LoginCliente data)
        {
            return IRepository.GetClientes().LoginCliente(data);
        }

    }
}
