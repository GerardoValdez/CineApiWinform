using CineBack.soporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineBack.acceso_a_datos.interfaz
{
    interface IPeliculaDao
    {
        List<Pelicula> GetPeliculas();

        bool Crear(Pelicula oPelicula);

        bool Actualizar(Pelicula oPelicula);

        bool Borrar(int nro);

    }
}
