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
    public class TiendasController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        string StrConn = string.Empty;
        public TiendasController(IConfiguration configuration)
        {
            _configuration = configuration;
            StrConn = _configuration.GetConnectionString("SQL");
        }

        [HttpGet("GetTiendas")]
        public IEnumerable<Tiendas> GetTiendas()
        {

            return new TiendasNG(StrConn).GetTienda();
        }

        [HttpGet("GetTiendasById")]
        public Tiendas GetTiendasById([FromQuery] string input)
        {

            return new TiendasNG(StrConn).GetTiendaById(input);
        }

        [HttpPost("CreaActualizaTiendas")]
        public IActionResult CreaActualizaTiendas([FromBody] Tiendas input)
        {
            Respuesta res = new TiendasNG(StrConn).CreaActualizaTienda(input);
            if (res == Respuesta.Exito)
                return StatusCode((int)HttpStatusCode.Created);
            else
                return StatusCode((int)HttpStatusCode.Conflict);
        }

        [HttpDelete("BajaTiendas")]
        public IActionResult DeleteTiendas([FromQuery] string input)
        {
            Respuesta res = new TiendasNG(StrConn).BajaTienda(input);
            if (res == Respuesta.Exito)
                return StatusCode((int)HttpStatusCode.Created);
            else
                return StatusCode((int)HttpStatusCode.Conflict);
        }

      
    }
}
