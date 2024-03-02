using CRUD.Model.Entidades;
using CRUD.Model.NG;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System;
using CRUD.Model.Repositorio.IRepository;
using Microsoft.Extensions.Configuration;

namespace CRUD.Model.Repositorio.Repository
{
    public class TIendaRepository:ITiendaRepository
    {

        string _conn = string.Empty;
        private SqlConnection conexion = null;
        private ReaderSchema lectorDeEsquema;
        public TIendaRepository()
        {
            _conn = ConfigurationHelper.SQL;
            lectorDeEsquema = new ReaderSchema();
            lectorDeEsquema.ReaderSchemaDAO();

        }

        private ConnectionState AbrirConexion()
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

        public List<Tiendas> GetTiendas()
        {
            List<Tiendas> lstTiendas = null;
            try
            {
                if (AbrirConexion() != ConnectionState.Open) return lstTiendas;
                SqlCommand cmd = new SqlCommand(lectorDeEsquema.GetScript("tiendas"), conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "T";
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    lstTiendas = new List<Tiendas>();
                    while (rdr.Read())
                    {
                        Tiendas tmpAg = new Tiendas();
                        tmpAg.IdTienda = Convert.ToInt32(rdr.GetValue(0));
                        tmpAg.Sucursal = Convert.ToString(rdr.GetValue(1));
                        tmpAg.Direccion = Convert.ToString(rdr.GetValue(2));
                        lstTiendas.Add(tmpAg);
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

            return lstTiendas;
        }
        public Tiendas GetTiendaById(string idTienda)
        {
            Tiendas tienda = null;
            try
            {
                if (AbrirConexion() != ConnectionState.Open) return tienda;
                SqlCommand cmd = new SqlCommand(lectorDeEsquema.GetScript("tiendas"), conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "C";
                cmd.Parameters.Add("@IdTienda", SqlDbType.VarChar).Value = idTienda;
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        tienda = new Tiendas();
                        tienda.IdTienda = Convert.ToInt32(rdr.GetValue(0));
                        tienda.Sucursal = Convert.ToString(rdr.GetValue(1));
                        tienda.Direccion = Convert.ToString(rdr.GetValue(2));
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

            return tienda;
        }
        public Respuesta CreaActualizatienda(Tiendas tienda)
        {
            Respuesta tiendaResp = Respuesta.Error;
            try
            {
                if (AbrirConexion() != ConnectionState.Open) return tiendaResp;
                SqlCommand cmd = new SqlCommand(lectorDeEsquema.GetScript("tiendas"), conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdTienda", SqlDbType.VarChar).Value = tienda.IdTienda;
                cmd.Parameters.Add("@Sucursal", SqlDbType.VarChar).Value = tienda.Sucursal;
                cmd.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = tienda.Direccion;
                cmd.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "A";
                int IdTienda = Convert.ToInt32(cmd.ExecuteScalar());


                if (IdTienda > 0)
                {
                    foreach (Articulos a in tienda.Articulos)
                    {
                        cmd = new SqlCommand(lectorDeEsquema.GetScript("tiendasArticulo"), conexion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = a.Codigo;
                        cmd.Parameters.Add("@IdTienda", SqlDbType.Int).Value = IdTienda;
                        cmd.ExecuteNonQuery();
                    }
                    tiendaResp = Respuesta.Exito;
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

            return tiendaResp;
        }

        public Respuesta Bajatienda(string idTienda)
        {
            Respuesta tiendaResp = Respuesta.Error;
            try
            {
                if (AbrirConexion() != ConnectionState.Open) return tiendaResp;
                SqlCommand cmd = new SqlCommand(lectorDeEsquema.GetScript("tiendas"), conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdTienda", SqlDbType.Int).Value = idTienda;
                cmd.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "B";
                if (cmd.ExecuteNonQuery() > 0)
                    tiendaResp = Respuesta.Exito;
            }

            catch (SqlException sqlex)
            {
                //enviamos a log
            }
            catch (Exception ex)
            {

            }
            finally { conexion.Close(); }

            return tiendaResp;
        }



    }
}
