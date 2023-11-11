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

        [HttpGet("{name}")]
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
    }
}