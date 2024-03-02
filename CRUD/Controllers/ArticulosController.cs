using CRUD.Model;
using CRUD.Model.Entidades;
using CRUD.Model.NG;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        string StrConn = string.Empty;
        public ArticulosController(IConfiguration configuration)
        {
            _configuration = configuration;
            StrConn = _configuration.GetConnectionString("SQL");
        }

        [HttpGet("GetArticulos")]
        public IEnumerable<Articulos> GetArticulos()
        {

            return new ArticulosNG(StrConn).GetArticulos();
        }

        [HttpGet("GetArticuloById")]
        public Articulos GetArticuloById([FromQuery] string input)
        {

            return new ArticulosNG(StrConn).GetArticuloById(input);
        }

        [HttpPost("CreaActualizaArticulo")]
        public IActionResult CreaActualizaArticulo([FromBody] Articulos input)
        {
            Respuesta res = new ArticulosNG(StrConn).CreaActualizaArticulo(input);
            if (res == Respuesta.Exito)
                return StatusCode((int)HttpStatusCode.Created);
            else
                return StatusCode((int)HttpStatusCode.Conflict);
        }

        [HttpDelete("BajaArticulo")]
        public IActionResult DeleteArticulo([FromQuery] string input)
        {
            Respuesta res = new ArticulosNG(StrConn).BajaArticulo(input);
            if (res == Respuesta.Exito)
                return StatusCode((int)HttpStatusCode.Created);
            else
                return StatusCode((int)HttpStatusCode.Conflict);
        }
    }
}
