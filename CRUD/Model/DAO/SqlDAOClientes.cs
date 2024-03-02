using CRUD.Model.Entidades;
using CRUD.Model.NG;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System;

namespace CRUD.Model.DAO
{
    public class SqlDAOClientes
    {
        string _conn = string.Empty;
        private SqlConnection conexion = null;
        private ReaderSchema lectorDeEsquema;
        public SqlDAOClientes(string conn)
        {
            _conn = conn;

            lectorDeEsquema = new ReaderSchema();
            lectorDeEsquema.ReaderSchemaDAO();
        }

        public ConnectionState AbrirConexion()
        {
            ConnectionState respuesta = ConnectionState.Closed;
            try
            {
                conexion = null;
                conexion = new SqlConnection(_conn);
                if (conexion == null)
                {

                    return respuesta;
                }
                conexion.Open();
                respuesta = ConnectionState.Open;
            }
            catch (SqlException e)
            {
                //enviamos a log

            }
            catch (Exception e)
            {

            }

            return respuesta;
        }

        public List<Clientes> GetClientes()
        {
            List<Clientes> lstClientes = null;
            try
            {
                if (AbrirConexion() != ConnectionState.Open) return lstClientes;
                SqlCommand cmd = new SqlCommand(lectorDeEsquema.GetScript("clientes"), conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "T";
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    lstClientes = new List<Clientes>();
                    while (rdr.Read())
                    {
                        Clientes tmpAg = new Clientes();
                        tmpAg.IdCliente = Convert.ToInt32(rdr.GetValue(0));
                        tmpAg.Nombre = Convert.ToString(rdr.GetValue(1));
                        tmpAg.Apellidos = Convert.ToString(rdr.GetValue(2));
                        tmpAg.Direccion = Convert.ToString(rdr.GetValue(3));
                        tmpAg.Correo = Convert.ToString(rdr.GetValue(4));
                        lstClientes.Add(tmpAg);
                    }
                }
            }
            catch (SqlException sqlex)
            {
                //enviamos a log
            }
            catch (Exception ex)
            {

            }
            finally { conexion.Close(); }

            return lstClientes;
        }
        public Clientes GetclienteById(string idcliente)
        {
            Clientes cliente = null;
            try
            {
                if (AbrirConexion() != ConnectionState.Open) return cliente;
                SqlCommand cmd = new SqlCommand(lectorDeEsquema.GetScript("clientes"), conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "C";
                cmd.Parameters.Add("@IdCliente", SqlDbType.VarChar).Value = idcliente;
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        cliente = new Clientes();
                        cliente.IdCliente = Convert.ToInt32(rdr.GetValue(0));
                        cliente.Nombre = Convert.ToString(rdr.GetValue(1));
                        cliente.Apellidos = Convert.ToString(rdr.GetValue(2));
                        cliente.Direccion = Convert.ToString(rdr.GetValue(3));
                        cliente.Correo = Convert.ToString(rdr.GetValue(4));
                    }
                }
            }
            catch (SqlException sqlex)
            {
                //enviamos a log
            }
            catch (Exception ex)
            {

            }
            finally { conexion.Close(); }

            return cliente;
        }
        public Respuesta CreaActualizacliente(Clientes cliente)
        {
            Respuesta clienteResp = Respuesta.Error;
            try
            {
                if (AbrirConexion() != ConnectionState.Open) return clienteResp;
                SqlCommand cmd = new SqlCommand(lectorDeEsquema.GetScript("clientes"), conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = cliente.IdCliente;
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = cliente.Nombre;
                cmd.Parameters.Add("@Apellidos", SqlDbType.VarChar).Value = cliente.Apellidos;
                cmd.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = cliente.Direccion;
                cmd.Parameters.Add("@Correo", SqlDbType.VarChar).Value = cliente.Correo;
                cmd.Parameters.Add("@Passwd", SqlDbType.VarChar).Value = cliente.Password;
                cmd.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "A";
                if (cmd.ExecuteNonQuery() > 0)
                    clienteResp = Respuesta.Exito;
            }

            catch (SqlException sqlex)
            {
                //enviamos a log
            }
            catch (Exception ex)
            {

            }
            finally { conexion.Close(); }

            return clienteResp;
        }

        public Respuesta Bajacliente(string idcliente)
        {
            Respuesta clienteResp = Respuesta.Error;
            try
            {
                if (AbrirConexion() != ConnectionState.Open) return clienteResp;
                SqlCommand cmd = new SqlCommand(lectorDeEsquema.GetScript("clientes"), conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idcliente", SqlDbType.VarChar).Value = idcliente;
                cmd.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "B";
                if (cmd.ExecuteNonQuery() > 0)
                    clienteResp = Respuesta.Exito;
            }

            catch (SqlException sqlex)
            {
                //enviamos a log
            }
            catch (Exception ex)
            {

            }
            finally { conexion.Close(); }

            return clienteResp;
        }
        public Clientes LoginCliente(LoginCliente data)
        {
            Clientes cliente = null;
            try
            {
                Clientes tmp = new Clientes() { Correo=data.correo, Password=data.password };
                if (AbrirConexion() != ConnectionState.Open) return cliente;
                SqlCommand cmd = new SqlCommand(lectorDeEsquema.GetScript("clientes"), conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "L";
                cmd.Parameters.Add("@Correo", SqlDbType.VarChar).Value = tmp.Correo;
                cmd.Parameters.Add("@Passwd", SqlDbType.VarChar).Value = tmp.Password;
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        cliente = new Clientes();
                        cliente.IdCliente = Convert.ToInt32(rdr.GetValue(0));
                        cliente.Nombre = Convert.ToString(rdr.GetValue(1));
                        cliente.Apellidos = Convert.ToString(rdr.GetValue(2));
                        cliente.Direccion = Convert.ToString(rdr.GetValue(3));
                        cliente.Correo = Convert.ToString(rdr.GetValue(4));
                    }
                }
            }
            catch (SqlException sqlex)
            {
                //enviamos a log
            }
            catch (Exception ex)
            {

            }
            finally { conexion.Close(); }

            return cliente;
        }
    }
}
