using CRUD.Model.NG;
using Microsoft.Data.SqlClient;
using System.Data;
using System;
using CRUD.Model.Entidades;
using System.Collections.Generic;

namespace CRUD.Model.DAO
{
    public class SqlDAOVentas
    {
        string _conn = string.Empty;
        private SqlConnection conexion = null;
        private ReaderSchema lectorDeEsquema;
        public SqlDAOVentas(string conn)
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

        public Respuesta RegistraVentas(Ventas venta)
        {
            Respuesta ventaResp = Respuesta.Error;
            try
            {
                
                if (AbrirConexion() != ConnectionState.Open) return ventaResp;
                SqlCommand cmd = new SqlCommand(lectorDeEsquema.GetScript("ventas"), conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (DetalleVenta t in venta.Articulos)
                {
                    cmd.Parameters.Add("@IdTienda", SqlDbType.Int).Value = venta.IdTienda ;
                    cmd.Parameters.Add("@IdCliente", SqlDbType.VarChar).Value = venta.IdCliente;
                    cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = t.Codigo;
                    cmd.Parameters.Add("@Precio", SqlDbType.Decimal).Value = t.Precio;
                    cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = t.Cantidad;
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                ventaResp = Respuesta.Exito;
                
            }
            catch (SqlException sqlex)
            {
                //enviamos a log
            }
            catch (Exception ex)
            {

            }
            finally { conexion.Close(); }

            return ventaResp;
        }


    }
}
