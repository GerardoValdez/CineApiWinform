using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineBack.dominio
{
    public class ViewComprobante
    {
        public int IdComprobante { get; set; }
        public string Cliente { get; set; }
        public string FormaCompra { get; set; }
        public string FormaPago { get; set; }
        public double Total { get; set; }
        public DateTime Fecha { get; set; }

        public ViewComprobante()
        {

        }

        public ViewComprobante(int id, string cliente, string formacompra, string formaPago, DateTime fecha)
        {
            IdComprobante = id;
            Cliente = cliente;
            FormaCompra = formacompra;
            FormaPago = formaPago;
            Fecha = fecha;
        }
    }
}
