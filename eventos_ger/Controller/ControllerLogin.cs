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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] loginRequest request)
        {
            // Chama o serviço para validar as credenciais e obter o resultado
            var resultado = await _pessoaService.ValidarLoginAsync(request.Login, request.Senha);

            // Retorna o tipo de pessoa autenticado
            return Ok(new
            {
                Message = $"{resultado.Login} autenticado.",
                TipoPessoa = resultado.Login
            });
        }
    }
}