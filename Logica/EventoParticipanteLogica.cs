using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlazorEvento1.Data;

namespace BlazorEvento1.Logica
{
    public class EventoParticipanteLogica
    {
        private BlazorEFContext _blazorefContext;

        public EventoParticipanteLogica()
        {
            _blazorefContext = new BlazorEFContext();
        }

        //Adiciona EventoParticipante
        public async Task Create(EventoParticipante eventoparticipante)
        {
            await _blazorefContext.EventoParticipante.AddAsync(eventoparticipante);
            await _blazorefContext.SaveChangesAsync();
        }

        //lista os participantes de um evento
        public async Task<List<EventoParticipante>> ListParticipantedoEvento()
        {
            //pegando o último evento
            var evento = await _blazorefContext.Evento.AsNoTracking().OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            int idevento = evento.Id;

            var participantes = await _blazorefContext.EventoParticipante
                                          .Include(p => p.IdParticipanteNavigation)
                                          .Include(x => x.IdEventoNavigation)
                                          .Where(y => y.IdEvento == idevento)
                                          .ToListAsync();
                                          

            return participantes;
        }

        //Listar um participante
        public async Task<EventoParticipante> Get(int id)
        {
            var participante = await _blazorefContext.EventoParticipante.FirstOrDefaultAsync(x => x.IdParticipante == id);

            return participante;
        }
    }
}
