using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineBack.dominio
{
    public class Comprobante
    {
        public int IdComprobante { get; set; }
        public int IdCliente { get; set; }
        public int IdFormaPago { get; set; }
        public int IdFormaCompra { get; set; }
        public DateTime Fecha { get; set; }
        public int IdEstado { get; set; }


        public List<DetalleComprobante> Detalles { get; set; }


        public Comprobante(int idComprobante, int idCliente, int idFormaPago, int idFormaCompra, DateTime fecha, int idEstado, List<DetalleComprobante> detalles)
        {
            IdComprobante = idComprobante;
            IdCliente = idCliente;
            IdFormaPago = idFormaPago;
            IdFormaCompra = idFormaCompra;
            Fecha = fecha;
            IdEstado = idEstado;
            Detalles = detalles;
        }

        public Comprobante()
        {
            Detalles = new List<DetalleComprobante>();
        }

        public void AgregarDetalle(DetalleComprobante detalle)
        {
            Detalles.Add(detalle);
        }

        public void QuitarDetalle(int indice)
        {
            Detalles.RemoveAt(indice);
        }

        public double CalcularTotal()
        {
            double total = 0;
            foreach (DetalleComprobante item in Detalles)
                total += item.CalcularSubTotal();
            return total;
        }
    }
}
