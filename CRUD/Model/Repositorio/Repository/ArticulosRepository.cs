using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Data;
using CRUD.Model.NG;
using Microsoft.AspNetCore.Mvc.Filters;
using CRUD.Model.Entidades;
using CRUD.Model.Repositorio.IRepository;

namespace CRUD.Model.Repositorio.Repository
{
    public class ArticulosRepository : IArticulosRepository

    {
    
        string _conn = string.Empty;
        private SqlConnection conexion = null;
        private ReaderSchema lectorDeEsquema;
        public ArticulosRepository() {
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

        public List<Articulos> GetArticulos()
        {
            List<Articulos> lstArticulos = null;
            try
            {
                if (AbrirConexion() != ConnectionState.Open) return lstArticulos;
                SqlCommand cmd = new SqlCommand(lectorDeEsquema.GetScript("articulos"), conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "T";
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    lstArticulos = new List<Articulos>();
                    while (rdr.Read())
                    {
                        Articulos tmpAg = new Articulos();
                        tmpAg.Codigo = Convert.ToString(rdr.GetValue(0));
                        tmpAg.Descripcion = Convert.ToString(rdr.GetValue(1));
                        tmpAg.Precio = Convert.ToDecimal(rdr.GetValue(2));
                        tmpAg.Stock = Convert.ToInt32(rdr.GetValue(3));
                        lstArticulos.Add(tmpAg);
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

            return lstArticulos;
        }
        public Articulos GetArticuloById(string codigo)
        {
            Articulos articulo = null;
            try
            {
                if (AbrirConexion() != ConnectionState.Open) return articulo;
                SqlCommand cmd = new SqlCommand(lectorDeEsquema.GetScript("articulos"), conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "C";
                cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = codigo;
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        articulo = new Articulos();
                        articulo.Codigo = Convert.ToString(rdr.GetValue(0));
                        articulo.Descripcion = Convert.ToString(rdr.GetValue(1));
                        articulo.Precio = Convert.ToDecimal(rdr.GetValue(2));
                        articulo.Stock = Convert.ToInt32(rdr.GetValue(3));
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

            return articulo;
        }
        public Respuesta CreaActualizaArticulo(Articulos articulo)
        {
            Respuesta articuloResp = Respuesta.Error;
            try
            {
                if (AbrirConexion() != ConnectionState.Open) return articuloResp;
                SqlCommand cmd = new SqlCommand(lectorDeEsquema.GetScript("articulos"), conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = articulo.Codigo;
                cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = articulo.Descripcion;
                cmd.Parameters.Add("@Precio", SqlDbType.Decimal).Value = articulo.Precio;
                cmd.Parameters.Add("@Stock", SqlDbType.Int).Value = articulo.Stock;
                cmd.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "A";
                if (cmd.ExecuteNonQuery() > 0)
                    articuloResp = Respuesta.Exito;
            }

            catch (SqlException sqlex)
            {
                //enviamos a log
            }
            catch (Exception ex)
            {

            }
            finally { conexion.Close(); }

            return articuloResp;
        }

        public Respuesta BajaArticulo(string codigo)
        {
            Respuesta articuloResp = Respuesta.Error;
            try
            {
                if (AbrirConexion() != ConnectionState.Open) return articuloResp;
                SqlCommand cmd = new SqlCommand(lectorDeEsquema.GetScript("articulos"), conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = codigo;
                cmd.Parameters.Add("@Accion", SqlDbType.VarChar).Value = "B";
                if (cmd.ExecuteNonQuery() > 0)
                    articuloResp = Respuesta.Exito;
            }

            catch (SqlException sqlex)
            {
                //enviamos a log
            }
            catch (Exception ex)
            {

            }
            finally { conexion.Close(); }

            return articuloResp;
        }


    }
}
