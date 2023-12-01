using CosmosDBExample.Models;
using CosmosDBExemple.Services;
using Microsoft.AspNetCore.Mvc;

//  Está no emulador.

namespace CosmosDBExemple.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoasService _pessoasService;
        private readonly ILogger _logger;

        public PessoaController(ILogger<PessoaController> logger, IPessoasService pessoasService)
        {
            _pessoasService = pessoasService;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            var result = await _pessoasService.GetPessoas();
            return Ok(result);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetPessoasPorId(string id)
        {
            try
            {
                var existingPessoa = await _pessoasService.GetPessoasPorId(id);
                if (existingPessoa == null)
                {
                    return NotFound();
                }

                var result = await _pessoasService.GetPessoasPorId(id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var result = await _pessoasService.GetPessoasPorNome(name);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> Add(Pessoa pessoa)
        {
            await _pessoasService.AddPessoa(pessoa);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Pessoa pessoa)
        {
            try
            {
                var existingPessoa = await _pessoasService.GetPessoasPorId(id);

                if (existingPessoa == null)
                {
                    return NotFound("Codigo não localizado !");
                }

                await _pessoasService.UpdatePessoa(id, pessoa);

                return Ok(pessoa);
            }
            catch (Exception)
            {

                return BadRequest("Id tem que ser igual ao Request body.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var existingPessoa = await _pessoasService.GetPessoasPorId(id);

                if (existingPessoa == null)
                {
                    return NotFound();
                }

                await _pessoasService.DeletePessoa(id);

                return Ok(id);
            }
            catch (Exception)
            {

                return BadRequest("Id não localizado.");
            }
        }
    }
}