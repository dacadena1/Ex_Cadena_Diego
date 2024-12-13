using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using BLL;
using Entities;

namespace API.Controllers
{
    [RoutePrefix("api/pagos")]
    public class PagosController : ApiController
    {
        private readonly PagosLogic _logic = new PagosLogic();

        // GET: http://localhost:54354/api/pagos
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var pagos = _logic.RetrieveAll();
            return Ok(pagos);
        }

        // GET: http://localhost:54354/api/pagos/{id}
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            var pago = _logic.RetrieveById(id);
            if (pago == null)
            {
                return NotFound();
            }
            return Ok(pago);
        }

        // POST: http://localhost:54354/api/pagos
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] Pagos pago)
        {
            if (pago == null)
            {
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
            }

            try
            {
                var created = _logic.Create(pago);
                return Created($"/api/pagos/{created.PagoID}", created);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: http://localhost:54354/api/pagos/{id}
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(int id, [FromBody] Pagos pago)
        {
            if (pago == null || id != pago.PagoID)
            {
                return BadRequest("Los datos proporcionados no son válidos.");
            }

            try
            {
                bool updated = _logic.Update(pago);
                if (!updated)
                {
                    return NotFound();
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: http://localhost:54354/api/pagos/{id}
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                bool deleted = _logic.Delete(id);
                if (!deleted)
                {
                    return NotFound();
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
