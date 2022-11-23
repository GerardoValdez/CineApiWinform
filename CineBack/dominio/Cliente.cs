using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineBack.dominio
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }


        public Cliente()
        {

        }

        public Cliente(int id, string nombre)
        {
            IdCliente = id;
            Nombre = nombre;
        }
    }
}
