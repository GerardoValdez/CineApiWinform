using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineBack.dominio
{
    public class DetalleComprobante
    {
        public int IdDetalleComprobante { get; set; }
        public int IdComprobante { get; set; }
        public Funcion Funcion { get; set; }
        public int Cantidad { get; set; }

        public DetalleComprobante() 
        {

        }

        public DetalleComprobante(Funcion funcion, int cantidad)
        {
            Funcion = funcion;
            Cantidad = cantidad;
        }

        public DetalleComprobante(int idDetalleComprobante, int idComprobante, Funcion funcion, int cantidad)
        {
            IdDetalleComprobante = idDetalleComprobante;
            IdComprobante = idComprobante;
            Funcion = funcion;
            Cantidad = cantidad;
        }

        public double CalcularSubTotal()
        {
            return Funcion.Precio * Cantidad;
        }
    }
}

