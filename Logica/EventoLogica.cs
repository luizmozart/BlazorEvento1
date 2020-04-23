using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlazorEvento1.Data;


namespace BlazorEvento1.Logica
{
    public class EventoLogica
    {
        private BlazorEFContext _eventoContext;

        public EventoLogica()
        {
            _eventoContext = new BlazorEFContext();
        }

        public async Task<Evento> GetMaior()
        {
            var evento = await _eventoContext.Evento.AsNoTracking().OrderByDescending(x => x.Id).FirstOrDefaultAsync();

            return evento;

        }

        //lista os participantes de um evento
        public async Task<List<Evento>> ListParticipantedoEvento()
        {
            //pegando o último evento
            var evento = await _eventoContext.Evento
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            var participantes = await _eventoContext.Evento
                .Include(x => x.EventoParticipante)
                .Where(y => y.Id == evento.Id)
                .ToListAsync();

            return participantes;
        }

    }
}
