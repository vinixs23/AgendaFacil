using AgendaFacil.Models;
using AgendaFacil.Services;
using Microsoft.AspNetCore.Mvc;

namespace AgendaFacil.Controllers;

    [ApiController]
    [Route("api/agendamentos")]
    public class AgendamentoController : ControllerBase
    {
        private readonly AgendamentoService _AgendamentoService;

        public AgendamentoController(AgendamentoService agendamentoService)
        {
            _AgendamentoService = agendamentoService;
        }


        [HttpPost("CriarAgendamento")]
        public IActionResult CreateAgendamento([FromBody] Agendamento model)
        {
            var resultado = _AgendamentoService.CreateAgendamento(model);
            return Ok(resultado);
        }
    }
    
