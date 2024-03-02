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
        private readonly IConfiguration _configuration;

        string StrConn = string.Empty;
        public ClientesController(IConfiguration configuration)
        {
            _configuration = configuration;
            StrConn = _configuration.GetConnectionString("SQL");
        }

        [HttpGet("GetClientes")]
        public IEnumerable<Clientes> GetClientes()
        {

            return new ClientesNG(StrConn).GetClientes();
        }

        [HttpGet("GetClientesById")]
        public Clientes GetClientesById([FromQuery] string input)
        {

            return new ClientesNG(StrConn).GetClienteById(input);
        }

        [HttpPost("CreaActualizaClientes")]
        public IActionResult CreaActualizaClientes([FromBody] Clientes input)
        {
            Respuesta res = new ClientesNG(StrConn).CreaActualizaCliente(input);
            if (res == Respuesta.Exito)
                return StatusCode((int)HttpStatusCode.Created);
            else
                return StatusCode((int)HttpStatusCode.Conflict);
        }

        [HttpDelete("BajaClientes")]
        public IActionResult DeleteClientes([FromQuery] string input)
        {
            Respuesta res = new ClientesNG(StrConn).BajaCliente(input);
            if (res == Respuesta.Exito)
                return StatusCode((int)HttpStatusCode.Created);
            else
                return StatusCode((int)HttpStatusCode.Conflict);
        }

        [HttpPost("LoginCliente")]
        public Clientes LoginCliente([FromBody] LoginCliente input)
        {
            return new ClientesNG(StrConn).LoginCliente(input);
        }
    }
}
