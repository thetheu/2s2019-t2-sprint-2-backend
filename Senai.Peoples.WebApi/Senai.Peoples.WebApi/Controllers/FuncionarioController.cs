using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Repositories;

namespace Senai.Peoples.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {

        List<FuncionarioDomain> funcionarios = new List<FuncionarioDomain>();

        FuncionarioRepository FuncionarioRepository = new FuncionarioRepository();

        [HttpGet]
        public IEnumerable<FuncionarioDomain> ListarTodos()
        {
            return FuncionarioRepository.Listar();
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            FuncionarioDomain funcionarioDomain = FuncionarioRepository.BuscarPorId(id);
            if (funcionarioDomain == null)
                return NotFound();
            return Ok(funcionarioDomain);
        }

        [HttpPost]
        public IActionResult Cadastrar(FuncionarioDomain funcionarioDomain)
        {
            FuncionarioRepository.Cadastrar(funcionarioDomain);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar (int id)
        {
            FuncionarioRepository.Deletar(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar (FuncionarioDomain funcionarioDomain, int id)
        {
            funcionarioDomain.IdFuncionarios = id;
            FuncionarioRepository.Atualizar(funcionarioDomain);
            return Ok();
        }
    }
}