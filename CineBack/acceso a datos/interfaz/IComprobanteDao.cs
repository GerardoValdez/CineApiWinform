using CineBack.dominio;
using CineBack.soporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineBack.acceso_a_datos
{
    interface IComprobanteDao
    {
        List<FormaCompra> GetFormaCompra();

        List<FormaPago> GetFormaPago();

        List<Cliente> GetClientes();

        List<Funcion> GetFunciones();

        List<ViewComprobante> GetComprobantes();

        bool Crear(Comprobante oComprobante);

        bool Actualizar(Comprobante oComprobante);
       
        bool Borrar(int nro);
        
    }
}
