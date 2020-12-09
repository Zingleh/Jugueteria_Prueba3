using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jugueteria_Prueba3.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Jugueteria_Prueba3.Models;

namespace Jugueteria_Prueba3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JugueteController : ControllerBase
    {
        // GET: api/Juguete
        [HttpGet("Hola")]
        public JsonResult Saludos()
        {
            return new JsonResult("hola!");
        }

        // GET: api/Juguete/5
        [HttpGet("all")]
        public JsonResult ObtenerJuguete()
        {
            var juguetesRecibidos  = JugueteAzure.ObtenerJuguete();
            return new JsonResult(juguetesRecibidos);
        }

        [HttpGet("{id_juguete}")]
        public JsonResult ObtenerJuguete(string id_juguete)
        {
            var conversionExitosa = int.TryParse(id_juguete, out int idConvertido);
            Juguete jugueteRecivido;

            if (conversionExitosa)
            {
                jugueteRecivido = JugueteAzure.ObtenerJugueteporID(idConvertido);
            }
            else
            {
                jugueteRecivido = JugueteAzure.ObtenerJuguetePorNombre(id_juguete);
            }

            if (jugueteRecivido is null)
            {
                return new JsonResult($"Intente nuevamente con un parametro distinto a {jugueteRecivido}");
            }
            else
            {
                return new JsonResult(jugueteRecivido);
            }

        }

    }
}
