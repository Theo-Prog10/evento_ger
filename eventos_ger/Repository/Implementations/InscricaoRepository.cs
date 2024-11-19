using System.Data.Entity;
using eventos_ger.Model;
using eventos_ger.Model.DTOs;
using eventos_ger.Repository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace eventos_ger.Repository;

public class InscricaoRepository : IInscricaoRepository
{
    private readonly Ger_Evento_Bd _context;

    public InscricaoRepository(Ger_Evento_Bd context)
    {
        _context = context;
    }

    public async Task<Evento> AddParticipante(int eventoId, int participanteId)
    {
        // Buscar o evento
        var evento = _context.Eventos
            .FirstOrDefault(e => e.Id == eventoId);

        if (evento == null) throw new ArgumentException($"Evento com ID {eventoId} não encontrado.");

        // Buscar o participante
        var participante = _context.Participantes
            .FirstOrDefault(p => p.Id == participanteId);

        if (participante == null) throw new ArgumentException($"Participante com ID {participanteId} não encontrado.");
        if (evento.Participantes.Contains(participante.Id))
            throw new InvalidOperationException($"Participante com ID {participanteId} já está associado ao evento.");

        // Adicionar o participante ao evento e vice-versa
        evento.Participantes.Add(participante.Id);
        participante.eventosInscritos.Add(evento.Id);

        // Salvar as mudanças
        _context.SaveChanges();

        return evento;
    }


    public async Task<Evento> AddPalestrante(int eventoId, int palestranteId)
    {
        // Buscar o evento
        var evento = _context.Eventos
            .FirstOrDefault(e => e.Id == eventoId);

        if (evento == null)
            throw new ArgumentException($"Evento com ID {eventoId} não encontrado.");

        // Buscar o palestrante
        var palestrante = _context.Palestrantes
            .FirstOrDefault(p => p.Id == palestranteId);

        if (palestrante == null)
            throw new ArgumentException($"Palestrante com ID {palestranteId} não encontrado.");

        // Verificar se o palestrante já está associado ao evento
        if (evento.palestrantes_presentes.Contains(palestrante.Id))
            throw new InvalidOperationException($"Palestrante com ID {palestranteId} já está associado ao evento.");

        // Adicionar o palestrante ao evento e vice-versa
        evento.palestrantes_presentes.Add(palestrante.Id);
        palestrante.palestras_ministradas.Add(evento.Id);

        // Salvar as mudanças
        _context.SaveChanges();

        // Retornar o evento atualizado
        return evento;
    }


    public async Task DeleteParticipanteEvento(int participanteId, int eventoId)
    {
        // Buscar o evento
        var evento = _context.Eventos
            .FirstOrDefault(e => e.Id == eventoId);

        if (evento == null)
            throw new ArgumentException($"Evento com ID {eventoId} não encontrado.");

        // Buscar o participante
        var participante = _context.Participantes
            .FirstOrDefault(p => p.Id == participanteId);

        if (participante == null)
            throw new ArgumentException($"Participante com ID {participanteId} não encontrado.");

        // Verificar se o participante está associado ao evento
        if (!evento.Participantes.Contains(participante.Id))
            throw new InvalidOperationException(
                $"Participante com ID {participanteId} não está associado ao evento com ID {eventoId}.");

        // Remover o participante do evento e vice-versa
        evento.Participantes.Remove(participante.Id);
        participante.eventosInscritos.Remove(evento.Id);

        // Salvar as alterações
        _context.SaveChanges();
    }

    public async Task DeletePalestranteEvento(int palestranteId, int eventoId)
    {
        // Buscar o evento
        var evento = _context.Eventos
            .FirstOrDefault(e => e.Id == eventoId);

        if (evento == null)
            throw new ArgumentException($"Evento com ID {eventoId} não encontrado.");

        // Buscar o palestrante
        var palestrante = _context.Palestrantes
            .FirstOrDefault(p => p.Id == palestranteId);

        if (palestrante == null)
            throw new ArgumentException($"Palestrante com ID {palestranteId} não encontrado.");

        // Verificar se o palestrante está associado ao evento
        if (!evento.palestrantes_presentes.Contains(palestrante.Id))
            throw new InvalidOperationException(
                $"Palestrante com ID {palestranteId} não está associado ao evento com ID {eventoId}.");

        // Remover o palestrante do evento e vice-versa
        evento.palestrantes_presentes.Remove(palestrante.Id);
        palestrante.palestras_ministradas.Remove(evento.Id);

        // Salvar as alterações
        _context.SaveChanges();
    }
}