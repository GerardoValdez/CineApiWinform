using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineBack.dominio
{
    public class Funcion
    {
        public int IdFuncion { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }

        //public int IdHorarioFuncion { get; set; }
        //public int IdPelicula { get; set; }


        public Funcion()
        {

        }

        public Funcion(int idFuncion, string descripcion, double precio)
        {
            IdFuncion = idFuncion;
            Descripcion = descripcion;
            Precio = precio;

            //IdHorarioFuncion = idHorarioFuncion;
            //IdPelicula = idPelicula;
        }


    }
}
