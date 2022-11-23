using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineBack.dominio
{
    public class FormaPago
    {
        public int IdFormaPago { get; set; }
        public string Descripcion { get; set; }

        public FormaPago()
        {

        }

        public FormaPago(int id, string descripcion)
        {
            IdFormaPago = id;
            Descripcion = descripcion;
        }
    }
}
