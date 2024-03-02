using CRUD.Model.DAO;
using CRUD.Model.Entidades;
using System.Collections.Generic;

namespace CRUD.Model.NG
{
    public class ClientesNG
    {
        string _conn = string.Empty;
        public ClientesNG(string conn)
        {
            _conn = conn;
        }
        public List<Clientes> GetClientes()
        {
            return new SqlDAOClientes(_conn).GetClientes();
        }
        public Clientes GetClienteById(string idcliente)
        {
            return new SqlDAOClientes(_conn).GetclienteById(idcliente);
        }
        public Respuesta CreaActualizaCliente(Clientes cliente)
        {
            return new SqlDAOClientes(_conn).CreaActualizacliente(cliente);
        }
        public Respuesta BajaCliente(string idcliente)
        {
            return new SqlDAOClientes(_conn).Bajacliente(idcliente);
        }

        public Clientes LoginCliente(LoginCliente data)
        {
            return new SqlDAOClientes(_conn).LoginCliente(data);
        }

    }
}
