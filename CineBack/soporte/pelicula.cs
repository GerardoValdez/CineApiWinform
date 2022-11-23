using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineBack.soporte
{
    public class Pelicula
    {
        public int IdPelicula { get; set; }
        public int IdClasificacion { get; set; }
        public int IdIdioma { get; set; }
        public int IdDirector { get; set; }
        public string NombrePelicula { get; set; }
        public DateTime FechaEstreno { get; set; }



        public Pelicula()
        {

        }



        public Pelicula(int idPelicula, int idClasificacion, int idIdioma, int idDirector, string nombreP, DateTime fechaEstreno)
        {
            IdPelicula = idPelicula;
            IdClasificacion = idClasificacion;
            IdIdioma = idIdioma;
            IdDirector = idDirector;
            NombrePelicula = nombreP;
            FechaEstreno = fechaEstreno;
                
        }



    }
}
