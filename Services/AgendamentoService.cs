using AgendaFacil.Controllers;
using AgendaFacil.Models;

namespace AgendaFacil.Services;

public class AgendamentoService
{
    private readonly AgendamentoRepository _AgendamentoRepository;

    public AgendamentoService(AgendamentoRepository agendamentoRepository)
    {
        _AgendamentoRepository = agendamentoRepository;
    }

    public Agendamento CreateAgendamento(Agendamento model) {
        var vagas = _AgendamentoRepository.ObterVagasRestantes(model.HorarioId);
        if (vagas <= 0)
            throw new InvalidOperationException("Vagas esgotadas para este horário.");

        var usuarioId = _AgendamentoRepository.CriarUsuario(model.NomeCompleto, model.Email, model.Telefone);
        var codigo = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
        var agendamentoId = _AgendamentoRepository.CriarAgendamento(usuarioId, model.EventoId, model.HorarioId, codigo);
        var eventoHorario = _AgendamentoRepository.ObterDadosEventoHorario(model.EventoId, model.HorarioId);

        return new Agendamento {
            Id = agendamentoId,
            UsuarioId = usuarioId,
            EventoId = model.EventoId,
            HorarioId = model.HorarioId,
            NomeCompleto = model.NomeCompleto,
            Email = model.Email,
            TituloEvento = eventoHorario.Titulo,
            DataHora = eventoHorario.DataHora,
            Codigo = codigo,
            DataAgendamento = DateTime.Now
        };
    }
    
}