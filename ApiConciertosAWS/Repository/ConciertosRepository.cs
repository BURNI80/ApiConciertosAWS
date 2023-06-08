using ApiConciertosAWS.Data;
using ApiConciertosAWS.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiConciertosAWS.Repository
{
    public class ConciertosRepository
    {

        public ConciertosContext context;

        public ConciertosRepository(ConciertosContext context)
        {
            this.context = context;
        }

        public async Task<List<Categoria>> GetCategorias()
        {
            return await this.context.Categorias.ToListAsync();
        }


        public async Task<List<Evento>> GetEventos()
        {
            return await this.context.Eventos.ToListAsync();
        }


        public async Task<List<Evento>> GetEventosCategoria(int id)
        {
            return await this.context.Eventos.Where(x => x.IdCategoria == id).ToListAsync();
        }

        public async Task InsertEvento(Evento evento)
        {
            int newID = this.context.Eventos.Max(x => x.Id) +1;
            evento.Id = newID;

            this.context.Eventos.Add(evento);
            await this.context.SaveChangesAsync();
        }




        }
    }
