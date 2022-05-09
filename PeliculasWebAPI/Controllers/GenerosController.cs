using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasWebAPI.Entidades;

namespace PeliculasWebAPI.Controllers {
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase {
        private readonly ApplicationDBContext context;

        public GenerosController(ApplicationDBContext context) {
            this.context = context;
        }

        /* endpoint */
        [HttpGet]
        public async Task<IEnumerable<Genero>> Get() {
            return await context.Generos
                                .OrderBy(g => g.Nombre)
                                .ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Genero>> PorId(int id) {
            var genero = await context.Generos
                                .FirstOrDefaultAsync(p => p.Identificador == id);

            if (genero is null) {
                return NotFound();
            }

            return genero;
        }

        /* Filtración con Ordenamiento */
        [HttpGet("filtrar")]
        public async Task<IEnumerable<Genero>> FiltrarPorFrase(string nombre) {
            return await context.Generos
                                .Where(g => g.Nombre.Contains(nombre))
                                .OrderBy(g => g.Nombre)
                                .ToListAsync();
        }

        [HttpGet("paginacion")]
        public async Task<ActionResult<IEnumerable<Genero>>> GetPacionacion(int page = 1) {
            var registrosPagina = 2;
            var genero = await context.Generos
                                      .Skip((page - 1) * registrosPagina) /* Salta el primer registro */
                                      .Take(registrosPagina) /* Toma dos registros */
                                      .ToListAsync();
            if (genero is null) {
                NotFound();
            }

            return genero;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Genero genero) {
            var status = context.Entry(genero).State;
            
            /* Guarda el status */
            context.Add(genero);
            var status2 = context.Entry(genero).State;

            /* Agrega el estado genero a la tabla Generos */
            await context.SaveChangesAsync();
            var status3 = context.Entry(genero).State;

            return Ok();
        }

        [HttpPost("variosGeneros")]
        public async Task<ActionResult> Post(Genero[] generos) { 
            context.AddRange(generos);

            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
