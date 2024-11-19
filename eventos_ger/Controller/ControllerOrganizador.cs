using Microsoft.AspNetCore.Mvc;
using eventos_ger.Model;
using eventos_ger.Model.DTOs;
using eventos_ger.Repository.Interfaces;

namespace eventos_ger.Controller;

[ApiController]
public class OrganizadorController : ControllerBase
    {
        private readonly IOrganizadorRepository _organizadorRepository;
        private readonly IEventoRepository _eventoRepository;

        
        public OrganizadorController(IOrganizadorRepository organizadorRepository, IEventoRepository eventoRepository)
        {
            _organizadorRepository = organizadorRepository;
            _eventoRepository = eventoRepository;
        }

        
        [HttpGet("organizadores")]
        public async Task<ActionResult<IEnumerable<OrganizadorDTO>>> GetOrganizadores()
        {
            var organizadores = await _organizadorRepository.ObterTodosAsync();

            var organizadoresDTO = organizadores.Select(o => new OrganizadorDTO
            {
                Id = o.Id,
                Nome = o.nome,
                Nascimento = o.nascimento,
                Cpf = o.cpf,
                Contato = o.contato,
                EventosOrganizados = o.EventosOrganizados
            }).ToList();

            return Ok(organizadoresDTO);
        }

        // Obter organizador por id
        [HttpGet("organizador/{id}")]
        public async Task<ActionResult<OrganizadorDTO>> GetOrganizador(int id)
        {
            var organizador = await _organizadorRepository.ObterPorIdAsync(id);
            if (organizador == null)
            {
                return NotFound(new { mensagem = "Organizador não encontrado." });
            }

            var organizadorDTO = new OrganizadorDTO
            {
                Id = organizador.Id,
                Nome = organizador.nome,
                Nascimento = organizador.nascimento,
                Cpf = organizador.cpf,
                Contato = organizador.contato,
                EventosOrganizados = organizador.EventosOrganizados
            };

            return Ok(organizadorDTO);
        }

        // Criar um novo organizador
        [HttpPost("organizadores")]
        public async Task<ActionResult<OrganizadorDTO>> PostOrganizador(OrganizadorDTO organizadorDTO)
        {
            var organizador = new Organizador
            {
                nome = organizadorDTO.Nome,
                nascimento = organizadorDTO.Nascimento,
                cpf = organizadorDTO.Cpf,
                contato = organizadorDTO.Contato,
                EventosOrganizados = organizadorDTO.EventosOrganizados
            };

            var novoOrganizador = await _organizadorRepository.AdicionarAsync(organizador);

            return CreatedAtAction(nameof(GetOrganizador), new { id = novoOrganizador.Id }, novoOrganizador);
        }

        // Atualizar organizador
        [HttpPut("organizador/{id}")]
        public async Task<IActionResult> PutOrganizador(int id, OrganizadorDTO organizadorDTO)
        {
            if (id != organizadorDTO.Id)
            {
                return BadRequest(new { mensagem = "IDs não coincidem." });
            }

            var organizadorExistente = await _organizadorRepository.ObterPorIdAsync(id);
            if (organizadorExistente == null)
            {
                return NotFound(new { mensagem = "Organizador não encontrado." });
            }

            // Atualizando os dados do organizador
            organizadorExistente.nome = organizadorDTO.Nome;
            organizadorExistente.contato = organizadorDTO.Contato;
            organizadorExistente.cpf = organizadorDTO.Cpf;
            organizadorExistente.nascimento = organizadorDTO.Nascimento;

            await _organizadorRepository.AtualizarAsync(organizadorExistente);

            return NoContent();
        }

        // Deletar organizador
        [HttpDelete("organizador/{id}")]
        public async Task<IActionResult> DeleteOrganizador(int id)
        {
            try
            {
                await _organizadorRepository.DeletarAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno no servidor.", detalhe = ex.Message });
            }
        }
    
}
