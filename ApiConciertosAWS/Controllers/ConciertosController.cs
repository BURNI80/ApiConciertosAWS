using ApiConciertosAWS.Models;
using ApiConciertosAWS.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiConciertosAWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConciertosController : ControllerBase
    {
        private ConciertosRepository repo;

        public ConciertosController(ConciertosRepository repo)
        {
            this.repo = repo;
        }


        [HttpGet]
        [Route("/api/[action]")]
        public async Task<ActionResult<List<Categoria>>> GetCategorias()
        {
            return await this.repo.GetCategorias();
        }

        [HttpGet]
        [Route("/api/[action]")]
        public async Task<ActionResult<List<Evento>>> GetEventos()
        {
            return await this.repo.GetEventos();
        }

        [HttpGet]
        [Route("/api/[action]/{id}")]
        public async Task<ActionResult<List<Evento>>> GetEventosCategoria(int id)
        {
            return await this.repo.GetEventosCategoria(id);
        }

        [HttpPost]
        [Route("/api/[action]")]
        public async Task CreateEvento(Evento evento)
        {
            await this.repo.InsertEvento(evento);
        }


    }
}
