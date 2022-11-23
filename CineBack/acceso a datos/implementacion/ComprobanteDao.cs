using CineBack.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineBack.acceso_a_datos
{
    class ComprobanteDao : IComprobanteDao
    {
        //MOSTRAR FORMA PAGO
        public List<FormaPago> GetFormaPago()
        {
            List<FormaPago> lst = new List<FormaPago>();

            string sp = "SP_CONSULTAR_FORMA_PAGO";
            DataTable t = HelperDao.ObtenerInstancia().ConsultaSQL(sp, null);

            foreach (DataRow dr in t.Rows)
            {
                FormaPago aux = new FormaPago();
                aux.IdFormaPago = int.Parse(dr["id_forma_pago"].ToString());
                aux.Descripcion = dr["descripcion"].ToString();

                lst.Add(aux);
            }
            return lst;
        }


        //MOSTRAR FORMA COMPRA
        public List<FormaCompra> GetFormaCompra()
        {
            List<FormaCompra> lst = new List<FormaCompra>();

            string sp = "SP_CONSULTAR_FORMA_COMPRA";
            DataTable t = HelperDao.ObtenerInstancia().ConsultaSQL(sp, null);

            foreach (DataRow dr in t.Rows)
            {
                FormaCompra aux = new FormaCompra();
                aux.IdFormaCompra = int.Parse(dr["id_forma_compra"].ToString());
                aux.Descripcion = dr["descripcion"].ToString();

                lst.Add(aux);
            }
            return lst;
        }


        //MOSTRAR CLIENTES
        public List<Cliente> GetClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            string sp = "SP_CONSULTAR_CLIENTES";
            DataTable t = HelperDao.ObtenerInstancia().ConsultaSQL(sp, null);

            foreach (DataRow dr in t.Rows)
            {
                Cliente aux = new Cliente();
                aux.IdCliente = int.Parse(dr["id_cliente"].ToString());
                aux.Nombre = dr["nombre"].ToString();

                clientes.Add(aux);
            }
            return clientes;
        }


        //MOSTRAR FUNCIONES
        public List<Funcion> GetFunciones()
        {
            List<Funcion> funciones = new List<Funcion>();

            string sp = "SP_CONSULTAR_FUNCIONES";
            DataTable t = HelperDao.ObtenerInstancia().ConsultaSQL(sp, null);

            foreach (DataRow dr in t.Rows)
            {
                Funcion aux = new Funcion();
                aux.IdFuncion   = int.Parse(dr["id_funcion"].ToString());
                aux.Descripcion = dr["descripcion"].ToString();
                aux.Precio      = double.Parse(dr["precio"].ToString());

                funciones.Add(aux);
            }
            return funciones;
        }

        //MOSTRAR COMPROBANTES
        public List<ViewComprobante> GetComprobantes()
        {
            List<ViewComprobante> comprobantes = new List<ViewComprobante>();

            string sp = "SP_CONSULTAR_COMPROBANTES";
            DataTable t = HelperDao.ObtenerInstancia().ConsultaSQL(sp, null);

            foreach (DataRow dr in t.Rows)
            {
                ViewComprobante aux = new ViewComprobante();
                aux.IdComprobante = int.Parse(dr["id_comprobante"].ToString());
                aux.Cliente     = dr["cliente"].ToString();
                aux.FormaPago   = dr["forma_pago"].ToString();
                aux.FormaCompra = dr["forma_compra"].ToString();
                aux.Total = double.Parse(dr["total"].ToString());
                aux.Fecha         = DateTime.Parse(dr["fecha"].ToString());

                comprobantes.Add(aux);
            }
            return comprobantes;
        }

        //POR SI FALLA
        //Comprobante aux = new Comprobante();
        //aux.IdComprobante = int.Parse(dr["id_comprobante"].ToString());
        //aux.IdCliente     = int.Parse(dr["id_cliente"].ToString());
        //aux.IdFormaPago   = int.Parse(dr["id_forma_pago"].ToString());
        //aux.IdFormaCompra = int.Parse(dr["id_forma_compra"].ToString());
        //aux.Fecha         = DateTime.Parse(dr["fecha"].ToString());
        //        aux.IdEstado      = int.Parse(dr["id_estado"].ToString());


        //CREAR COMPROBANTES
        public bool Crear(Comprobante oComprobante)
        {
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
                cmd.CommandText = "SP_INSERTAR_MAESTRO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCliente", oComprobante.IdCliente);
                cmd.Parameters.AddWithValue("@idFormaPago", oComprobante.IdFormaPago);
                cmd.Parameters.AddWithValue("@idFormaCompra", oComprobante.IdFormaCompra);
                cmd.Parameters.AddWithValue("@total", oComprobante.CalcularTotal());

                //parámetro de salida:
                SqlParameter pOut = new SqlParameter();
                pOut.ParameterName = "@comprobante_nro";
                pOut.DbType = DbType.Int32;
                pOut.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pOut);
                cmd.ExecuteNonQuery();

                int comprobanteNro = (int)pOut.Value;

                SqlCommand cmdDetalle;

                foreach (DetalleComprobante item in oComprobante.Detalles)
                {
                    cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE", cnn, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@idComprobante", comprobanteNro);
                    cmdDetalle.Parameters.AddWithValue("@idFuncion", item.Funcion.IdFuncion);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", item.Cantidad);
                    cmdDetalle.ExecuteNonQuery();
                }
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


        //ACTUALIZAR COMPROBANTE
        public bool Actualizar(Comprobante oComprobante)
        {
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
                cmd.CommandText = "SP_MODIFICAR_MAESTRO";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCliente", oComprobante.IdCliente);
                cmd.Parameters.AddWithValue("@idFormaPago", oComprobante.IdFormaPago);
                cmd.Parameters.AddWithValue("@idFormaCompra", oComprobante.IdFormaCompra);
                cmd.Parameters.AddWithValue("@total", oComprobante.CalcularTotal());
                cmd.Parameters.AddWithValue("@comprobante_nro", oComprobante.IdComprobante);
                cmd.ExecuteNonQuery();

                SqlCommand cmdDetalle;

                foreach (DetalleComprobante item in oComprobante.Detalles)
                {
                    cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE", cnn, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@idComprobante", oComprobante.IdComprobante);
                    cmdDetalle.Parameters.AddWithValue("@idFuncion", item.Funcion.IdFuncion);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", item.Cantidad);
                    cmdDetalle.ExecuteNonQuery();
                }
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


        //BORRAR COMPROBANTES
        public bool Borrar(int nro)
        {
            string sp = "SP_BORRAR_COMPROBANTE";
            List<Parametro> lst = new List<Parametro>();
            lst.Add(new Parametro("@comprobante_nro", nro));
            int afectadas = HelperDao.ObtenerInstancia().EjecutarSQL(sp, lst);
            return afectadas > 0;
        }
    }
}
