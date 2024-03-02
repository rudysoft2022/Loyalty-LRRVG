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
    public class VentasController : ControllerBase
    {
       
        [HttpPost("RegistraVentas")]
        public IActionResult RegistraVentas([FromBody] Ventas input)
        {
            Respuesta res = new VentasNG().RegistraVentas(input);
            if (res == Respuesta.Exito)
                return StatusCode((int)HttpStatusCode.Created);
            else
                return StatusCode((int)HttpStatusCode.Conflict);
        }
    }
}
