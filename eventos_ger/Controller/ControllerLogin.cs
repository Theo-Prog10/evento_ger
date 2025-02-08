using eventos_ger.Model;
using eventos_ger.Model.DTOs.Request;
using eventos_ger.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using eventos_ger.Model.DTOs.Response;

namespace eventos_ger.Controller
{
    [ApiController]
    //[Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public LoginController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpPost("login")] // Alterado para POST
        public async Task<ActionResult<PessoaDTOResponse?>> Login([FromBody] loginRequest request)
        {
            var pessoa = await _pessoaService.ValidarLoginAsync(request.Login, request.Senha);
    
            if (pessoa == null)
                return Unauthorized(); // Melhor que NotFound para credenciais inválidas

            return Ok(pessoa);
        }

    }
}