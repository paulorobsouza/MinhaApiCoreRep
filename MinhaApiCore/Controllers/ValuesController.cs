using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MinhaApiCore.Controllers
{
    [Route("api/[controller]")]
   
    public class ValuesController : MainController
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> ObterTodos()
        {
            var valores = new string[] { "value1", "value2" };

            if (valores.Length < 5000)
                return BadRequest();

            return valores;
        }

        public ActionResult ObterResultado()
        {
            var valores = new string[] { "value1", "value2" };

            if (valores.Length < 5000)
                return CustomResponse();

            return CustomResponse(valores);
        }


        [HttpGet("obter-valores")]
        public IEnumerable<string> ObterValores()
        {
            var valores = new string[] { "value1", "value2" };

            if (valores.Length < 5000)
                return null;

            return valores;
        }


        // GET api/values/obter-por-id/5
        [HttpGet("obter-por-id/{id:int}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        //[ApiConventionMethod(typeof(DefaultApiConventions),nameof(DefaultApiConvetions.Post))]
        public ActionResult Post(Product value)
        {
            if (value.Id == 0)
                return BadRequest();

            //ok banco
            //return Ok(value);

            //return CreatedAtAction("Post", value);

            return CreatedAtAction(nameof(Post), value);
        }

        [HttpPut("{id}")]
        public ActionResult Put([FromRoute]int id , [FromForm]Product value)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (id != value.Id) return NotFound();
            
            //ok banco
            return Ok(value);

            //return CreatedAtAction("Post", value);
            //retur NoContent(); codigo 204 sem conteúdo
            
        }


        // PUT api/values/5
        
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete([FromQuery]int id)
        {
        }

       
    }
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected ActionResult CustomResponse(object result = null)
        {
            if (OperaValida())
            {
                return Ok(new
                {
                    sucess = true,
                    data = result
                }) ;
            }
            return BadRequest(new
            {
                sucess = false,
                errors = ObterErros()
            });
        }

        public bool OperaValida()
        {
            //validações

            return true;
        }

        protected string ObterErros()
        {
            return "";

        }
    }

    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
