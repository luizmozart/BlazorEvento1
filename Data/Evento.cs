using System;
using System.Collections.Generic;

namespace BlazorEvento1.Data
{
    public class Evento
    {
        public Evento()
        {
            EventoParticipante = new HashSet<EventoParticipante>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public int QuantidadeInscritos { get; set; }

        public virtual ICollection<EventoParticipante> EventoParticipante { get; set; }
    }
}
