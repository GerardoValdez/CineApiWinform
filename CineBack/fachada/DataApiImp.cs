using CineBack.acceso_a_datos;
using CineBack.acceso_a_datos.implementacion;
using CineBack.acceso_a_datos.interfaz;
using CineBack.dominio;
using CineBack.soporte;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CineBack.fachada
{
    public class DataApiImp : IDataApi
    {
        private IComprobanteDao dao;
        private IPeliculaDao daoP;

        public DataApiImp()
        {
            dao = new ComprobanteDao();
            daoP = new PeliculaDao();
        }

        public List<FormaPago> GetFormaPago()
        {
            return dao.GetFormaPago();
        }

        public List<FormaCompra> GetFormaCompra()
        {
            return dao.GetFormaCompra();
        }

        public List<Cliente> GetClientes()
        {
            return dao.GetClientes();
        }

        public List<Funcion> GetFunciones()
        {
            return dao.GetFunciones();
        }

        public List<ViewComprobante> GetComprobantes()
        {
            return dao.GetComprobantes();
        }

        public bool SaveComprobante(Comprobante comprobante)
        {
            return dao.Crear(comprobante);
        }

        public bool DeleteComprobante(int nro)
        {
            return dao.Borrar(nro);
        }

        public bool UpdateComprobante(Comprobante comprobante)
        {
            return dao.Actualizar(comprobante);

        }


        //SOPORTE
        public List<Pelicula> GetPeliculas()
        {
            return daoP.GetPeliculas();
        }

        public bool SavePelicula(Pelicula pelicula)
        {
            
            return daoP.Crear(pelicula);
            
        }

        public bool DeletePelicula(int nro)
        {
            return daoP.Borrar(nro);
        }

        public bool UpdatePelicula(Pelicula pelicula)
        {
            return daoP.Actualizar(pelicula);
        }

    }
}
