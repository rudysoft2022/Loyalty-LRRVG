using CRUD.Model.Entidades;
using CRUD.Model.NG;
using CRUD.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {


        [HttpGet("GetClientes")]
        public IEnumerable<Clientes> GetClientes()
        {

            return new ClientesNG().GetClientes();
        }

        [HttpGet("GetClientesById")]
        public Clientes GetClientesById([FromQuery] string input)
        {

            return new ClientesNG().GetClienteById(input);
        }

        [HttpPost("CreaActualizaClientes")]
        public IActionResult CreaActualizaClientes([FromBody] Clientes input)
        {
            Respuesta res = new ClientesNG().CreaActualizaCliente(input);
            if (res == Respuesta.Exito)
                return StatusCode((int)HttpStatusCode.Created);
            else
                return StatusCode((int)HttpStatusCode.Conflict);
        }

        [HttpDelete("BajaClientes")]
        public IActionResult DeleteClientes([FromQuery] string input)
        {
            Respuesta res = new ClientesNG().BajaCliente(input);
            if (res == Respuesta.Exito)
                return StatusCode((int)HttpStatusCode.Created);
            else
                return StatusCode((int)HttpStatusCode.Conflict);
        }

        [HttpPost("LoginCliente")]
        public Clientes LoginCliente([FromBody] LoginCliente input)
        {
            return new ClientesNG().LoginCliente(input);
        }
    }
}
