using CineBack.fachada;
using CineBack.soporte;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        private IDataApi dataApi; //punto de acceso a la API

        public PeliculaController()
        {
            dataApi = new DataApiImp();
        }


        [HttpGet("/peliculas")]
        public IActionResult GetProductos()
        {
            List<Pelicula> lst = null;
            try
            {
                lst = dataApi.GetPeliculas();
                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }


        [HttpPost("/registrarPelicula")]
        public IActionResult PostPelicula(Pelicula pelicula)
        {
            try
            {
                if (pelicula == null)
                {
                    return BadRequest("Datos de comprobante incorrectos!");
                }
                return Ok(dataApi.SavePelicula(pelicula));
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }


        [HttpPut("/actualizarPelicula")]
        public IActionResult UpdateComprobante(Pelicula pelicula)
        {
            return Ok(dataApi.UpdatePelicula(pelicula));
        }


        [HttpDelete("/borrarPelicula")]
        public IActionResult DeletePelicula(int nro)
        {
            return Ok(dataApi.DeletePelicula(nro));
        }
    }
}
