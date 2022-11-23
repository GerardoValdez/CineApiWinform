using CineBack.acceso_a_datos.interfaz;
using CineBack.soporte;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineBack.acceso_a_datos.implementacion
{
    public class PeliculaDao : IPeliculaDao
    {

        //CONSULTAR PELICULAS
        public List<Pelicula> GetPeliculas()
        {
            List<Pelicula> lst = new List<Pelicula>();

            string sp = "SP_CONSULTAR_PELICULAS";
            DataTable t = HelperDao.ObtenerInstancia().ConsultaSQL(sp, null);

            foreach (DataRow dr in t.Rows)
            {
                //Mapear un registro a un objeto del modelo de dominio
                int IdPelicula = int.Parse(dr["id_pelicula"].ToString());
                int IdClasificacion = int.Parse(dr["id_clasificacion"].ToString());
                int IdIdioma = int.Parse(dr["id_idioma"].ToString());
                int IdDirector = int.Parse(dr["id_director"].ToString());
                string NombrePelicula = dr["nombre_pelicula"].ToString();
                DateTime FechaEstreno = DateTime.Parse(dr["fecha_estreno"].ToString());


                Pelicula aux = new Pelicula(IdPelicula, IdClasificacion, IdIdioma, IdDirector, NombrePelicula, FechaEstreno);
                lst.Add(aux);
            }

            return lst;
        }

        //CREAR PELICULA
        public bool Crear(Pelicula oPelicula)
        {
            //throw new NotImplementedException();
            bool ok = true;
            SqlConnection cnn = HelperDao.ObtenerInstancia().ObtenerConexion();
            SqlTransaction t = null;
            SqlCommand cmd = new SqlCommand();
            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                cmd.Connection = cnn;
                cmd.Transaction = t;
                cmd.CommandText = "SP_INSERTAR_PELICULA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idClasificacion", oPelicula.IdClasificacion);
                cmd.Parameters.AddWithValue("@idIdioma", oPelicula.IdIdioma);
                cmd.Parameters.AddWithValue("@idDirector", oPelicula.IdDirector);
                cmd.Parameters.AddWithValue("@nombrePelicula", oPelicula.NombrePelicula);
                cmd.Parameters.AddWithValue("@fechaEstreno", oPelicula.FechaEstreno);
                cmd.ExecuteNonQuery();

                //parámetro de salida:

                /*SqlParameter pOut = new SqlParameter();
                pOut.ParameterName = "@pelicula_nro";
                pOut.DbType = DbType.Int32;
                pOut.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pOut);
                
                
                int PeliculaNro = (int)pOut.Value;
                 
                */

                t.Commit();
            }
            catch (Exception)
            {
                if (t != null)
                    t.Rollback();
                ok = false;
            }

            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }

            return ok;
        }


        //ACTUALIZAR PELICULA
        public bool Actualizar(Pelicula oPelicula)
        {
            //throw new NotImplementedException();

            bool ok = true;
            SqlConnection cnn = HelperDao.ObtenerInstancia().ObtenerConexion();
            SqlTransaction t = null;
            SqlCommand cmd = new SqlCommand();

            try
            {
                cnn.Open();
                t = cnn.BeginTransaction();
                cmd.Connection = cnn;
                cmd.Transaction = t;
                cmd.CommandText = "SP_MODIFICAR_PELICULA";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idClasificacion", oPelicula.IdClasificacion);
                cmd.Parameters.AddWithValue("@idIdioma", oPelicula.IdIdioma);
                cmd.Parameters.AddWithValue("@idDirector", oPelicula.IdDirector);
                cmd.Parameters.AddWithValue("@nombrePelicula", oPelicula.NombrePelicula);
                cmd.Parameters.AddWithValue("@fechaEstreno", oPelicula.FechaEstreno);
                cmd.Parameters.AddWithValue("@peliculaNro", oPelicula.IdPelicula);

                cmd.ExecuteNonQuery();

                t.Commit();
            }

            catch (Exception)
            {
                if (t != null)
                    t.Rollback();
                ok = false;
            }

            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                    cnn.Close();
            }

            return ok;

        }



        //BORRAR PELICULA
        public bool Borrar(int nro)
        {
            //throw new NotImplementedException();

            string sp = "BORRAR_PELICULA";
            List<Parametro> lst = new List<Parametro>();
            lst.Add(new Parametro("@pelicula_nro", nro));
            int afectadas = HelperDao.ObtenerInstancia().EjecutarSQL(sp, lst);
            return afectadas > 0;
        }

        

        
    }
}
