using CineBack.dominio;
using CineBack.fachada;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CineWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprobanteController : ControllerBase
    {
        private IDataApi dataApi; //punto de acceso a la API

        public ComprobanteController()
        {
            dataApi = new DataApiImp();
        }

        //GET
        [HttpGet("/formaCompra")]
        public IActionResult GetFormaCompra()
        {
            List<FormaCompra> lst = null;
            try
            {
                lst = dataApi.GetFormaCompra();

                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        //GET
        [HttpGet("/formaPago")]
        public IActionResult GetFormaPago()
        {
            List<FormaPago> lst = null;
            try
            {
                lst = dataApi.GetFormaPago();

                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        //GET
        [HttpGet("/clientes")]
        public IActionResult GetClientes()
        {
            List<Cliente> lst = null;
            try
            {
                lst = dataApi.GetClientes();

                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        // GET
        [HttpGet("/funciones")]
        public IActionResult GetFunciones()
        {
            List<Funcion> lst = null;
            try
            {
                lst = dataApi.GetFunciones();

                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }

        // GET
        [HttpGet("/comprobantes")]
        public IActionResult GetComprobantes()
        {
            List<ViewComprobante> lst = null;
            try
            {
                lst = dataApi.GetComprobantes();

                return Ok(lst);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }


        // POST
        [HttpPost("/cargarComprobante")]
        public IActionResult PostComprobante(Comprobante comprobante)
        {
            try
            {
                if (comprobante == null)
                {
                    return BadRequest("Datos del comprobante incorrectos!");
                }

                return Ok(dataApi.SaveComprobante(comprobante));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno! Intente luego");
            }
        }


        // PUT 
        [HttpPut("/actualizarComprobante")]
        public IActionResult updateComprobante(Comprobante comprobante)
        {
            return Ok(dataApi.UpdateComprobante(comprobante));
        }


        // DELETE
        [HttpDelete("/borrarComprobante")]
        public IActionResult DeleteComprobante(int nro)
        {
            return Ok(dataApi.DeleteComprobante(nro));
        }
    }
}
