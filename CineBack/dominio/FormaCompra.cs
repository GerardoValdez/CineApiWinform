using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineBack.dominio
{
    public class FormaCompra
    {
        public int IdFormaCompra { get; set; }
        public string Descripcion { get; set; }


        public FormaCompra()
        {

        }

        public FormaCompra(int id, string descripcion)
        {
            IdFormaCompra = id;
            Descripcion = descripcion;
        }
    }
}
