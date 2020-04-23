using System;
using System.Collections.Generic;

namespace BlazorEvento1.Data
{
    public class EventoParticipante
    {
        public int Id { get; set; }
        public int IdEvento { get; set; }
        public int IdParticipante { get; set; }

        public virtual Evento IdEventoNavigation { get; set; }
        public virtual Participante IdParticipanteNavigation { get; set; }
    }
}
