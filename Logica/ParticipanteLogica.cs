using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlazorEvento1.Data;


namespace BlazorEvento1.Logica
{
    public class ParticipanteLogica
    {
        private BlazorEFContext _eventoContext;

        public ParticipanteLogica()
        {
            _eventoContext = new BlazorEFContext();
        }

        //lista todos os participantes
        public async Task<List<Participante>> List()
        {
            return await _eventoContext.Participante.ToListAsync();
        }

        //Adiciona um participante
        public async Task Create(Participante participante)
        {

            await _eventoContext.Participante.AddAsync(participante);
            await _eventoContext.SaveChangesAsync();
        }

        //Editar um participante
        public async Task Edit(Participante participante)
        {
            _eventoContext.Entry(participante).State = EntityState.Modified;
            await _eventoContext.SaveChangesAsync();
        }


        //Excluir um participante
        public async Task Delete(int id)
        {
            var participante = await _eventoContext.Participante.FirstOrDefaultAsync(x => x.Id == id);

            if (participante != null)
            {
                _eventoContext.Participante.Remove(participante);
                await _eventoContext.SaveChangesAsync();
            }
        }

        //Listar um participante
        public async Task<Participante> Get(int id)
        {
            var participante = await _eventoContext.Participante.FirstOrDefaultAsync(x => x.Id == id);

            return participante;
        }

        public async Task<Participante> GetBycpf(string cpf)
        {
            var particpante = await _eventoContext.Participante.FirstOrDefaultAsync(x => x.Cpf == cpf);

            return particpante;
        }

        //lista os participantes de um evento
        public async Task<List<Participante>> ListParticipantedoEvento()
        {
            //pegando o último evento
            
            var evento = await _eventoContext.Evento
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

            int idevento = evento.Id;

            var participantes = await _eventoContext.Participante
                                          .Include(x => x.EventoParticipante)
                                          //.FirstOrDefault(y => y.IdEvento == evento.Id)
                                          .OrderByDescending(x => x.Id)  ///aqui está ordenando por evento e não por participante o "Id" aqui se refere ao Id do evento que é o contexto sendo consultado
                                          .ToListAsync();

            //var participantes = await _eventoContext.Participante
            //    .Include(x => x.EventoParticipante.Where(ep => ep.IdEvento == idevento)
            //    .AsEnumerable().ToListAsync();      

            return participantes;
        }

        public bool ValidaCPF(string vrCPF)
        {
            string valor = vrCPF.Replace(".", "");
            valor = valor.Replace("-", "");
            if (valor.Length != 11)
                return false;
            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(
                  valor[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;


            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];
            resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;
            return true;
        }
    }
}
