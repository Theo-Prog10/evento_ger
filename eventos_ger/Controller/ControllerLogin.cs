﻿using eventos_ger.Model;
using eventos_ger.Model.DTOs.Request;
using eventos_ger.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using eventos_ger.Model.DTOs.Response; // Adicione a referência à classe de resposta

namespace eventos_ger.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public LoginController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpGet("login")]
        public async Task<ActionResult<Pessoa?>> Login(string login, string senha)
        {
            var pessoa = await _pessoaService.ValidarLoginAsync(login, senha);
    
            if (pessoa == null)
                return NotFound(null); // Retorna 404 caso não encontre o usuário

            return Ok(pessoa); // Retorna o objeto Pessoa
        }

    }
}