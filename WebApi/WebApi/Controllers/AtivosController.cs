using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AtivosController : Controller
    {
        public IRepositorio _repo { get; }
        public AtivosController(IRepositorio repo)
        {
            _repo = repo;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
           try 
            {
                var result =await _repo.GetAllAtivosAsync();
                return Ok(result);
            }

            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados falhou");
            }

        }

        [HttpGet("{id_ativo}")]
        public async Task<IActionResult> GetByAtivoId([FromRoute]int id_ativo)
        {
            try
            {
                var result = await _repo.GetAtivoAsyncById(id_ativo);
                return Ok(result);
            }

            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados falhou");
            }
            

        }

        


        [HttpPost]
        public async Task<IActionResult> Post(Ativo model)
        {
            try
            {
                _repo.Add(model);

                if(await _repo.SaveChangesAsync())
                    {
                        return Created($"/api/ativos/{model.IdAtivo}",model);
                    }
              
            }

            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados falhou");
            }

            return BadRequest();

        }

        [HttpPut("{id_ativo}")]
        public async Task<IActionResult> Put(int id_ativo, Ativo model)
        {
            try
            {
                var ativo = await _repo.GetAtivoAsyncById(id_ativo);
                if(ativo==null) return NotFound();

                _repo.Update(model);

                if(await _repo.SaveChangesAsync())
                    {
                        return Created($"/api/ativos/{model.IdAtivo}",ativo);
                    }
              
            }

            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados falhou");
            }
            return BadRequest();

        }

        [HttpDelete("{id_ativo}")]
        public async Task<IActionResult> Delete(int id_ativo)
        {
            try
            {
                  var ativo = await _repo.GetAtivoAsyncById(id_ativo);
                if(ativo==null) return NotFound();

                _repo.Delete(ativo);

                if(await _repo.SaveChangesAsync())
                    {
                        return Ok();
                    }
            }

            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados falhou");
            }
            return BadRequest();

        }


    }
}